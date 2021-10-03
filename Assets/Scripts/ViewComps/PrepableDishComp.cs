using Assets.Scripts.Views;
using System.Collections;
using UnityEngine;
namespace Assets.Scripts.ViewComps
{
	[RequireComponent(typeof(MeshFilter))]
	public class PrepableDishComp : MonoBehaviour, Views.PrepStation.IPrepable
	{
		[SerializeField]
		Dish dish;

		MeshFilter PrepStation.IPrepable.GetMesh() {
			return GetComponent<MeshFilter>();
		}

		string PrepStation.IPrepable.GetName() {
			return dish.GetName();
		}
	}
}