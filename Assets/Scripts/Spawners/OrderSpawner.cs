using System.Collections;
using UnityEngine;
using Assets.Scripts.Views;
using System.Collections.Generic;

namespace Assets.Scripts.Spawners
{
	public class OrderSpawner : MonoBehaviour
	{
		[SerializeField]
		OrderView orderView;
		[SerializeField]
		List<Dish> orderableDishes;

		[SerializeField]
		float timeToSpawnOrder = 1f;

		private void OnEnable() {
			SpawnNewOrder();
			OrderView.EOrderFulfilled += OnOrderFulfilled;
		}
		private void OnDisable() {
			OrderView.EOrderFulfilled -= OnOrderFulfilled;
		}

		void OnOrderFulfilled(OrderFulfilledStruct args) {
			if (args.Order == this.orderView) {
				StartCoroutine(SpawnOrderOnTimer());
			}
		}

		IEnumerator SpawnOrderOnTimer() {
			yield return new WaitForSeconds(timeToSpawnOrder);
			SpawnNewOrder();	
		}

		void SpawnNewOrder() {
			var randomIndex = Random.Range(0, orderableDishes.Count);
			if (orderableDishes.Count == 0) {
				throw new System.Exception($"{nameof(orderableDishes)} cannot be empty, add at least one dish!");
			}
			var randomOrder = orderableDishes[randomIndex];
			orderView.SetDish(randomOrder);
		}

	}
}