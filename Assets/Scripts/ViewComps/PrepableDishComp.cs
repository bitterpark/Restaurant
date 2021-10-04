using Assets.Scripts.Views;
using Assets.Scripts.Wrappers;
using System.Collections;
using UnityEngine;
namespace Assets.Scripts.ViewComps
{
	[RequireComponent(typeof(MeshFilter))]
	public class PrepableDishComp : MonoBehaviour, Views.PrepStation.IPrepable
	{
		[SerializeField]
		IHasItemDataWrapper dishSource;

		MeshFilter PrepStation.IPrepable.GetMesh() {
			return GetComponent<MeshFilter>();
		}

		string PrepStation.IPrepable.GetName() {
			return dishSource?.GetValue()?.GetItemData()?.GetName();
		}
	}
}