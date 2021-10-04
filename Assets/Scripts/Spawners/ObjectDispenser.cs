using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Spawners
{
	public class ObjectDispenser : MonoBehaviour, ISpawner
	{
		[SerializeField]
		MeshFilter dispensedObjectPrefab;

		[SerializeField]
		Vector3 collisionBoxBounds;

		[SerializeField]
		LayerMask collisionsLayerMask;

		Collider[] overlapCheckBuffer;

		[SerializeField]
		bool spawnOnce;

		public void SpawnObj(MeshFilter prefab, bool spawnOnce) {
			dispensedObjectPrefab = prefab;
			this.spawnOnce = spawnOnce;
			if (!gameObject.activeInHierarchy) {
				gameObject.SetActive(true);
			} else {
				StartCoroutine(StartSpawningNewObject());
			}
		}

		private void OnEnable() {
			StartCoroutine(StartSpawningNewObject());
		}
	
		IEnumerator StartSpawningNewObject() {
			while (OverlappingCollidersExist()) {
				yield return new WaitForFixedUpdate();
			}
			CreateNewObj();
		}
		void CreateNewObj() {
			Instantiate(dispensedObjectPrefab, transform.position, Quaternion.identity);
			if (!spawnOnce) {
				StartCoroutine(StartSpawningNewObject());
			}
		}

		bool OverlappingCollidersExist() {
			if (overlapCheckBuffer == null) {
				overlapCheckBuffer = new Collider[1];
			}
			var pos = transform.position;
			var bounds = GetBounds();
			//pos.y += bounds.y / 2;
			int collidersCount = Physics.OverlapBoxNonAlloc(pos,
				bounds / 2, 
				overlapCheckBuffer,
				Quaternion.identity
				);
			return collidersCount > 0;
		}

		void OnDrawGizmos() {
			Gizmos.color = Color.blue;
			if (OverlappingCollidersExist()) {
				Gizmos.color = Color.red;
			}

			if (dispensedObjectPrefab == null) {
				return;
			} else {
				var boundsSize = GetBounds();
				Gizmos.matrix = transform.localToWorldMatrix;
				Gizmos.DrawWireCube(Vector3.zero, boundsSize);
			}
		}

		Vector3 GetBounds() {
			return dispensedObjectPrefab.sharedMesh.bounds.size;
		}

		
	}

	public interface ISpawner
	{
		void SpawnObj(MeshFilter prefab, bool spawnOnce);
	}
}