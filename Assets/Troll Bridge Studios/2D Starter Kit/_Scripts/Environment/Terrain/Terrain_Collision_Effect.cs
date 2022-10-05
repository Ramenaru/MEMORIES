using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TrollBridge {

	[RequireComponent (typeof (Effector2D))]
	public class Terrain_Collision_Effect: MonoBehaviour {

		// The animation to be played while colliding.
		[Tooltip("The effect to be played while colliding.")]
		public GameObject terrainEffect;

		
		void Awake(){
			// Check for mistakes.
			DebugCheck();
		}
		
		void OnTriggerEnter2D(Collider2D coll){
			// Loop through the children of this gameobject.
			foreach(Transform child in coll.transform){
				// IF the child gameobject has a tag of 'feet'?
				if(child.gameObject.tag == "Feet"){
					// Spawn the animation at the feet.
					GameObject anim = Instantiate(terrainEffect, child.gameObject.transform.position, Quaternion.identity) as GameObject;
					// Set the animation as a child.
					Grid.helper.SetParentTransform(child.gameObject.transform, anim);
				}
			}
		}

		void OnTriggerExit2D(Collider2D coll){

			// Loop through the children of this gameobject.
			foreach(Transform child in coll.transform){
				// IF the child gameobject has a tag of 'feet'?
				if(child.gameObject.tag == "Feet"){
					// Destroy all child gameobjects.
					Grid.helper.DestroyGameObjectsByParent(child.gameObject);
				}
			}
		}

		void DebugCheck(){
			// IF there isn't a terrain animation we need to let the user know.
			if(terrainEffect == null){
				Grid.helper.DebugErrorCheck(44, this.GetType(), gameObject);
			}
		}
	}
}
