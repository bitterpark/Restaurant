using Assets.Scripts.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraVerticalAngler : MonoBehaviour
{
    private float currentPitch = 0f;
    public float minPitch = -80f;
    public float maxPitch = 80f;
    public float turnSpeed = 1f;

	private void Awake() {
        InventoryOpener.EInventoryToggled += OnInventoryToggled;
    }
	private void OnDestroy() {
        InventoryOpener.EInventoryToggled -= OnInventoryToggled;
    }


	void OnInventoryToggled(bool toggledOn) {
        enabled = !toggledOn;
	}

	private void Update() {
        var input = Input.GetAxis("Mouse Y");
        currentPitch -= input * turnSpeed;
        currentPitch = Mathf.Clamp(currentPitch, minPitch, maxPitch);
        transform.localEulerAngles = Vector3.right * currentPitch;
    }
}
