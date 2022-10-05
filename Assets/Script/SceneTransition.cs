using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneTransition : MonoBehaviour {

    public string sceneToLoad;
    public Vector2 playerPos;
    public VectorValue playerStorage;
    public GameObject fadeToBlack;

    private void Awake() {
        if (fadeToBlack != null) {
            GameObject panel = Instantiate(fadeToBlack, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(panel, 1);
        }
    }

    public void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") && !other.isTrigger) {
            playerStorage.initialValue = playerPos;
            SceneManager.LoadScene(sceneToLoad); 
        }
    }

}