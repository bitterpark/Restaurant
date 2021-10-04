using Assets.Scripts.Utility;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Controls
{
	public class CursorController : MonoBehaviour
	{

		[SerializeField]
		IInputSourceWrapper inputSource;
		[System.Serializable]
		class IInputSourceWrapper : Wrapper<IInputSource>
		{
		}

		private void Update() {
			if (inputSource.GetValue().InputEnabled) {
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
			} else {
				Cursor.lockState = CursorLockMode.Confined;
				Cursor.visible = true;
			}
		}

	}
}