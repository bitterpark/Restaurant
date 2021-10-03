using System.Collections;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Text;
using Assets.Scripts.Utility;
using Assets.Scripts.Spawners;

namespace Assets.Scripts.Views
{
	[RequireComponent(typeof(Collider))]
	public class PrepStation : MonoBehaviour {
		[SerializeField]
		WrapperIPrepable spawnedPrepable;
		[System.Serializable]
		class WrapperIPrepable : Wrapper<IPrepable> { }

		[SerializeField]
		TextMeshProUGUI mainLabelText;
		[SerializeField]
		TextMeshProUGUI ingredientsText;

		[SerializeField]
		List<Dish> ingredientsRequired = new List<Dish>();

		[SerializeField]
		WrapperISpawner spawner;
		[System.Serializable]
		class WrapperISpawner : Wrapper<ISpawner> {
		}

		List<Dish> ingredientsMissing = new List<Dish>();

		private void Awake() {
			SetupForNextDish();
		}

		private void OnTriggerEnter(Collider other) {
			var broughtBy = other.GetComponent<IHasDish>();
			var dish = broughtBy?.GetDish();
			if (dish != null && ingredientsMissing.Contains(dish)) {
				IngredientBrought(dish);
				Destroy(other.gameObject);
			}
		}

		private void IngredientBrought(Dish dish) {
			ingredientsMissing.Remove(dish);
			UpdateText();
			if (ingredientsMissing.Count == 0) {
				SpawnDish();
				SetupForNextDish();
			}
		}

		void SpawnDish() {
			var spawnerVal = spawner.GetValue();
			var dishMesh = spawnedPrepable.GetValue().GetMesh();
			spawnerVal.SpawnObj(dishMesh, true);
		}

		void SetupForNextDish() {
			ingredientsMissing.AddRange(ingredientsRequired);
			UpdateLabel();
			UpdateText();
		}

		void UpdateLabel() {
			mainLabelText.text = $"Prep {spawnedPrepable.GetValue().GetName()}";
		}
		void UpdateText() {
			var ingredientsString = new StringBuilder("Requires:\n");
			foreach (var ingredient in ingredientsRequired) {
				if (ingredientsMissing.Contains(ingredient)) {
					ingredientsString.Append($"{ingredient.GetName()}\n");
				}
			}
			ingredientsText.text = ingredientsString.ToString();
		}
		

		public interface IPrepable {
			MeshFilter GetMesh();
			string GetName();
		}
	}
}