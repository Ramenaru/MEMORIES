using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {

	public void Home(int sceneID) {
		Time.timeScale = 1f;
		SceneManager.LoadScene (sceneID);
	}
}