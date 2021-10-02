using System.Collections;
using UnityEngine;

namespace Assets.Scripts.ViewComps
{
	[RequireComponent(typeof(Rigidbody))]
	public class PickUpableComp : MonoBehaviour
	{
		public static event System.Action<Rigidbody> EPickUpableClicked;
		
		[SerializeField]
		Transform playerHandsPos;
		
		Rigidbody myRB;

		private void Awake() {
			Inventory.InventoryOpener.EInventoryToggled += OnInventoryToggled;
			myRB = GetComponent<Rigidbody>();
		}
		private void OnDestroy() {
			Inventory.InventoryOpener.EInventoryToggled -= OnInventoryToggled;
		}

		void OnInventoryToggled(bool on) {
			enabled = !on;
		}

		bool carried = false;

		private void OnMouseDown() {
			if (enabled) {
				EPickUpableClicked?.Invoke(myRB);
			}
			//if (enabled) {
			//	gameObject.isStatic = false;
			//	myRB.useGravity = false;
			//	carried = true;
			//	myRB.collisionDetectionMode = CollisionDetectionMode.Continuous;
			//}
		}

		//private void FixedUpdate() {
		//	if (carried) {
		//		var directionVector = Vector3.Lerp(playerHandsPos.position, transform.position, 0.9f);
		//		var vel =directionVector - transform.position;
		//		myRB.MovePosition(directionVector);
		//		myRB.rotation = Quaternion.identity;
		//	}
		//}

	}
}