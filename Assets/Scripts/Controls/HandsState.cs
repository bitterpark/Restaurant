using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Controls
{
	[CreateAssetMenu(fileName = "Hands State", menuName = "ScriptableObjects/Global State/Hands", order = 0)]
	public class HandsState : ScriptableObject
	{
		public event System.Action EItemChanged;

		public bool CarryingItem => currentItem != null;

		Rigidbody currentItem;

		public void SetItem(Rigidbody item) {
			currentItem = item;
			EItemChanged?.Invoke();
		}

		public Rigidbody GetItem() {
			return currentItem;
		}
	}
}