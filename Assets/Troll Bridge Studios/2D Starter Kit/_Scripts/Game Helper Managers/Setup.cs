using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

namespace TrollBridge {

	/// <summary>
	/// This script is what sets up important variables for your game.  You set your player GameObject and your UI Canvas here.  Also all of your Sprites and GameObjects in your Resources folder will be loaded for referencing here.
	/// </summary>
	public class Setup : MonoBehaviour {

		// Player prefab.
		[Tooltip("The Player Prefab that will be spawned when the Scene starts.")]
		public GameObject player;
		// The UI.
		public GameObject UICanvas;
		// The Integer that correlates to the scenes spawn location.
		private int sceneSpawnLocation = 0;
		// The name of the current scene.
		private string sceneName = "";
		// The array that holds the Sprites in your Resources folder.
		private Sprite[] sprites;
		// The array that holds the GameObjects in your Resources folder.
		private GameObject[] gameobjects;


		void OnEnable(){
			// Add a delegate.
			SceneManager.sceneLoaded += OnLoadedLevel;
		}

		void OnDisable(){
			// Remove the delegate.
			SceneManager.sceneLoaded -= OnLoadedLevel;
		}

		void Awake(){
			// Lets load all of our sprites in the resources folder.
			LoadResources();
			// Load the sceneSpawnLocation.
			Load();
		}

		void OnLoadedLevel(Scene scene, LoadSceneMode mode){
			// IF the player exists then so does our UI.
			// ELSE the player does not exists then our UI does not.
			if (Character_Manager.GetPlayerManager () != null) {
				// Set the UI Active.
				UICanvas.SetActive (true);
			} else {
				// Set the UI NOT Active.
				UICanvas.SetActive (false);
			}
			// Load any states associated with this current scene.
			Grid.stateManager.Load ();
		}

		/// <summary>
		/// Gets the scene spawn location.
		/// </summary>
		/// <returns>The scene spawn location.</returns>
		public int GetSceneSpawnLocation(){
			return sceneSpawnLocation;
		}
		/// <summary>
		/// Sets the scene spawn location.
		/// </summary>
		/// <param name="location">Location.</param>
		public void SetSceneSpawnLocation(int location){
			sceneSpawnLocation = location;
		}

		/// <summary>
		/// Gets the name of the scene start.
		/// </summary>
		/// <returns>The scene start name.</returns>
		public string GetSceneStartName(){
			return sceneName;
		}
		/// <summary>
		/// Sets the name of the scene start.
		/// </summary>
		/// <param name="name">Name.</param>
		public void SetSceneStartName(string name){
			sceneName = name;
		}

		/// <summary>
		/// Clear the setup.
		/// </summary>
		public void ClearSetup(){
			sceneSpawnLocation = 0;
			sceneName = "";
		}

		/// <summary>
		/// Loads the resources from our Resources Folder.
		/// </summary>
		public void LoadResources(){
			sprites = Resources.LoadAll<Sprite> ("Sprites");
			gameobjects = Resources.LoadAll<GameObject> ("Prefabs");
		}

		public GameObject[] GetPrefabs(){
			return gameobjects;
		}

		public bool IsSpriteInResource(Sprite target){
			// Loop through the array.
			for(int i = 0; i < sprites.Length; i++){
				// IF we have a matching pair.
				if(sprites[i] == target){
					// It is in the array so return true.
					return true;
				}
			}
			// It is not in the array so return false;
			return false;
		}

		public Sprite GetSprite(string spriteName){
			// Loop through the array size.
			for(int i = 0; 0 < sprites.Length; i++){
				// IF we have a matching pair.
				if(sprites[i].name == spriteName){
					// It is in the array so return true.
					return sprites [i];
				}
			}
			// We dun goofed.
			Debug.Log("The sprite " +spriteName+ " is not in your Resources/Sprites/ Folder.");
			return null;
		}

		public bool IsGameObjectInResource(GameObject target){
			// Loop through the array.
			for(int i = 0; i < gameobjects.Length; i++){
				// IF we have a matching pair.
				if(gameobjects[i] == target){
					// It is in the array so return true.
					return true;
				}
			}
			// It is not in the array so return false;
			return false;
		}

		public GameObject GetGameObjectPrefab(string gameobjectName){
			// Loop through the array size.
			for(int i = 0; 0 < gameobjects.Length; i++){
				// IF we have a matching pair.
				if(gameobjects[i].name == gameobjectName){
					// Return the GameObject
					return gameobjects [i];
				}
			}
			// We dun goofed.
			Debug.Log("The GameObject " +gameobjectName+ " is not in your Resources/Prefabs/ Folder.");
			return null;
		}

		/// <summary>
		/// Save anything important	for loading next time.
		/// </summary>
		public void Save()
		{
			// Create a new Player_Data.
			Setup_Data data = new Setup_Data ();
			// Save the data.
			data.sceneSpawnLocation = sceneSpawnLocation;
			data.sceneStart = sceneName;
			// Turn the Setup_Data to Json data.
			string setupToJson = JsonUtility.ToJson (data);
			// Save the information.
			PlayerPrefs.SetString ("Setup", setupToJson);
		}

		/// <summary>
		/// Load anything important to setup.
		/// </summary>
		private void Load()
		{
			// Grab the encrypted Setup_Data json string.
			string setupJson = PlayerPrefs.GetString ("Setup");
			// IF there is nothing in this string.
			if (String.IsNullOrEmpty (setupJson)) {
				// GTFO of here we done son!
				return;
			}
			// Turn the json data to represent Equipment_Data.
			Setup_Data data = JsonUtility.FromJson<Setup_Data> (setupJson);
			// Load the player stats.
			sceneSpawnLocation = data.sceneSpawnLocation;
			sceneName = data.sceneStart;
		}
	}

	[Serializable]
	class Setup_Data
	{	
		public int sceneSpawnLocation;
		public string sceneStart;
	}
}
