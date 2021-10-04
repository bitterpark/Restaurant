using Assets.Scripts.Inventory;
using Assets.Scripts.Views;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
	[UnityEngine.Scripting.APIUpdating.MovedFrom(true, sourceClassName: "Dish")]
	[CreateAssetMenu(fileName ="Item Data", menuName = "ScriptableObjects/Item Data", order = 0)]
	public class ItemData : ScriptableObject, IHasItemData
	{
		[SerializeField]
		Sprite icon;

		[SerializeField]
		public string displayName;

		public string GetName() {
			return displayName;
		}

		public Sprite GetSprite() {
			return icon;
		}

		ItemData IHasItemData.GetItemData() {
			return this;
		}
	}
}