 using UnityEngine;
 using System.Collections;
 using UnityEngine.SceneManagement;
 
 public class MusicMantapmm : MonoBehaviour {
 
     void Awake ()
     {
         GameObject[] objs = GameObject.FindGameObjectsWithTag("music");
         if (objs.Length > 1)
             Destroy(this.gameObject);
 
         DontDestroyOnLoad(this.gameObject);
 
     }
 
     void Update()
     {
         if (SceneManager.GetActiveScene().name == "whitespace")
         {
             Destroy(this.gameObject);
         }
     }
 }