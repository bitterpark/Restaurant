using Assets.Scripts.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
	public class MainMenu : MonoBehaviour
	{
		[SerializeField]
		Button playButton;
		[SerializeField]
		Button quitButton;
		[SerializeField]
		GameObject menuObj;
		[SerializeField]
		MainMenuState state;

		private void Awake() {
			playButton.onClick.AddListener(PlayPressed);
			quitButton.onClick.AddListener(QuitPressed);
			SetState(true);
		}

		private void Update() {
			if (Input.GetKeyDown(KeyCode.Escape)) {
				SetState(!state.IsOpen);
			}
		}
		void PlayPressed() {
			SetState(false);
		}

		private void SetState(bool active) {
			state.IsOpen = active;
			menuObj.SetActive(active);
		}

		

		void QuitPressed() {
			Application.Quit();
		}

	}
}