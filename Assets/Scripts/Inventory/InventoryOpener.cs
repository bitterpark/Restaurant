using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Inventory
{
	public class InventoryOpener : MonoBehaviour
	{
		public static event System.Action<bool> EInventoryToggled;

		public static bool InventoryOpen = false;

		[SerializeField]
		InventoryState state;

		[SerializeField]
		GameObject inventory;

		private void Update() {
			if (Input.GetKeyDown(KeyCode.Tab)) {
				ToggleInventory();
			}
		}

		private void ToggleInventory() {
			bool setActive = !inventory.activeSelf;
			inventory.SetActive(setActive);
			InventoryOpen = setActive;
			state.IsOpen = setActive;
			EInventoryToggled?.Invoke(setActive);
		}
	}
}