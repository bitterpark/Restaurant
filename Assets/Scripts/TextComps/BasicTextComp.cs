using System.Collections;
using UnityEngine;
using TMPro;
using Assets.Scripts;

namespace Assets.Scripts.TextComps
{
	[RequireComponent(typeof(TextMeshPro))]
	public class BasicTextComp : MonoBehaviour
	{
		TextMeshPro text;

		private void Awake() {
			text = GetComponent<TextMeshPro>();
		}

		public void SetText(string textToSet) {
			text.text = textToSet;
		}

	}
}