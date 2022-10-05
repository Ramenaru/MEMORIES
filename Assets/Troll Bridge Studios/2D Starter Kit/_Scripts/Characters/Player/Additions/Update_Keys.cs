using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace TrollBridge {

	[RequireComponent(typeof(Text))]
	public class Update_Keys : MonoBehaviour {

		public string keyToUpdate;

		private GameObject playerManager;
		private Key key;
		private Text keyText;

		void Start () {
			keyText = GetComponent<Text> ();
		}

		void Update () {
			// IF there isn't a player manager active on the scene.
			if (playerManager == null) {
				// Get the Player GameObject.
				playerManager = Character_Manager.GetPlayerManager ();
				return;
			}
			// IF there isn't a Key component set yet.
			if(key == null){
				// Get the Key script that is on the player GameObject.
				key = playerManager.GetComponentInChildren<Key> ();
				return;
			}
			// Set the Text component.
			keyText.text = "x " + key.GetKeys(keyToUpdate).ToString();
		}
	}
}
