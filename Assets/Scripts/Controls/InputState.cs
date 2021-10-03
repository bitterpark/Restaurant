using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Controls
{
	[CreateAssetMenu(fileName ="InputAccessor", menuName = "ScriptableObjects/Global State/Input", order = 0)]
	public class InputState : ScriptableObject, IInputSource
	{
		[SerializeField]
		Inventory.InventoryState invState;
		[SerializeField]
		UI.MainMenuState menuState;
		
		public bool InputEnabled => !invState.IsOpen && !menuState.IsOpen;

		float IInputSource.GetHorizontalAxis(bool raw) {
			return GetAxisValue("Horizontal", raw);
		}	
		float IInputSource.GetVerticalAxis(bool raw) {
			return GetAxisValue("Vertical", raw);
		}
		float IInputSource.GetMouseX(bool raw) {
			return GetAxisValue("Mouse X", raw);
		}
		float IInputSource.GetMouseY(bool raw) {
			return GetAxisValue("Mouse Y", raw);
		}

		float IInputSource.GetAxis(string axis, bool raw) {
			return GetAxisValue(axis, raw);
		}

		private float GetAxisValue(string axisName, bool getRaw) {
			float axis = 0;
			if (InputEnabled) {
				axis = getRaw? Input.GetAxisRaw(axisName) : Input.GetAxis(axisName);
			}
			return axis;
		}

		bool IInputSource.GetMouseButtonDown(int button) {
			if (InputEnabled) {
				return Input.GetMouseButtonDown(button);
			} else {
				return false;
			}
		}

		
	}

	public interface IInputSource
	{
		bool InputEnabled { get; }

		float GetAxis(string axis, bool raw);


		float GetHorizontalAxis(bool raw);
		float GetVerticalAxis(bool raw);

		float GetMouseX(bool getRaw);
		float GetMouseY(bool getRaw);

		bool GetMouseButtonDown(int button);
	}
}