using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Inventory
{
	[CreateAssetMenu(fileName = "Inventory State", menuName = "ScriptableObjects/Global State/Inventory", order = 0)]
	public class InventoryState : ScriptableObject
	{
		[SerializeField]
		MouseDraggedInventoryItem draggedInvItem;

		public bool MouseHasItem => draggedInvItem?.GetItem() != null;
		public MouseDraggedInventoryItem CurrentMouseItem => draggedInvItem;

		public bool IsOpen {
			get { return isOpen; }
			set {
				if (isOpen != value) {
					isOpen = value;
					EInventoryToggled?.Invoke(isOpen);
				}
			}
		}
		bool isOpen;

		private void OnEnable() {
			isOpen = false;
		}

		public event System.Action<bool> EInventoryToggled;
	}
}