using Assets.Scripts.Controls;
using Assets.Scripts.Inventory;
using Assets.Scripts.Utility;
using Assets.Scripts.ViewComps;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Scripts.Controls
{
	public class Grabber : MonoBehaviour
	{
		[SerializeField]
		float throwForce = 3f;

		[SerializeField]
		IInputSourceWrapper inputSource;

		[SerializeField]
		float maxPickupDist = 1f;

		const float CARRIED_DRAG_VALUE = 10f;

		[System.Serializable]
		class IInputSourceWrapper : Wrapper<IInputSource>
		{
		}

		[SerializeField]
		HandsState handsState;
		[SerializeField]
		InventoryState invState;

		bool waitingOneFrame = false;

		private void OnEnable() {
			PickUpableComp.EPickUpableClicked += OnPickUpableClicked;
			invState.EInventoryToggled += OnInventoryToggled;
		}
		private void OnDisable() {
			PickUpableComp.EPickUpableClicked -= OnPickUpableClicked;
			invState.EInventoryToggled -= OnInventoryToggled;
		}

		void OnInventoryToggled(bool on) {
			if (on && handsState.CarryingItem) {
				UnsetCarried();
			} else if (!on && invState.MouseHasItem) {
				var mouseItem = invState.CurrentMouseItem;
				var gameObj = mouseItem.GetItem().GetGameObject();
				gameObj.SetActive(true);
				ThrowObj(gameObj.GetComponent<Rigidbody>());
				mouseItem.SetItem(null);
			}
		}

		void OnPickUpableClicked(Rigidbody pickUpable) {
			if (!handsState.CarryingItem && (transform.position - pickUpable.position).magnitude < maxPickupDist) {
				SetCarried(pickUpable);
			}
		}

		void SetCarried(Rigidbody newCarried) {
			UnsetCarried();
			handsState.SetItem(newCarried);
			var carried = newCarried;
			if (carried != null) {
				carried.rotation = Quaternion.Euler(new Vector3(0, carried.rotation.eulerAngles.y, 0));
				carried.transform.SetParent(null, true);
				carried.useGravity = false;
				carried.collisionDetectionMode = CollisionDetectionMode.Continuous;
				carried.interpolation = RigidbodyInterpolation.Interpolate;
				carried.drag = CARRIED_DRAG_VALUE;
				carried.angularDrag = CARRIED_DRAG_VALUE;
				//Waiting for a frame is required to ensure the click that set the carried obj to mouse doesn't unset it in the same frame
				waitingOneFrame = true;
			}
		}

		private void UpdateCarriedObjPos() {
			if (handsState.CarryingItem) {
				var carried = handsState.GetItem();
				carried.MovePosition(transform.position);
				var wheelVal = inputSource.GetValue().GetAxis("Mouse ScrollWheel", false);
				if (wheelVal != 0) {
					var rotationDelta = Quaternion.Euler(new Vector3(0, wheelVal * 180, 0));
					carried.MoveRotation(carried.rotation * rotationDelta);
				}
			}
		}

		void Update() {
			if (waitingOneFrame) {
				waitingOneFrame = false;
				return;
			}
			var inputVal = inputSource.GetValue();
			if (!inputVal.InputEnabled) {
				return;
			}

			bool carryingItem = handsState.CarryingItem;

			if (inputVal.GetMouseButtonDown(0) && carryingItem) {
				UnsetCarried();

			}

			if (inputVal.GetMouseButtonDown(1) && carryingItem) {
				var thrown = handsState.GetItem();
				UnsetCarried();
				ThrowObj(thrown);
			}
			UpdateCarriedObjPos();

		}

		private void UnsetCarried() {
			var carried = handsState.GetItem();
			if (carried != null) {
				carried.useGravity = true;
				carried.isKinematic = false;
				carried.drag = 0;
				carried.angularDrag = 0;
				carried.collisionDetectionMode = CollisionDetectionMode.Discrete;
				carried.interpolation = RigidbodyInterpolation.None;
				handsState.SetItem(null);
			}
		}

		public void ThrowObj(Rigidbody rigidbody) {
			rigidbody.position = transform.position;
			rigidbody.AddForce(transform.TransformDirection(Vector3.forward) * throwForce, ForceMode.Impulse);
		}
	}
}
