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
		Dish orderedDish;
		[SerializeField]
		DishView dishView;
	
		private void OnEnable() {
			SetDish(orderedDish);
		}
		private void OnTriggerEnter(Collider other) {
			var broughtBy = other.GetComponent<IHasDish>();
			var dish = broughtBy?.GetDish();
			if (dish != null && dish == orderedDish) {
				SetDish(null);
				EOrderFulfilled?.Invoke(new OrderFulfilledStruct() {Order = this, FulfilledBy = broughtBy });
			} 
		}

		public void SetDish(Dish newDish) {
			orderedDish = newDish;
			dishView.SetDish(newDish);
		}
	}

	public struct OrderFulfilledStruct
	{
		public OrderView Order;
		public IHasDish FulfilledBy;
	}

	public interface IHasDish
	{
		Dish GetDish();
	}
}