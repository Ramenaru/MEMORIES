using UnityEngine;
using System.Collections;

namespace TrollBridge {

	[RequireComponent (typeof (Collider2D))]
	public class SetActive_Exit_Collision : MonoBehaviour {

		// Set to 'true' if you want to set the gameobjects InActive else it will make the gameobjects Active.
		public bool setInactive = true;
		// The gameobjects that activate.
		public GameObject[] activateGameObjects;
		// The GameObjects that will either be activated or deactivated.
		public GameObject[] onExitGameObjects;

		void Start(){
			DebugCheck();
		}

		// Exit Physical Collision.
		void OnCollisionExit2D(Collision2D coll){
			SetActivityOnExit(coll.gameObject);
		}
		
		// Exit Trigger Collision.
		void OnTriggerExit2D(Collider2D coll){
			SetActivityOnExit(coll.gameObject);
		}

		void DebugCheck(){
			// IF there is not any gameobjects to manipulate.
			if (activateGameObjects.Length == 0) {
				Grid.helper.DebugErrorCheck (21, this.GetType (), gameObject);
			}
			// IF there is not any onEnterActivation gameobjects.
			if (onExitGameObjects.Length == 0) {
				Grid.helper.DebugErrorCheck (27, this.GetType (), gameObject);
			}
		}

		void SetActivityOnExit(GameObject coll){
			// Loop through the activated gameobjects.
			for(int i = 0; i < activateGameObjects.Length; i++){
				// IF we have matching Gameobjects for activation.
				if(coll.gameObject == activateGameObjects[i]){
					// Time to set activeness based on choice.
					Grid.helper.SetActiveGameObjects (onExitGameObjects, !setInactive);
					Grid.helper.SetActiveGameObject (gameObject, false);
					// Once we have 1 match we are done.
					return;
				}
			}
		}
	}
}
