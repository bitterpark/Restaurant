using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Utility;
using Assets.Scripts;
using Assets.Scripts.Inventory;
//
[CustomPropertyDrawer(typeof(Wrapper<>), true)]
public class WrapperEditor : PropertyDrawer
{
    int selected = -1;
    List<Type> acceptedTypes;
	GUIContent[] humanReadableTypes;

	const float SPACING = 5f;
	private const float LABEL_WIDTH_PER_LETTER = 7.5f;
	Type wrappedGenericType;

    string MainLabelText { get {
            return $"{fieldInfo.Name} ({wrappedGenericType})";
        } 
    }

    enum DisplayCurrently {UObj, PlainObj, TypesPopup }

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {

		var plainObj = property.FindPropertyRelative("plainObj");

		var uObj = property.FindPropertyRelative("uObj");

		if (wrappedGenericType == null) {
			wrappedGenericType = fieldInfo.FieldType.BaseType.GetGenericArguments()[0];
		}

		string typeName;
		bool plainObjIsSet;
		InitStuff(plainObj, out plainObjIsSet, out typeName);
		
		var uObjIsSet = VerifyUObjType(uObj);
		DisplayCurrently displayMode;
		if (plainObjIsSet) {
			displayMode = DisplayCurrently.PlainObj;
		} else if (uObjIsSet) {
			displayMode = DisplayCurrently.UObj;
		} else {
			displayMode = DisplayCurrently.TypesPopup;
		}

		var lineHeight = EditorGUIUtility.singleLineHeight;

		var labelPos = new Rect(position);
		labelPos.height = lineHeight;

		var xButtonPos = new Rect(position);
		xButtonPos.width *= 0.05f;
		xButtonPos.height = lineHeight;

		var fieldPos = new Rect(position);
		fieldPos.y += lineHeight + SPACING;
		fieldPos.height = lineHeight;

		if (displayMode == DisplayCurrently.PlainObj) {
			DrawXButton(xButtonPos, plainObj, uObj);
			labelPos.width -= xButtonPos.width + SPACING;
			labelPos.x += xButtonPos.width + SPACING;
			DrawLabel(labelPos, MainLabelText);
			//PLAIN OBJECT FIELD
			DrawPropertyField(fieldPos, plainObj, typeName);

		} else if (displayMode == DisplayCurrently.UObj) {
			DrawXButton(xButtonPos, plainObj, uObj);
			labelPos.width -= xButtonPos.width + SPACING;
			labelPos.x += xButtonPos.width + SPACING;
			DrawLabel(labelPos, MainLabelText);
			//UNITY OBJECT FIELD
			DrawPropertyField(fieldPos, uObj, typeName);

		} else {
			DrawLabel(labelPos, MainLabelText);

			var leftHalf = new Rect(fieldPos);
			leftHalf.width *= 0.5f;

			DrawPropertyField(leftHalf, uObj, "");

			var rightHalf = new Rect(leftHalf);
			rightHalf.width -= SPACING;
			rightHalf.x += leftHalf.width + SPACING;
			DrawPopup(rightHalf, plainObj, humanReadableTypes);
		}

		DrawSeparator(position);
	}

	private bool VerifyUObjType(SerializedProperty uObj) {
		bool uObjIsSet = uObj.objectReferenceValue != null;
		if (uObjIsSet && !wrappedGenericType.IsAssignableFrom(uObj.objectReferenceValue.GetType())) {
			if (uObj.objectReferenceValue is GameObject uobjGO) {
				var goComp = uobjGO.GetComponent(wrappedGenericType);
				if (goComp != null) {
					uObj.objectReferenceValue = goComp;
					return true;
				}
			}
			uObjIsSet = false;
			uObj.objectReferenceValue = null;
			Debug.LogError($"Fielf requires object assignable from type {wrappedGenericType}");
		}

		return uObjIsSet;
	}

	private void DrawLabel(Rect labelRect, string text) {
        var origLabelWidth = EditorGUIUtility.labelWidth;
        EditorGUIUtility.labelWidth = 80f;
        EditorGUI.LabelField(labelRect, text);
        EditorGUIUtility.labelWidth = origLabelWidth;
    }

    private void DrawPropertyField(Rect propertyRect, SerializedProperty property, string label) {
        EditorGUI.indentLevel+=2;
        var origLabelWidth = EditorGUIUtility.labelWidth;
        float proceduralLabelWidth = label.Length * LABEL_WIDTH_PER_LETTER;
        EditorGUIUtility.labelWidth = proceduralLabelWidth;
        EditorGUI.PropertyField(propertyRect, property, new GUIContent(label), true);
        EditorGUIUtility.labelWidth = origLabelWidth; 
        EditorGUI.indentLevel--;
    }



	private void DrawXButton(Rect position, SerializedProperty plainObj, SerializedProperty uObj) {
        var buttonPos = position;

        if (GUI.Button(buttonPos, "X")) {
            plainObj.managedReferenceValue = null;
            uObj.objectReferenceValue = null;
            selected = -1;
        }
    }

    private void DrawPopup(Rect position, SerializedProperty plainObj, GUIContent[] humanReadableTypes) {
        selected = EditorGUI.Popup(position, GUIContent.none, selected, humanReadableTypes);
        if (selected >= 0 && acceptedTypes.Count > selected) {
            var theType = acceptedTypes[selected];
            CreateInstanceOfT(theType, plainObj);
            selected = -1;
        }
    }

    void DrawSeparator(Rect rect) {
        Vector2 point1 = rect.position + new Vector2(0, rect.height - SPACING);
		Vector2 point2 = point1 + new Vector2(rect.width, 0);
        Handles.DrawLine(point1, point2);
    }

	private void InitStuff(SerializedProperty plainObj, out bool plainObjIsSet, out string plainObjTypeName) { 
        if (acceptedTypes == null) {
			acceptedTypes = GetAcceptedTypes();
			humanReadableTypes = acceptedTypes.ConvertAll(arg => new GUIContent(arg.Name.ToString())).ToArray();
		}
		plainObjTypeName = plainObj.managedReferenceFullTypename;
		plainObjIsSet = plainObjTypeName != null && plainObjTypeName != "";
	}

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
        var height = EditorGUIUtility.singleLineHeight + SPACING * 2;

        var plainObj = property.FindPropertyRelative("plainObj");
        var uObj = property.FindPropertyRelative("uObj");
        var typeName = plainObj.managedReferenceFullTypename;
        bool plainObjIsSet = typeName != null && typeName != "";

        if (plainObjIsSet) {
            height += EditorGUI.GetPropertyHeight(plainObj);
		} else {
            height += EditorGUI.GetPropertyHeight(uObj);
        }
        height += SPACING;

        return height;
    }

    private void CreateInstanceOfT(Type type, SerializedProperty plainObj) {
        plainObj.managedReferenceValue = Activator.CreateInstance(type);
        Debug.Log(type.ToString());
    }

    /// <summary>
    /// Возвращает типы классов, реализующих T
    /// </summary>
    /// <returns></returns>
    List<Type> GetAcceptedTypes() {
        List<Type> result = new List<Type>();
        var t = wrappedGenericType;
        return AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes()).Where(type =>
            t.IsAssignableFrom(type) && 
			!type.IsInterface && 
			!type.IsGenericType && 
			!type.IsAbstract &&
			!typeof(UnityEngine.Object).IsAssignableFrom(type))
			.ToList();
    }
}



