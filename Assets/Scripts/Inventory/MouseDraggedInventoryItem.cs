using Assets.Scripts.Inventory;
using Assets.Scripts.Utility;
using System.Collections;
using UnityEngine;


namespace Assets.Scripts.Inventory
{
	[CreateAssetMenu(fileName ="MouseDraggedInvItem", menuName ="ScriptableObjects/MouseDraggedInvItem", order = 0)]
	public class MouseDraggedInventoryItem : ScriptableObject {
		
		public event System.Action EItemChanged;

		IInventoryItemWrapper currentItem;

		public void SetItem(IInventoryItem item) {
			currentItem.SetValue(item);
			EItemChanged?.Invoke();
		}

		public IInventoryItem GetItem() {
			return currentItem.GetValue();
		}
	}

	[System.Serializable]
	public class IInventoryItemWrapper : Wrapper<IInventoryItem> { }
}