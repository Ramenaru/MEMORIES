using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

namespace TrollBridge {

	public class Character_Stats : MonoBehaviour {

		// The characters base damage.
		public float DefaultDamage = 0f;
		// The characters base movement speed.
		public float DefaultMoveSpeed = 1f;
		// The characters base health.
		public float DefaultHealth = 3f;
		public float DefaultMaxHealth = 5f;
		// The characters base mana.
		public float DefaultMana = 20f;
		public float DefaultMaxMana = 20f;


		[ReadOnlyAttribute]
		public float CurrentDamage;
		[ReadOnlyAttribute]
		public float BonusDamage;

		[ReadOnlyAttribute]
		public float CurrentMoveSpeed;
		[ReadOnlyAttribute]
		public float BonusMoveSpeed;

		[ReadOnlyAttribute]
		public float MaxHealth;
		[ReadOnlyAttribute]
		public float CurrentHealth;
		[ReadOnlyAttribute]
		public float BonusHealth;

		[ReadOnlyAttribute]
		public float MaxMana;
		[ReadOnlyAttribute]
		public float CurrentMana;
		[ReadOnlyAttribute]
		public float BonusMana;

		// Used for our Player_Manager referencing.
		private Player_Manager playerManager;

		void Awake () 
		{
			playerManager = GetComponentInParent<Player_Manager> ();
			Load ();
		}

		/// <summary>
		/// Increase the max health.
		/// </summary>
		public void IncreaseMaxHealth(float amount)
		{
			// We add to our bonus variable to keep track of how much bonus we have.
			BonusHealth += amount;
			// Since our max is growing, so does our current with it.
			CurrentHealth += amount;
		}

		/// <summary>
		/// Increase the max mana.
		/// </summary>
		public void IncreaseMaxMana(float amount)
		{
			// We add to our bonus variable to keep track of how much bonus we have.
			BonusMana += amount;
			// Since our max is growing, so does our current with it.
			CurrentMana += amount;
		}

		/// <summary>
		/// Increase the base damage.
		/// </summary>
		public void IncreaseBaseDamage(float amount)
		{
			// We add to our bonus variable to keep track of how much bonus we have.
			BonusDamage += amount;
		}

		/// <summary>
		/// Increase the base movement speed.
		/// </summary>
		public void IncreaseBaseMoveSpeed(float amount)
		{
			// We add to our bonus variable to keep track of how much bonus we have.
			BonusMoveSpeed += amount;
		}


		/// <summary>
		/// Get the default health.
		/// </summary>
		public float GetDefaultHealth(){
			return DefaultHealth;
		}

		/// <summary>
		/// Get the default max health.
		/// </summary>
		public float GetDefaultMaxHealth(){
			return DefaultMaxHealth;
		}

		/// <summary>
		/// Get the current.
		/// </summary>
		public float GetCurrentHealth(){
			return CurrentHealth;
		}

		/// <summary>
		/// Get the current max health.
		/// </summary>
		public float GetMaxHealth(){
			return MaxHealth;
		}

		/// <summary>
		/// Get the bonus health.
		/// </summary>
		public float GetBonusHealth(){
			return BonusHealth;
		}

		/// <summary>
		/// Set the max health.
		/// </summary>
		public void SetMaxHealth(float newMaxHealth){
			MaxHealth = newMaxHealth;
		}


		/// <summary>
		/// Get the default mana.
		/// </summary>
		public float GetDefaultMana(){
			return DefaultMana;
		}

		/// <summary>
		/// Get the default max mana.
		/// </summary>
		public float GetDefaultMaxMana(){
			return DefaultMaxMana;
		}

		/// <summary>
		/// Get the current mana.
		/// </summary>
		public float GetCurrentMana(){
			return CurrentMana;
		}

		/// <summary>
		/// Get the max mana.
		/// </summary>
		public float GetMaxMana(){
			return MaxMana;
		}

		/// <summary>
		/// Get the bonus mana.
		/// </summary>
		public float GetBonusMana(){
			return BonusMana;
		}

		/// <summary>
		/// Set the max mana.
		/// </summary>
		public void SetMaxMana(float newMaxMana){
			MaxMana = newMaxMana;
		}


