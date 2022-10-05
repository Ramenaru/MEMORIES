using UnityEngine;
using UnityEngine.SceneManagement;

namespace TrollBridge {

	/// <summary>
	/// This is the script that starts it all!!  This controls the start of the saved persistent data.  You click "New Game" and all of your saved data gets removed so the game can be played like it was the first run through.
	/// The continue button will not be interactable if there is no saved data as there is nothing to continue!
	/// </summary>
	public class Start_Game : MonoBehaviour {
		
		[Tooltip("The Start scene for when you click New Game.")]
		public string NewGameSceneName;


		public void NewGame(){
			// Remove all saved Player Prefs.
			PlayerPrefs.DeleteAll();

			// Clear the Setup.
			Grid.setup.ClearSetup();
			// Clear any State Lists.
			Grid.stateManager.ClearList();
			// Load the first scene that starts your game.
			SceneManager.LoadScene (NewGameSceneName);
		}
	}
}
