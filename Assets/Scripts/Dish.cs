using Assets.Scripts.Inventory;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
	[CreateAssetMenu(fileName ="Dish", menuName = "ScriptableObjects/Dish", order = 0)]
	public class Dish : ScriptableObject
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
	}
}