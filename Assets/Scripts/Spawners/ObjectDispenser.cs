using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Spawners
{
	public class ObjectDispenser : MonoBehaviour {
		[SerializeField]
		MeshFilter dispensedObjectPrefab;

		[SerializeField]
		Vector3 collisionBoxBounds;

		[SerializeField]
		LayerMask collisionsLayerMask;

		GameObject currentObject;

		Collider[] overlapCheckBuffer;

		private void Awake() {
			CreateNewObj();
		}

		void CreateNewObj() {
			var newObj = Instantiate(dispensedObjectPrefab, transform.position, Quaternion.identity);
			currentObject = newObj.gameObject;
			StartCoroutine(WaitUnitObjectTaken());
		}
		IEnumerator WaitUnitObjectTaken() {
			while (currentObject != null) {
				var overlapping = OverlappingCollidersExist();
				if (overlapping) {
					yield return new WaitForFixedUpdate();
				} else {
					OnObjectTaken();
					yield break;
				}
			}
		}

		void OnObjectTaken() {
			currentObject = null;
			StartCoroutine(StartSpawningNewObject());
		}
		IEnumerator StartSpawningNewObject() {
			while (currentObject == null) {
				var overlapping = OverlappingCollidersExist();
				if (overlapping) {
					yield return new WaitForFixedUpdate();
				} else {
					CreateNewObj();
					yield break;
				}
			}
		}

		bool OverlappingCollidersExist() {
			if (overlapCheckBuffer == null) {
				overlapCheckBuffer = new Collider[1];
			}
			var pos = transform.position;
			var bounds = GetBounds();
			pos.y += bounds.y / 2;
			int collidersCount = Physics.OverlapBoxNonAlloc(pos,
				bounds / 2, 
				overlapCheckBuffer,
				Quaternion.identity
				);
			return collidersCount > 0;
		}

		//Draw the Box Overlap as a gizmo to show where it currently is testing. Click the Gizmos button to see this
		void OnDrawGizmos() {
			Gizmos.color = Color.blue;
			if (OverlappingCollidersExist()) {
				Gizmos.color = Color.red;
			}

			if (dispensedObjectPrefab == null) {
				return;
			} else {
				var pos = transform.position;
				var boundsSize = GetBounds();
				pos.y += boundsSize.y / 2;
				Gizmos.DrawWireCube(pos, boundsSize);
			}
		}

		Vector3 GetBounds() {
			return dispensedObjectPrefab.sharedMesh.bounds.size;
		}

	}
}