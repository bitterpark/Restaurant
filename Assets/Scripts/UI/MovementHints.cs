using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
	public class MovementHints : MonoBehaviour
	{
		[SerializeField]
		Inventory.InventoryState invState;
		[SerializeField]
		Controls.HandsState handsState;
		[SerializeField]
		GameObject hintsObj;
		[SerializeField]
		GameObject handsEmptyObj;
		[SerializeField]
		GameObject handsFullObj;

		private void OnEnable() {
			invState.EInventoryToggled += OnInventoryToggled;
			OnInventoryToggled(invState.IsOpen);
		}
		private void OnDisable() {
			invState.EInventoryToggled -= OnInventoryToggled;
		}

		private void Update() {
			if (hintsObj.activeSelf) {
				if (handsState.CarryingItem) {
					handsEmptyObj.SetActive(false);
					handsFullObj.SetActive(true);
				} else {
					handsEmptyObj.SetActive(true);
					handsFullObj.SetActive(false);
				}
			}
		}

		void OnInventoryToggled(bool toggledOn) {
			hintsObj.SetActive(!toggledOn);
		}

	}
}