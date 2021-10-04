using Assets.Scripts.Inventory;
using Assets.Scripts.Utility;
using Assets.Scripts.Wrappers;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Views
{
	public class DishView : MonoBehaviour, IHasItemData
	{
		[SerializeField]
		IHasItemDataWrapper itemSource;
		[SerializeField]
		TextMeshPro textMesh;

		void OnEnable() {
			SetDish(itemSource.GetValue().GetItemData());
			OrderView.EOrderFulfilled += OnOrderFulfilled;
		}
		private void OnDisable() {
			OrderView.EOrderFulfilled -= OnOrderFulfilled;
		}

		void SetDish(ItemData newDish) {
			if (textMesh != null) {
				if (newDish != null) {
					textMesh.text = newDish.displayName;
					textMesh.gameObject.SetActive(true);
				} else {
					textMesh.gameObject.SetActive(false);
				}
			}
		}


		void OnOrderFulfilled(OrderFulfilledStruct args) {
			if (args.FulfilledBy == itemSource.GetValue()) {
				Destroy(gameObject);
			}
		}

		ItemData IHasItemData.GetItemData() {
			return itemSource.GetValue().GetItemData();
		}
	}
}