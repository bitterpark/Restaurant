using System.Collections;
using UnityEngine;

namespace Assets.Scripts.UI
{
	[CreateAssetMenu(fileName = "Main Menu State", menuName = "ScriptableObjects/Global State/Main Menu", order = 0)]
	public class MainMenuState : ScriptableObject
	{
		public bool IsOpen { 
			get=> menuOpen;
			set {
				if (menuOpen != value) {
					menuOpen = value;
					EMenuToggled?.Invoke(menuOpen);
				}
			}
		}
		bool menuOpen;

		private void OnEnable() {
			menuOpen = false;
		}

		public event System.Action<bool> EMenuToggled;
	}
}