using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Inventory
{
	[RequireComponent(typeof(Image))]
	public class ThrowItemArea : MonoBehaviour, IPointerClickHandler
	{
		[SerializeField]
		MouseDraggedInventoryItem mouseItem;

		[SerializeField]
		Grabber grabber;

		void Awake() {
			mouseItem.EItemChanged += OnMouseItemChanged;
			OnMouseItemChanged();
		}

		void OnDestroy() {
			mouseItem.EItemChanged -= OnMouseItemChanged;
		}
	
		void OnMouseItemChanged() {
			if (mouseItem.GetItem() != null) {
				gameObject.SetActive(true);
			} else {
				gameObject.SetActive(false);
			}
		}

		void IPointerClickHandler.OnPointerClick(PointerEventData eventData){
			Debug.Log("CLICKED!");
			var mouseCarried = mouseItem.GetItem();
			if (mouseCarried != null) {
				ThrowMousedItem(mouseCarried.GetGameObject());
				mouseItem.SetItem(null);
			}
		}

		void ThrowMousedItem(GameObject itemView) {
			itemView.gameObject.SetActive(true);
			var rb = itemView.GetComponent<Rigidbody>();
			grabber.ThrowObj(rb);
		}
	}
}