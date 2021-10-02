using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Inventory
{
	public class InventoryOpener : MonoBehaviour
	{
		public static event System.Action<bool> EInventoryToggled;
		
		[SerializeField]
		GameObject inventory;

		private void Update() {
			if (Input.GetKeyDown(KeyCode.Tab)) {
				bool setActive = !inventory.activeSelf;
				inventory.SetActive(setActive);
				EInventoryToggled?.Invoke(setActive);
			}
		}
	}
}