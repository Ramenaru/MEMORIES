using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace TrollBridge {

	/// <summary>
	/// When we dont want to save when changing scenes.  This is useful if we want to transfer to a Main Menu scene or a similar scene like Main Menu.
	/// </summary>
	public class Scene_Change_Button : MonoBehaviour {

		public void NextScene(string nextScene){
			Grid.stateManager.ClearList ();
			SceneManager.LoadScene (nextScene);
		}
	}
}
