using Assets.Scripts.Inventory;
using Assets.Scripts.Utility;
using System.Collections;
using UnityEngine;


namespace Assets.Scripts.Inventory
{
	[CreateAssetMenu(fileName ="MouseDraggedInvItem", menuName ="ScriptableObjects/MouseDraggedInvItem", order = 0)]
	public class MouseDraggedInventoryItem : ScriptableObject {
		
		public event System.Action EItemChanged;
		
		ItemData? currentItem;

		public void SetItem(ItemData? item) {
			currentItem = item;
			EItemChanged?.Invoke();
		}

		public ItemData? GetItem() {
			return currentItem;
		}

		[System.Serializable]
		public struct ItemData {	
			public IInventoryItemWrapper item;
		}

		[System.Serializable]
		public class IInventoryItemWrapper : Wrapper<IInventoryItem> { }

	}

	
}