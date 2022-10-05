using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

namespace TrollBridge {

	public class Continue_Game : MonoBehaviour {

		private Button ContinueButton;

		void Awake(){
			// Get the Button component on this GameObject.
			ContinueButton = GetComponent<Button> ();
			// IF there is no saved data and the Continue button is not null.
			if (Grid.setup.GetSceneStartName() == "" && ContinueButton != null) {
				// No interactable button.
				ContinueButton.interactable = false;
			} else {
				// Interactable button.
				ContinueButton.interactable = true;
			}
		}

		void Update(){
			// IF there is no saved data and the Continue button is not null.
			if (Grid.setup.GetSceneStartName() == "" && ContinueButton != null) {
				// No interactable button.
				ContinueButton.interactable = false;
			} else {
				// Interactable button.
				ContinueButton.interactable = true;
			}
		}

		/// <summary>
		/// IF we are to Continue a Game then we are either doing this from the Start Screen or a Death Screen in which we want to try again.  IF you know any other ideas for this then make sure to code for that situation.
		/// </summary>
		public void ContinueGame(){
			// Destroy the Player.
			Destroy (Character_Manager.GetPlayerManager ());
			// Clear our State_Manager Lists.
			Grid.stateManager.ClearList ();
			// Load the last scene that was saved.
			SceneManager.LoadScene (Grid.setup.GetSceneStartName());
		}
	}
}
