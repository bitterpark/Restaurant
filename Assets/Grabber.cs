using Assets.Scripts.ViewComps;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
{
	[SerializeField]
    float throwForce = 3f;
    Rigidbody carried = null;

    bool waitingForCarried = false;

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

	void SetCarried(Rigidbody newCarried) {
        if (carried != null) {
            carried.useGravity = true;
            carried.collisionDetectionMode = CollisionDetectionMode.Discrete;
		}

        carried = newCarried;
        if (carried != null) {
            carried.useGravity = false;
            carried.collisionDetectionMode = CollisionDetectionMode.Continuous;
            waitingForCarried = true;
        }
    }

    

	void Update()
    {
        if (waitingForCarried) {
            waitingForCarried = false;
            return;
		}
        if (Input.GetMouseButtonDown(0) && carried != null) {
            SetCarried(null);
        }

        if (Input.GetMouseButtonDown(1) && carried != null) {
            var thrown = carried;
            SetCarried(null);
            ThrowObj(thrown);
        }
    }

    public void ThrowObj(Rigidbody rigidbody) {
        rigidbody.position = transform.position;
        rigidbody.AddForce(transform.TransformDirection(Vector3.forward) * throwForce, ForceMode.Impulse);
    }
}
