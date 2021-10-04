using Assets.Scripts.Inventory;
using Assets.Scripts.Views;
using Assets.Scripts.Wrappers;
using System.Collections;
using UnityEngine;
using static Assets.Scripts.Inventory.MouseDraggedInventoryItem;

namespace Assets.Scripts.ViewComps
{
	public class ItemContainerComp : MonoBehaviour, IInventoryItem
	{
		[SerializeField]
		IHasItemDataWrapper itemDataSource;

		GameObject IInventoryItem.GetGameObject() {
			return gameObject;
		}

		public ItemData GetItemData() {
			return itemDataSource?.GetValue()?.GetItemData();
		}

		string IInventoryItem.GetName() {
			return GetItemData().GetName();
		}

		Sprite IInventoryItem.GetSprite() {
			return GetItemData().GetSprite();
		}
	}
}