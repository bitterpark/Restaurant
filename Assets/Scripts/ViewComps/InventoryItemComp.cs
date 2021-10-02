using Assets.Scripts.Inventory;
using System.Collections;
using UnityEngine;
using static Assets.Scripts.Inventory.MouseDraggedInventoryItem;

namespace Assets.Scripts.ViewComps
{
	public class InventoryItemComp : MonoBehaviour, IInventoryItem
	{
		[SerializeField]
		string invName;
		[SerializeField]
		Sprite icon;

		GameObject IInventoryItem.GetGameObject() {
			return gameObject;
		}

		string IInventoryItem.GetName() {
			return invName;
		}

		Sprite IInventoryItem.GetSprite() {
			return icon;
		}
	}
}