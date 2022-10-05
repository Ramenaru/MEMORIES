using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace TrollBridge {

	/// <summary>
	/// If the state of your GameObjects position is something valuable you want to save.  Think of if you wanted to move something and wanted that GameObject to stay in that position if you were to leave the scene and come back.
	/// </summary>
	public class State_Transform : MonoBehaviour {

		[Tooltip("The default activeness of this GameObject at runtime if there isn't any saved information.")]
		public bool defaultActiveState = true;

		void Awake () {
			// See if there is a (Clone) string in the name.
			int index = gameObject.name.IndexOf ("(Clone)");
			// IF there is a (Clone) string in the name.
			if(index != -1){
				// Remove part of the string.
				gameObject.name = gameObject.name.Remove (index);
			}
			Grid.stateManager.Register (gameObject);
		}
	}
}
