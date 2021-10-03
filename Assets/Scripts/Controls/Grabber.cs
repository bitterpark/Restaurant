using Assets.Scripts.Controls;
using Assets.Scripts.Utility;
using Assets.Scripts.ViewComps;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
{
	[SerializeField]
    float throwForce = 3f;
    Rigidbody carried = null;

	[SerializeField]
	IInputSourceWrapper inputSource;
	[System.Serializable]
	class IInputSourceWrapper : Wrapper<IInputSource>
	{
	}

	bool waitingOneFrame = false;

    private void OnEnable() {
        PickUpableComp.EPickUpableClicked += OnPickUpableClicked;
	}
	private void OnDisable() {
        PickUpableComp.EPickUpableClicked -= OnPickUpableClicked;
    }


    void OnPickUpableClicked(Rigidbody pickUpable) {
        if (carried == null) {
            SetCarried(pickUpable);
        }
	}

    void FixedUpdate() {
		UpdateCarriedObj();
	}

	private void UpdateCarriedObj() {
		if (carried) {
			var directionVector = Vector3.Lerp(carried.transform.position, transform.position, 0.1f);
			carried.MovePosition(directionVector);
			carried.rotation = Quaternion.identity;
		}
	}

	

	private void UnsetCarried() {
		if (carried != null) {
			carried.useGravity = true;
			carried.collisionDetectionMode = CollisionDetectionMode.Discrete;
			carried = null;
		}
	}


	void Update()
    {
        if (waitingOneFrame) {
            waitingOneFrame = false;
            return;
		}
		var inputVal = inputSource.GetValue();
		if (inputVal.InputEnabled) {
			return;
		}

        if (inputVal.GetMouseButtonDown(0) && carried != null) {
			UnsetCarried();

		}

        if (inputVal.GetMouseButtonDown(1) && carried != null) {
            var thrown = carried;
			UnsetCarried();
			ThrowObj(thrown);
        }

    }

	void SetCarried(Rigidbody newCarried) {
		UnsetCarried();
		carried = newCarried;
		if (carried != null) {
			carried.useGravity = false;
			carried.collisionDetectionMode = CollisionDetectionMode.Continuous;
			//Waiting for a frame is required to ensure the click that set the carried obj to mouse doesn't unset it in the same frame
			waitingOneFrame = true;
		}
	}

	public void ThrowObj(Rigidbody rigidbody) {
        rigidbody.position = transform.position;
        rigidbody.AddForce(transform.TransformDirection(Vector3.forward) * throwForce, ForceMode.Impulse);
    }
}