		/// <summary>
		/// Get the default damage.
		/// </summary>
		public float GetDefaultDamage(){
			return DefaultDamage;
		}

		/// <summary>
		/// Get the current damage.
		/// </summary>
		public float GetCurrentDamage(){
			return CurrentDamage;
		}

		/// <summary>
		/// Get the bonus damage.
		/// </summary>
		public float GetBonusDamage(){
			return BonusDamage;
		}

		/// <summary>
		/// Set the current damage.
		/// </summary>
		public void SetCurrentDamage(float newCurrentDamage){
			CurrentDamage = newCurrentDamage;
		}


		/// <summary>
		/// Get the default movement speed.
		/// </summary>
		public float GetDefaultMoveSpeed(){
			return DefaultMoveSpeed;
		}

		/// <summary>
		/// Get the current movement speed.
		/// </summary>
		public float GetCurrentMoveSpeed(){
			return CurrentMoveSpeed;
		}

		/// <summary>
		/// Get the bonus movement speed.
		/// </summary>
		public float GetBonusMoveSpeed(){
			return BonusMoveSpeed;
		}

		/// <summary>
		/// Set the current movementspeed.
		/// </summary>
		public void SetCurrentMoveSpeed(float newMoveSpeed){
			CurrentMoveSpeed = newMoveSpeed;
		}


		/// <summary>
		/// Save our Character Stats.
		/// </summary>
		public void Save()
		{
			// Create a new Player_Data.
			Character_Data data = new Character_Data ();
			// Save the data.
			data.currentHealth = CurrentHealth;
			data.currentMana = CurrentMana;
			// Save the bonus information.
			data.bonusHealth = BonusHealth;
			data.bonusMana = BonusMana;
			data.bonusDamage = BonusDamage;
			data.bonusMoveSpeed = BonusMoveSpeed;

			// Turn the Character_Stats data into Json data.
			string charStatsToJson = JsonUtility.ToJson (data);
			// IF we are saving the player,
			// ELSE we are saving an non player.
			if (playerManager != null) {
				// Save the information.
				PlayerPrefs.SetString ("Player", charStatsToJson);
			} else {
				// Save the information. (scene name / gameobject name).  Care when using this as you want unique names of your monsters if you choose to have them saved.
				PlayerPrefs.SetString (SceneManager.GetActiveScene().name +"/"+ gameObject.name, charStatsToJson);
			}
		}

		private void Load()
		{
			string charStatsJson;
			// IF we are loading the player,
			// ELSE we are loading an enemy.
			if (playerManager != null) {
				// Load the information.
				charStatsJson = PlayerPrefs.GetString ("Player");
				// IF there is nothing in this string.
				if (String.IsNullOrEmpty (charStatsJson)) {
					// Load the default value of the stats.
					CurrentHealth =  DefaultHealth;
					CurrentMana =  DefaultMana;
					// GTFO of here we done son!
					return;
				}
			} else {
				// Load the information. (scene name / gameobject name).
				charStatsJson = PlayerPrefs.GetString (SceneManager.GetActiveScene().name +"/"+ gameObject.name);
				// IF there is nothing in this string.
				if (String.IsNullOrEmpty (charStatsJson)) {
					// Load the default value of the stats.
					CurrentDamage = DefaultDamage;
					CurrentHealth =  DefaultHealth;
					MaxHealth = DefaultMaxHealth;
					CurrentMana =  DefaultMana;
					MaxMana = DefaultMaxMana;
					CurrentMoveSpeed = DefaultMoveSpeed;
					// GTFO of here we done son!
					return;
				}
			}
			// Turn the json data to represent Equipment_Data.
			Character_Data data = JsonUtility.FromJson<Character_Data> (charStatsJson);
			// Load the player stats.
			CurrentHealth = data.currentHealth;
			CurrentMana = data.currentMana;
			BonusDamage = data.bonusDamage;
			BonusMoveSpeed = data.bonusMoveSpeed;
			BonusHealth = data.bonusHealth;
			BonusMana = data.bonusMana;;
		}
	}


	[Serializable]
	class Character_Data
	{	
		public float currentHealth;
		public float currentMana;

		public float bonusHealth;
		public float bonusMana;
		public float bonusDamage;
		public float bonusMoveSpeed;
	}
}

