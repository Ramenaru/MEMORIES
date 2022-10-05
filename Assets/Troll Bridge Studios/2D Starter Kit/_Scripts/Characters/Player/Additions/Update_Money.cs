using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace TrollBridge {

	[RequireComponent(typeof(Text))]
	public class Update_Money : MonoBehaviour {

		public string currencyToUpdate;

		private GameObject playerManager;
		private Money currency;
		private Text moneyText;

		void Start () {
			moneyText = GetComponent<Text> ();
		}

		void Update () {
			// IF there isn't a player manager active on the scene.
			if (playerManager == null) {
				// Get the Player GameObject.
				playerManager = Character_Manager.GetPlayerManager ();
				return;
			}
			// IF there isn't a Money component set yet.
			if(currency == null){
				// Get the Money script that is on the player GameObject.
				currency = playerManager.GetComponentInChildren<Money> ();
				return;
			}
			// Update the text to this currency.
			moneyText.text = "x " + currency.GetCurrency(currencyToUpdate).ToString();
		}
	}
}
