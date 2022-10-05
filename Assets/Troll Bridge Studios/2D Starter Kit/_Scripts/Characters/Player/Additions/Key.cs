using UnityEngine;
using System.Collections;
using System;

namespace TrollBridge {

	public class Key : MonoBehaviour {

		// The currency of keys.
		public Currency[] key;

		void Awake(){
			Load ();
		}

		/// <summary>
		/// Based on the paramater string keyName this will return the key amount.
		/// </summary>
		/// <returns>The keys.</returns>
		/// <param name="keyName">Key name.</param>
		public int GetKeys(string keyName){
			// Loop through all the keys.
			for(int i = 0; i < key.Length; i++){
				// IF we find the key we are looking for.
				if(key[i].currencyName == keyName){
					// Return the amount of keys.
					return key[i].currencyAmount;
				}
			}
			return 0;
		}

		/// <summary>
		/// Adds or subtracts the key amount based on the keyName.
		/// </summary>
		/// <param name="keyName">Key name.</param>
		/// <param name="amount">Amount.</param>
		public void AddSubtractKeys(string keyName, int amount){
			// Loop through all the key types.
			for(int i = 0; i < key.Length; i++){
				// IF we find the key we are looking for.
				if(key[i].currencyName == keyName){
					// Add or Subtract the amount of keys.
					key[i].currencyAmount += amount;
					// We found the match so return.
					return;
				}
			}
		}

		/// <summary>
		/// Save all the types of keys.
		/// </summary>
		public void Save()
		{
			// Create a new Key_Data.
			Key_Data data = new Key_Data ();
			// Setup the data to be saved.
			string[] currNames = new string[key.Length];
			int[] currAmount = new int[key.Length];
			// Loop through the keys.
			for(int i = 0; i < key.Length; i++){
				// Set the name and the amount.
				currNames [i] = key [i].currencyName;
				currAmount [i] = key [i].currencyAmount;
			}
			// Save the data.
			data.keyName = currNames;
			data.keyAmount = currAmount;
			// Turn the Key_Data to Json data.
			string keyToJson = JsonUtility.ToJson(data);
			// Save the encrypted information.
			PlayerPrefs.SetString("Key", keyToJson);
		}

		/// <summary>
		/// Load the keys.
		/// </summary>
		private void Load()
		{
			// Get the saved encrypted information.
			string keyJson = PlayerPrefs.GetString("Key");
			// If we have a null or empty string.
			if(String.IsNullOrEmpty(keyJson)){
				// We do nothing.
				return;
			}
			// Cast our Json data to Key_Data.
			Key_Data data = JsonUtility.FromJson<Key_Data> (keyJson);
			// Load the values of the players currency.
			for (int i = 0; i < key.Length; i++) {
				key [i].currencyName = data.keyName [i];
				key [i].currencyAmount = data.keyAmount [i];
			}
		}
	}

	[Serializable]
	class Key_Data
	{	
		public string[] keyName;
		public int[] keyAmount;
	}
}
