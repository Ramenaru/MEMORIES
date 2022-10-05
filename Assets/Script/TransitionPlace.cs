using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class TransitionPlace : MonoBehaviour {

    public Vector2 cameraChange;
    public Vector3 playerChange;
    private CameraMovement cameraMovement;
    public bool needText;
    public string placeName;
    public GameObject text;
    public TMP_Text placeText;
    void Start() {
        cameraMovement = Camera.main.GetComponent<CameraMovement>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") && !other.isTrigger) {
            cameraMovement.minPos += cameraChange;
            cameraMovement.maxPos += cameraChange;
            other.transform.position += playerChange;
            if (needText) {

                StartCoroutine(placeNameCo());

            }
        }
    }
    private IEnumerator placeNameCo () {
        text.SetActive(true);
        placeText.text = placeName;
        yield return new WaitForSeconds(4f);
        text.SetActive(false);
    }
}