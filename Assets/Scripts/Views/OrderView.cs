using System.Collections;
using UnityEngine;
using TMPro;
using System;

namespace Assets.Scripts.Views
{
	public class OrderView : MonoBehaviour
	{
		public static event Action<OrderFulfilledStruct> EOrderFulfilled;

		[SerializeField]
		ItemData orderedDish;
		[SerializeField]
		TextMeshPro textMesh;

		private void OnEnable() {
			SetOrder(orderedDish);
		}
		private void OnTriggerEnter(Collider other) {
			var broughtBy = other.GetComponent<IHasItemData>();
			var item = broughtBy?.GetItemData();
			if (item != null && item == orderedDish) {
				SetOrder(null);
				EOrderFulfilled?.Invoke(new OrderFulfilledStruct() {Order = this, FulfilledBy = broughtBy });
			} 
		}

		public void SetOrder(ItemData newDish) {
			orderedDish = newDish;
			if (newDish != null) {
				orderedDish = newDish;
				textMesh.text = newDish.displayName;
				textMesh.gameObject.SetActive(true);
			} else {
				textMesh.gameObject.SetActive(false);
			}
		}
	}

	public struct OrderFulfilledStruct
	{
		public OrderView Order;
		public IHasItemData FulfilledBy;
	}

	public interface IHasItemData
	{
		ItemData GetItemData();
	}
}