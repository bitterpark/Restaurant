using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Assets.Scripts.Utility;
using Assets.Scripts.Wrappers;
using Assets.Scripts.Views;

namespace Assets.Scripts.Inventory
{

	public class ItemView : MonoBehaviour, IPointerDownHandler
	{
		[SerializeField]
		MouseDraggedInventoryItem mouseItem;
		[SerializeField]
		IInventoryItemWrapper startingData;

		[SerializeField]
		Image iconComp;
		[SerializeField]
		TextMeshProUGUI textcomp;

		[SerializeField]
		public Wrapper<IInventoryItem> item;

		IInventoryItem myItem;

		void Awake() {
			SetItem(startingData.GetValue());
		}

		public void SetItem(IInventoryItem newItem) {
			myItem = newItem;
			Sprite setSprite = null;
			string setText = null;
			if (myItem != null) {
				setSprite = myItem.GetSprite();
				setText = myItem.GetName();
			}
			iconComp.sprite = setSprite;
			textcomp.text = setText;
		}

		void IPointerDownHandler.OnPointerDown(PointerEventData eventData) {
			var currentMouseItem = mouseItem.GetItem();
			mouseItem.SetItem(myItem);
			SetItem(currentMouseItem);
		}
	}

	public interface IInventoryItem: IHasItemData
	{
		string GetName();
		Sprite GetSprite();

		GameObject GetGameObject();
	}
}