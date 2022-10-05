using UnityEngine;
using System.Collections;

namespace TrollBridge {
	
	public class Point_To_Point_Movement : MonoBehaviour {

		[Header("Point To Point Locations")]
		[Tooltip("Do you want the NPC to go to the first location after reaching the last location?")]
		public bool loop;
		[Tooltip("The order in which the GameObject will be moving towards, once the end is reached it will traverse backwards through the array unless Loop is set to true then it will go to the first location once it reaches the end.")]
		public Transform[] Locations;
		[Header("Object Speed")]
		[Tooltip("The speed at which the GameObject will be moving.  IF there is a Character script attached to this GameObject then this variable 'Speed' will be changed to the Character Component 'CurrentMoveSpeed'")]
		public float Speed = 1f;
		[Header("Pause Time")]
		[Tooltip("The minimum time the GameObject waits before going to the next 'Location'.")]
		public float WaitTimeMin = 3f;
		[Tooltip("The maximum time the GameObject waits before going to the next 'Location'.")]
		public float WaitTimeMax = 3f;

		private int _locationIndex;
		private Transform _transform;
		private bool forward = true;
		private Character _character;
		private Character_Stats charStats;


		void Awake(){
			// Check to make sure the user has the scripts working correctly.
			DebugCheck();
		}

		void Start () {
			// Set the index to 0 for the start.
			_locationIndex = 0;
			// Grab the transform for more efficiency.
			_transform = gameObject.transform;
			// Grab the manager of this gameobject.
			_character = GetComponentInParent<Character>();
			// IF there is a Character to this Entity.
			if(_character != null){
				// Grab the stats of this Entity.
				charStats = _character.GetComponentInChildren<Character_Stats>();
			}
			// Lets start moving!
			StartCoroutine(GoToNextLocation());
		}

		void DebugCheck(){
			// IF the locations were not set.
			if(Locations.Length == 0){
				// Say the locations are not set.
				Grid.helper.DebugErrorCheck(76, this.GetType(), gameObject);
			}
		}
			
		private IEnumerator GoToNextLocation(){
			// While everything is true!!!!
			while(true){
				// WHILE the current position has not reached the next position.
				while(_transform.position != Locations[_locationIndex].position){
					// IF this is a Character,
					// ELSE its just a random object so lets move to the next point with no interruptions.
					if (_character != null) {
						// IF this Entity has stats.
						if (charStats != null) {
							// Set its movement speed based on the stats.
							Speed = charStats.CurrentMoveSpeed;
						}
						// IF we are not pausing the movement.
						if (_character.CanMove) {
							// Move towards the next point.
							_transform.position = Vector2.MoveTowards (_transform.position, Locations [_locationIndex].position, Speed * Time.deltaTime);
						}
					} else {
						// Move towards the next point.
						_transform.position = Vector2.MoveTowards (_transform.position, Locations [_locationIndex].position, Speed * Time.deltaTime);
					}
					yield return null;
				}
				// IF we are moving forward in the array,
				// ELSE we are moving backwards in the array.
				if(forward){
					// Increase the index.
					_locationIndex++;
				}else{
					// Decrease the index.
					_locationIndex--;
				}
				// IF we are at the end of the array,
				// ELSE IF we are at the start of the array.
				if((_locationIndex + 1 == Locations.Length)){
					// IF we want to loop back to the beginning,
					// ELSE we move backwards through the array.
					if (loop) {
						// Go to the start.
						_locationIndex = 0;
					} else {
						// Time to move backwards in the array.
						forward = false;
					}
				}else if((_locationIndex == 0)){
					// Time to move forward in the array.
					forward = true;
				}
				// Wait somewhere inbetween the min and max time.
				yield return new WaitForSeconds(Random.Range(WaitTimeMin, WaitTimeMax));
			}
		}
	}
}