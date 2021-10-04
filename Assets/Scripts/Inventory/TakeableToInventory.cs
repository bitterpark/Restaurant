using Assets.Scripts.Wrappers;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using static Assets.Scripts.Inventory.MouseDraggedInventoryItem;

namespace Assets.Scripts.Inventory
{
	public class TakeableToInventory : MonoBehaviour
	{
		[SerializeField]
		MouseDraggedInventoryItem mouseItem;

		[SerializeField]
		IInventoryItemWrapper itemSource;

		private void Awake() {
			InventoryOpener.EInventoryToggled += OnInventoryToggled;
			OnInventoryToggled(InventoryOpener.InventoryOpen);
		}

		private void OnDestroy() {
			InventoryOpener.EInventoryToggled -= OnInventoryToggled;
		}

		void OnInventoryToggled(bool on) {
			enabled = on;
		}

		private void OnMouseDown() {
			var mouseItemVal = mouseItem.GetItem();
			if (enabled && mouseItemVal == null && mouseItem.PosWithinReach(transform.position)) {
				var myValue = itemSource.GetValue();
				myValue.GetGameObject().SetActive(false);
				mouseItem.SetItem(myValue);
			}
		}
	}
}