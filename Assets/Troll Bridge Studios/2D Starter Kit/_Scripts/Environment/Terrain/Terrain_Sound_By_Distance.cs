using UnityEngine;
using System.Collections;

namespace TrollBridge {

	public class Terrain_Sound_By_Distance : MonoBehaviour {

		// The sound clip to play when Colliding.
		public AudioClip soundClip;
		// The min and max pitch for when this sound is played.
		public float minPitch = 1;
		public float maxPitch = 1;

		// The distance before playing another sound
		public float distance = 1;
		// The distance variable that holds the current distance.
		private float currDistance;

		// Previous position.
		private Vector2 prev;
		// Current position.
		private Vector2 curr;


		void Awake(){
			// Check for mistakes.
			DebugCheck();
		}

		void OnTriggerEnter2D(Collider2D coll){
			// IF there is a sound to play.
			if(soundClip != null && coll.gameObject.tag == "Player"){
				// Set the start distance.
				curr = coll.transform.position;
			}
		}

		void OnTriggerStay2D(Collider2D coll){
			if(coll.gameObject.tag == "Player"){
				// The new is the old.
				prev = curr;
				// Get the current distance.
				curr = coll.transform.position;
				// Compare distances.
				currDistance += Vector2.Distance(curr, prev);

				// IF we have traveled the required amount.
				if(currDistance > distance){
					// Play the sound.
					AudioSource soundSource = Grid.soundManager.PlaySound(soundClip, coll.transform.position, minPitch, maxPitch);
					// Set the parent of this gameobject.
					Grid.helper.SetParentTransform(coll.gameObject.transform, soundSource.gameObject);
					// Reset currDistance.
					currDistance = 0;
				}
			}
		}

		void DebugCheck(){
			// IF there isn't a soundClip we need to let the user know.
			if(soundClip == null){
				Grid.helper.DebugErrorCheck(46, this.GetType(), gameObject);
			}
			// IF the distance is 0 there will be a spam, notify to make greater than 0.
			if(distance == 0){
				Grid.helper.DebugErrorCheck(47, this.GetType(), gameObject);
			}
		}
	}
}
