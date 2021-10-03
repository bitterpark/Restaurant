using Assets.Scripts.Controls;
using Assets.Scripts.Inventory;
using Assets.Scripts.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraVerticalAngler : MonoBehaviour
{
    private float currentPitch = 0f;
    public float minPitch = -80f;
    public float maxPitch = 80f;
    public float turnSpeed = 1f;

    [SerializeField]
    IInputSourceWrapper inputSource;

    [System.Serializable]
    class IInputSourceWrapper : Wrapper<IInputSource>
    {
    }

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
        var inputSourceVal = inputSource.GetValue();
        if (inputSourceVal.InputEnabled) {
            var input = inputSourceVal.GetMouseY(false);
            currentPitch -= input * turnSpeed;
            currentPitch = Mathf.Clamp(currentPitch, minPitch, maxPitch);
            transform.localEulerAngles = Vector3.right * currentPitch;
        }
    }
}
