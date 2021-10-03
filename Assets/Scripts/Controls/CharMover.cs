using Assets.Scripts.Controls;
using Assets.Scripts.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMover : MonoBehaviour
{
    [SerializeField]
    float playerSpeed;
    [SerializeField]
    float gravity = 1f;

    [SerializeField]
    IInputSourceWrapper inputSource;
    [System.Serializable]
    class IInputSourceWrapper : Wrapper<IInputSource>
    {
    }
    CharacterController controller;

    // Start is called before the first frame update
    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

	private void Update() {
        var inputSourceVal = inputSource.GetValue();
        if (inputSourceVal.InputEnabled) {
            var direction = new Vector3(inputSourceVal.GetHorizontalAxis(true), 0, inputSourceVal.GetVerticalAxis(true)).normalized;
            DoMove(direction);
            ApplyPlayerGravity();
        }
	}

    void DoMove(Vector3 direction) {
        var moveVect = transform.TransformDirection(direction) * playerSpeed * Time.deltaTime;
        controller.Move(moveVect);
    }
    private void ApplyPlayerGravity() {
		controller.Move(-transform.up * gravity);
	}
}
