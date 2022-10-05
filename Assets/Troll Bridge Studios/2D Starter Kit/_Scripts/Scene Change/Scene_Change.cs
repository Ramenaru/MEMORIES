using UnityEngine;
using System.Collections;

namespace TrollBridge {

	[RequireComponent (typeof (Collider2D))]
	public class Scene_Change : MonoBehaviour {

		// The tags that trigger the scene change.
		[Tooltip("The tags that trigger the scene change.")]
		public string[] targetTags;
		// The scene that will be loaded.
		[Tooltip("The name of the scene that will be loaded.")]
		public string newScene;
		// The location in the new scene that the player will be spawned at.  This number is correlated to the
		// Transform locations on your Camera.
		[Tooltip("The location in the new scene that the player will be spawned at.  This number is correlated to the " +
		         "Transform locations on your Scene Manager Script.")]
		public int sceneSpawnLocation = 0;
		

		void Awake(){
			// Check for user errors.
			DebugCheck();
		}

		void OnTriggerEnter2D(Collider2D coll){
			// Loop through the array.
			for(int i = 0; i < targetTags.Length; i++){
				// IF this Tag is touched by the Tag(s) you consided in your targetTags.
				if(coll.gameObject.tag == targetTags[i]){
					// Load the next scene.
					Grid.helper.ChangeScene(newScene, sceneSpawnLocation);
				}
			}
		}

		void DebugCheck(){
			// IF the 'TargetTags' is empty.
			if(targetTags.Length == 0){
				Grid.helper.DebugErrorCheck(30, this.GetType(), gameObject);
			}
			// IF the user forgot to place the Scene they want to go to.
			if(newScene.Equals("")){
				Grid.helper.DebugErrorCheck(31, this.GetType(), gameObject);
			}
		}
	}
}
