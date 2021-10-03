using System.Collections;
using UnityEngine;

namespace Assets.Scripts.ViewComps
{
	[RequireComponent(typeof(Rigidbody))]
	public class PickUpableComp : MonoBehaviour
	{
		public static event System.Action<Rigidbody> EPickUpableClicked;
		
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

		private void OnMouseDown() {
			if (enabled) {
				EPickUpableClicked?.Invoke(myRB);
			}
		}

	}
}