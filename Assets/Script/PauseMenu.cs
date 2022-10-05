using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    private bool isPaused;
    [SerializeField] GameObject pausePanel;
    public string mainMenu;

    void Start() {
        isPaused = false;
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            isPaused =!isPaused;
            if(isPaused) {
                pausePanel.SetActive(true);
                Time.timeScale = 0f;
            }else{
                pausePanel.SetActive(false);
                Time.timeScale = 1f;
            }
        }
    }
   public void Resume () {
		pausePanel.SetActive (false);
		Time.timeScale = 1f;
	}

    public void QuitMainmenu (int sceneID) {
        Time.timeScale = 1f;
        SceneManager.GetActiveScene();
        SceneManager.LoadScene (sceneID);

    }
}