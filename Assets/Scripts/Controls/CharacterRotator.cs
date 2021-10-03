using Assets.Scripts.Controls;
using Assets.Scripts.Inventory;
using Assets.Scripts.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRotator : MonoBehaviour
{
    [SerializeField]
    float turnSpeed = 1f;

    [SerializeField]
    IInputSourceWrapper inputSource;
    [System.Serializable]
    class IInputSourceWrapper: Wrapper<IInputSource> {
    }

    private void Update() {
        if (inputSource.GetValue().InputEnabled) {
            var input = inputSource.GetValue().GetMouseX(false);
            transform.Rotate(Vector3.up * input * turnSpeed);
        }
    }
}
