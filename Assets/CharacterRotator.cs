using Assets.Scripts.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRotator : MonoBehaviour
{
    [SerializeField]
    float turnSpeed = 1f;

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
        var input = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up * input * turnSpeed);
    }
}
