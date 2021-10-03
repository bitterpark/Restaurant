using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Inventory
{
	[RequireComponent(typeof(Image))]
	public class ThrowItemArea : MonoBehaviour, IPointerDownHandler
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
			StartCoroutine(ChangeStateAfterOneFrame());
		}

		IEnumerator ChangeStateAfterOneFrame() {
			yield return new WaitForEndOfFrame();
			UpdateActiveState();
		}
		
		void UpdateActiveState() {
			if (mouseItem.GetItem() != null) {
				enabled = true;
			} else {
				enabled = false;
			}
		}

		void IPointerDownHandler.OnPointerDown(PointerEventData eventData){
			Debug.Log("Throw item area clicked");
			var mouseCarried = mouseItem.GetItem();
			if (enabled && mouseCarried != null) {
				ThrowItem(mouseCarried.GetGameObject());
				mouseItem.SetItem(null);
			}
		}

		void ThrowItem(GameObject itemView) {
			itemView.gameObject.SetActive(true);
			var rb = itemView.GetComponent<Rigidbody>();
			grabber.ThrowObj(rb);
		}
	}
}