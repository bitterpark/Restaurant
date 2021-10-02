using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Assets.Scripts.Utility;

namespace Assets.Scripts.Inventory
{

	public class ItemView : MonoBehaviour, IPointerUpHandler, IPointerClickHandler
	{
		[SerializeField]
		MouseDraggedInventoryItem mouseItem;
		[SerializeField]
		MouseDraggedInventoryItem.ItemData startingData;

		[SerializeField]
		Image iconComp;
		[SerializeField]
		TextMeshProUGUI textcomp;

		[SerializeField]
		public Wrapper<IInventoryItem> item;

		MouseDraggedInventoryItem.ItemData? myItem;

		void OnEnable() {
			SetItem(startingData);
		}

		public void SetItem(MouseDraggedInventoryItem.ItemData? newItem) {
			myItem = newItem;
			Sprite setSprite = null;
			string setText = null;
			if (myItem.HasValue) {
				var itemVal = myItem.Value.item.GetValue();
				setSprite = itemVal.GetSprite();
				setText = itemVal.GetName();
			}
			iconComp.sprite = setSprite;
			textcomp.text = setText;
		}

		void IPointerClickHandler.OnPointerClick(PointerEventData eventData) {
			if (mouseItem.GetItem() == null) {
				mouseItem.SetItem(myItem);
				SetItem(null);
			}
		}

		void IPointerUpHandler.OnPointerUp(PointerEventData eventData) {
			var droppedItem = mouseItem.GetItem();
			if (droppedItem != null) {
				var myOriginalItem = myItem;
				SetItem(droppedItem);
				mouseItem.SetItem(myOriginalItem);
			}
		}
	}

	public interface IInventoryItem
	{
		string GetName();
		Sprite GetSprite();

		GameObject GetGameObject();
	}
}