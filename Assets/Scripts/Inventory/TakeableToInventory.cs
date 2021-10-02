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
		IInventoryItemWrapper myData;

		private void Awake() {
			enabled = false;
			InventoryOpener.EInventoryToggled += OnInventoryToggled;
		}

		private void OnDestroy() {
			InventoryOpener.EInventoryToggled -= OnInventoryToggled;
		}

		void OnInventoryToggled(bool on) {
			enabled = on;
		}

		private void OnMouseDown() {
			var mouseItemVal = mouseItem.GetItem();
			if (enabled && mouseItemVal == null) {
				var myValue = myData.GetValue();
				myValue.GetGameObject().SetActive(false);
				mouseItem.SetItem(myValue);
			}
		}
	}
}