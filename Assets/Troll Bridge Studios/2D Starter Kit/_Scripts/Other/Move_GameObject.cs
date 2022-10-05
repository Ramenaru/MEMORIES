using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace TrollBridge {

	public class Move_GameObject : MonoBehaviour {

		[Tooltip("The start location of this gameobject.")]
		public Transform StartLocation;
		[Tooltip("The end location of this gameobject.")]
		public Transform EndLocation;

		private Transform _transform;

		void Start(){
			_transform = GetComponent<RectTransform> ();
		}

		public IEnumerator MoveStartEnd(float moveTime){
			// Loop for how ever long moveTime is.
			for(float x = 0f; x < 1.0f; x += Time.deltaTime / moveTime){
				// Move to the EndLocation in a SmoothStep fashion.
				_transform.position = new Vector3 (
					Mathf.SmoothStep(StartLocation.position.x, EndLocation.position.x, x),
					Mathf.SmoothStep(StartLocation.position.y, EndLocation.position.y, x),
					Mathf.SmoothStep(StartLocation.position.z, EndLocation.position.z, x));
				yield return null;
			}
		}

		public void MoveToStart(){
			// Move back to the start.
			_transform.position = StartLocation.position;
		}
	}
}
