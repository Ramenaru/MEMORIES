using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

namespace TrollBridge {

	public class State_Manager : MonoBehaviour {

		// List of GameObjects that have State_Transform attached to them in this scene.
		private List<GameObject> transformStateList = new List<GameObject>();


		// Register the State_Transform in our List.
		public void Register(GameObject stateTrans){
			// IF the State_Transform is not in the list.
			if (!transformStateList.Contains (stateTrans)) {
				// Add it to the list.
				transformStateList.Add (stateTrans);
			}
		}
		// Unregister the State_Transform in our List.
		public void Unregister(GameObject stateTrans){
			// IF the State_Transform is in the list.
			if(transformStateList.Contains(stateTrans)){
				// Remove from the list.
				transformStateList.Remove (stateTrans);
			}
		}

		// Clear the List.
		public void ClearList(){
			transformStateList.Clear ();
		}

		public void Save()
		{
			// IF we have nothing to save then don't save.
			if(transformStateList.Count == 0){
				return;
			}
			// Create a new State_Data.
			State_Data data = new State_Data ();
			// Loop the amount of the size of the List.
			for(int i = 0; i < transformStateList.Count; i++){
				// Create a new Object_Data struct.
				Object_Data objectData = new Object_Data ();
				// Store our data in our data structure.
				objectData.StoreData(transformStateList[i]);
				// Add it to the list.
				data.objectData.Add (objectData);
			}
			// Turn the Store_Data into Json data.
			string stateToJson = JsonUtility.ToJson(data);
			// Save the information.
			PlayerPrefs.SetString(SceneManager.GetActiveScene().name + "_State", stateToJson);
		}

		public void Load()
		{
			// Get the information.
			string stateJson = PlayerPrefs.GetString (SceneManager.GetActiveScene().name + "_State");
			// IF there is nothing in this string then lets load the default states.
			if (String.IsNullOrEmpty (stateJson)) {
				// Loop through what we have registered.
				for (int i = 0; i < transformStateList.Count; i++) {
					// Set the GameObject registered to its default active state.
					transformStateList [i].gameObject.SetActive (transformStateList [i].GetComponent<State_Transform> ().defaultActiveState);
				}
				// We loaded the defualts so GTFO of here we done son!
				return;
			}
			// Turn the Json data to State_Data.
			State_Data data = JsonUtility.FromJson<State_Data> (stateJson);

			// Loop the amount of times we have saved GameObjects.
			for (int i = 0; i < data.objectData.Count; i++) {
				// GameObject reference.
				GameObject gObject = GameObject.Find (data.objectData[i].name);
				// IF the saved GameObject does not exist currently in this scene (meaning it was added after hitting play so in this current case it is something in our Resources folder that was put on the ground).
				if (gObject == null) {
					// Lets create the Item GameObject.
					gObject = Instantiate (Grid.setup.GetGameObjectPrefab (data.objectData[i].name));
				}
				// IF this item is not an item in the players inventory.
				if (gObject.GetComponent<Item_Data> () == null) {
					// Set the position where it was last left off.
					gObject.transform.position = new Vector3 (data.objectData[i].xPos, data.objectData[i].yPos, data.objectData[i].zPos);
					// Set the layer where it was last left off.
					gObject.layer = data.objectData[i].layer;
					// IF there is a sprite name letting us know there is a Sprite Renderer.
					if (!String.IsNullOrEmpty (data.objectData[i].spriteName)) {
						// Set the Sprite Renderer properties.
						gObject.GetComponent<SpriteRenderer> ().sprite = Grid.setup.GetSprite (data.objectData[i].spriteName);
						gObject.GetComponent<SpriteRenderer> ().sortingLayerName = data.objectData[i].sortLayerName;
						gObject.GetComponent<SpriteRenderer> ().sortingOrder = data.objectData[i].sortLayerOrder;
					}
					// IF there is a collider involved.
					if (data.objectData[i].isCollider) {
						// Set the collider either active or inactive.
						gObject.GetComponent<Collider2D> ().enabled = data.objectData[i].activeCollider;
					}
					// Set the GameObject either active or inactive.
					gObject.SetActive (data.objectData[i].active);
				}
			}
		}
	}

	[System.Serializable]
	class State_Data
	{	
		public List<Object_Data> objectData = new List<Object_Data> ();
	}
}
