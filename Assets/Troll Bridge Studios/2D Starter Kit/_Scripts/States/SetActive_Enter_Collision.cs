using UnityEngine;
using System.Collections;

namespace TrollBridge {

	[RequireComponent (typeof (Collider2D))]
	public class SetActive_Enter_Collision : MonoBehaviour {

		// Set to 'true' if you want to set the gameobjects InActive else it will make the gameobjects Active.
		public bool setInactive = true;
		// The GameObjects that activate.
		public GameObject[] activateGameObjects;
		// The GameObjects that will either be activated or de-activated.
		public GameObject[] onEnterGameObjects;

		void Start(){
			DebugCheck();
		}

		// Enter Physical Collision.
		void OnCollisionEnter2D(Collision2D coll){
			SetActivityOnEnter(coll.gameObject);
		}

		// Enter Trigger Collision.
		void OnTriggerEnter2D(Collider2D coll){
			SetActivityOnEnter(coll.gameObject);
		}

		void DebugCheck(){
			// IF there is not any gameobjects to manipulate.
			if (activateGameObjects.Length == 0) {
				Grid.helper.DebugErrorCheck (20, this.GetType (), gameObject);
			}
			// IF there is not any onEnterActivation gameobjects.
			if (onEnterGameObjects.Length == 0) {
				Grid.helper.DebugErrorCheck (26, this.GetType (), gameObject);
			}
		}

		void SetActivityOnEnter(GameObject coll){
			// Loop through the activated gameobjects.
			for(int i = 0; i < activateGameObjects.Length; i++){
				// IF we have matching gameobjects for activation.
				if(coll.gameObject == activateGameObjects[i]){
					// Time to set activeness based on choice.
					Grid.helper.SetActiveGameObjects (onEnterGameObjects, !setInactive);
					Grid.helper.SetActiveGameObject (gameObject, false);
					// Once we have 1 match we are done.
					return;
				}
			}
		}
	}
}
