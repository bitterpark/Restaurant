using Assets.Scripts.Inventory;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
	[RequireComponent(typeof(Image))]
	[RequireComponent(typeof(RectTransform))]
	public class MouseWidget : MonoBehaviour
	{
		[SerializeField]
		MouseDraggedInventoryItem mouseItem;

		Image imageComp;
		RectTransform rectTransform;

		private void Awake() {
			imageComp = GetComponent<Image>();
			rectTransform = GetComponent<RectTransform>();
		}

		void OnSet(IInventoryItem item) {
			if (item != null) {
				imageComp.enabled = true;
				imageComp.sprite = item.GetSprite();
			} else {
				imageComp.enabled = false;
			}
		}

		private void Update() {
			var currentItem = mouseItem.GetItem();
			if (currentItem != null) {
				OnSet(currentItem);
			} else {
				OnSet(null);
			}
			rectTransform.anchoredPosition = Input.mousePosition;
		}

	}
}