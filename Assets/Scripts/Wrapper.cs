using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Utility
{
    [System.Serializable]
    public class Wrapper<T> where T : class
    {
        [SerializeReference]
        T plainObj;
        [SerializeField]
        UnityEngine.Object uObj;

        public event Action<ValueChanges> EOnValueChanged;

        public T GetValue() {
            if (plainObj != null) {
                return plainObj as T;
            } else {
                return uObj as T;
            }
        }

        public void SetValue(T newVal) {
            var oldVal = GetValue();
            if (oldVal != newVal) {
                if (newVal is UnityEngine.Object unityObject) {
                    uObj = unityObject;
                    plainObj = null;
                } else {
                    plainObj = newVal;
                    uObj = null;
                }
                EOnValueChanged?.Invoke(new ValueChanges(oldVal, newVal));
            }
        }

        public struct ValueChanges
        {
            public T OldValue;
            public T NewValue;

            public ValueChanges(T oldVal, T newVal) {
                OldValue = oldVal;
                NewValue = newVal;
            }

        }

    }

}