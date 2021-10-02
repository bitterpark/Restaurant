using Assets.Scripts.Inventory;
using Assets.Scripts.Utility;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Views
{
	public class DishView : MonoBehaviour
	{
		[SerializeField]
		Dish viewedDish;
		[SerializeField]
		TextMeshPro textMesh;

		private void OnEnable() {
			SetDish(viewedDish);
		}

		public void SetDish(Dish newDish) {
			if (newDish != null) {
				viewedDish = newDish;
				textMesh.text = viewedDish.displayName;
				textMesh.gameObject.SetActive(true);
			} else {
				textMesh.gameObject.SetActive(false);
			}
		}
	}
}
[System.Serializable]
public class Wropper
{


}