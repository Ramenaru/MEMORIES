using UnityEngine;
using System.Collections;
using System;

namespace TrollBridge {

	public class Money : MonoBehaviour {
		// The types of currencies
		public Currency[] currency;

		void Awake(){
			Load ();
		}

		/// <summary>
		/// Based on the paramater string currencyName this will return the currency amount.
		/// </summary>
		/// <returns>The money.</returns>
		/// <param name="currencyName">Currency name.</param>
		public int GetCurrency(string currencyName){
			// Loop through all the currenies.
			for(int i = 0; i < currency.Length; i++){
				// IF we find the currency we are looking for.
				if(currency[i].currencyName == currencyName){
					// Return the amount of currency.
					return currency[i].currencyAmount;
				}
			}
			return 0;
		}

		/// <summary>
		/// Adds or subtracts the currency based on the currencyName.
		/// </summary>
		/// <param name="currencyName">Currency name.</param>
		/// <param name="amount">Amount.</param>
		public void AddSubtractMoney(string currencyName, int amount){
			// Loop through all the currenies.
			for(int i = 0; i < currency.Length; i++){
				// IF we find the currency we are looking for.
				if(currency[i].currencyName == currencyName){
					// Add or Subtract the amount of currency.
					currency[i].currencyAmount += amount;
					// We found the match so return.
					return;
				}
			}
		}

		/// <summary>
		/// Save all the types of currencies.
		/// </summary>
		public void Save()
		{
			// Create a new Currency_Data.
			Currency_Data data = new Currency_Data ();
			// Setup the data to be saved.
			string[] currNames = new string[currency.Length];
			int[] currAmount = new int[currency.Length];
			// Loop through the currencies.
			for(int i = 0; i < currency.Length; i++){
				// Set the name and the amount.
				currNames [i] = currency [i].currencyName;
				currAmount [i] = currency [i].currencyAmount;
			}
			// Save the data.
			data.currencyName = currNames;
			data.currencyAmount = currAmount;
			// Turn the Currency_Data to json.
			string currencyToJson = JsonUtility.ToJson(data);
			// Save the information.
			PlayerPrefs.SetString ("Money", currencyToJson);
		}

		/// <summary>
		/// Load the currencies.
		/// </summary>
		private void Load()
		{
			// Get the encrypted json of the Money.
			string currencyJson = PlayerPrefs.GetString ("Money");
			// IF the Json string is null or empty
			if(String.IsNullOrEmpty(currencyJson)){
				// We leave as there is nothing to load.
				return;
			}
			// Turn the Json to Currency_Data.
			Currency_Data data = JsonUtility.FromJson<Currency_Data> (currencyJson);
			// Load the values of the players currency.
			for (int i = 0; i < currency.Length; i++) {
				currency [i].currencyName = data.currencyName [i];
				currency [i].currencyAmount = data.currencyAmount [i];
			}
		}
	}

	[Serializable]
	class Currency_Data
	{	
		public string[] currencyName;
		public int[] currencyAmount;
	}
}
