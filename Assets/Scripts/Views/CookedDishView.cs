using Assets.Scripts.Inventory;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Views
{
	public class CookedDishView : MonoBehaviour, IHasDish, IInventoryItem
	{
		[SerializeField]
		Dish cookedDish;
		[SerializeField]
		DishView dishView;

		void OnEnable() {
			dishView.SetDish(cookedDish);
			OrderView.EOrderFulfilled += OnOrderFulfilled;
		}
		private void OnDisable() {
			OrderView.EOrderFulfilled -= OnOrderFulfilled;
		}

		void OnOrderFulfilled(OrderFulfilledStruct args) {
			if (args.FulfilledBy == this) {
				Debug.Log("DESTROYING");
				Destroy(gameObject);
			}
		}

		Dish IHasDish.GetDish() {
			return cookedDish;
		}

		public string GetName() {
			return cookedDish.GetName();
		}

		public Sprite GetSprite() {
			return cookedDish.GetSprite();
		}

		GameObject IInventoryItem.GetGameObject() {
			return gameObject;
		}
	}
}