using Assets.Scripts.Inventory;
using Assets.Scripts.Utility;
using Assets.Scripts.Wrappers;
using System.Collections;
using UnityEngine;


namespace Assets.Scripts.Inventory
{
	[CreateAssetMenu(fileName ="MouseDraggedInvItem", menuName ="ScriptableObjects/MouseDraggedInvItem", order = 0)]
	public class MouseDraggedInventoryItem : ScriptableObject {
		
		public event System.Action EItemChanged;

		[SerializeField]
		float maxGrabDistance = 1f;

		IInventoryItemWrapper currentItem = new IInventoryItemWrapper();
		Transform playerTransform;

		public void SetPlayerTransform(Transform player) {
			playerTransform = player;
		}

		public bool PosWithinReach(Vector3 pos) {
			if (playerTransform == null) {
				return false;
			}
			return (playerTransform.position - pos).magnitude < maxGrabDistance;
		}

		public void SetItem(IInventoryItem item) {
			currentItem.SetValue(item);
			EItemChanged?.Invoke();
		}

		public IInventoryItem GetItem() {
			return currentItem.GetValue();
		}
	}
}