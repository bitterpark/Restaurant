using System.Collections;
using UnityEngine;

namespace Assets.Scripts.UI
{
	public class InventoryHints : MonoBehaviour
	{
		[SerializeField]
		Inventory.InventoryState invState;
		[SerializeField]
		GameObject hintsObj;
		[SerializeField]
		GameObject mosueIsEmptyHint;
		[SerializeField]
		GameObject mosueHasObjHint;

		private void OnEnable() {
			invState.EInventoryToggled += OnInventoryToggled;
			OnInventoryToggled(invState.IsOpen);
		}
		private void OnDisable() {
			invState.EInventoryToggled -= OnInventoryToggled;
		}

		void OnInventoryToggled(bool toggledOn) {
			hintsObj.SetActive(toggledOn);
		}

		private void Update() {
			if (hintsObj.activeSelf) {
				if (invState.MouseHasItem) {
					mosueIsEmptyHint.SetActive(false);
					mosueHasObjHint.SetActive(true);
				} else {
					mosueIsEmptyHint.SetActive(true);
					mosueHasObjHint.SetActive(false);
				}
			}
		}	
	}
}