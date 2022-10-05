using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace TrollBridge {

	[RequireComponent(typeof(Text))]
	public class Update_Bombs : MonoBehaviour {

		private GameObject playerManager;
		private Bombs dropBombs;
		private Text bombText;

		void Start () {
			bombText = GetComponent<Text> ();
		}
		
		void Update () {
			// IF there isn't a player active on the scene.
			if (playerManager == null) {
				// Get the Player GameObject.
				playerManager = Character_Manager.GetPlayerManager ();
				return;
			}
			// IF there isn't a Bomb component set.
			if(dropBombs == null){
				// Get the Bomb script that is a child to the Player GameObject.
				dropBombs = playerManager.GetComponentInChildren<Bombs> ();
				return;
			}
			// Set the Text component.
			bombText.text = "x " + dropBombs.GetBombs().ToString();
		}
	}
}
