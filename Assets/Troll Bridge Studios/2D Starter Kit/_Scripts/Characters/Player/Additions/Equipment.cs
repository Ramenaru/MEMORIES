using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

namespace TrollBridge {

	public class Equipment : MonoBehaviour {

		// The stats from the items.
		public Item weapon, armour, bracelet, ring;
		private int weaponID = -1;
		private int armourID = -1;
		private int ringID = -1;
		private int braceletID = -1;

		private Item_Database database;
		private Player_Manager playerManager;


		void Awake()
		{
			database = Grid.itemDataBase;
			playerManager = GetComponentInParent<Player_Manager> ();
			// Load the player stats, if there is any.
			Load();
		}

		public void RemoveWeapon(){
			weapon = null;
			weaponID = -1;
		}

		public void RemoveArmour(){
			armour = null;
			armourID = -1;
		}

		public void RemoveRing(){
			ring = null;
			ringID = -1;
		}

		public void RemoveBracelet(){
			bracelet = null;
			braceletID = -1;
		}

		public Item EquipWeapon (Item item)
		{
			// Set up the potential variable of an Item you have equipped.
			Item oldEquippedItem = null;
			// Set the weapon we are using now as the old equipped weapon.
			oldEquippedItem = weapon;
			// Set the new Item we are equipping as our weapon.
			weapon = item;
			weaponID = weapon.ID;
			// Make sure to change the player stats when changing an item.
			playerManager.LoadStatsFromItems();
			// Return the old equipped item.
			return oldEquippedItem;
		}

		public Item EquipArmour (Item item)
		{
			// Set up the potential variable of an Item you have equipped.
			Item oldEquippedItem = null;
			// Set the armour we are using now as the old equipped armour.
			oldEquippedItem = armour;
			// Set the new Item we are equipping as our armour
			armour = item;
			armourID = armour.ID;
			// Make sure to change the player stats when changing an item.
			playerManager.LoadStatsFromItems();
			// Return the old equipped item.
			return oldEquippedItem;
		}

		public Item EquipRing (Item item)
		{
			// Set up the potential variable of an Item you have equipped.
			Item oldEquippedItem = null;
			// Set the Item we are using now as the old equipped ring.
			oldEquippedItem = ring;
			// Set the new Item we are equipping as our ring.
			ring = item;
			ringID = ring.ID;
			// Make sure to change the player stats when changing an item.
			playerManager.LoadStatsFromItems();
			// Return the old equipped item.
			return oldEquippedItem;
		}

		public Item EquipBracelet (Item item)
		{
			// Set up the potential variable of an Item you have equipped.
			Item oldEquippedItem = null;
			// Set the Item we are using now as the old equipped Bracelet.
			oldEquippedItem = bracelet;
			// Set the new Item we are equipping as our Bracelet.
			bracelet = item;
			braceletID = bracelet.ID;
			// Make sure to change the player stats when changing an item.
			playerManager.LoadStatsFromItems();
			// Return the old equipped item.
			return oldEquippedItem;
		}

		public Item GetWeapon(){
			return weapon;
		}

		public Item GetArmour(){
			return armour;
		}

		public Item GetRing(){
			return ring;
		}

		public Item GetBracelet(){
			return bracelet;
		}

		public int GetWeaponDamage(){
			// IF there is an actual weapon.
			if(weapon != null){
				// return the weapom damage stat.
				return weapon.Damage;
			}
			// There isn't a weapon so there isn't any damage.
			return 0;
		}

		public int GetWeaponHealth(){
			// IF there is an actual weapon.
			if(weapon != null){
				// return the weapom health stat.
				return weapon.Health;
			}
			// There isn't a weapon so there isn't a health stat.
			return 0;
		}

		public int GetWeaponMana(){
			// IF there is an actual weapon.
			if(weapon != null){
				// return the weapom mana stat.
				return weapon.Mana;
			}
			// There isn't a weapon so there isn't a mana stat.
			return 0;
		}

		public float GetWeaponMoveSpeed(){
			// IF there is an actual weapon.
			if(weapon != null){
				// return the weapom movement speed stat.
				return weapon.MoveSpeed;
			}
			// There isn't a weapon so there isn't a movement speed stat.
			return 0f;
		}

		public int GetArmourDamage(){
			// IF there is an actual armour.
			if(armour != null){
				// return the armour damage stat.
				return armour.Damage;
			}
			// There isn't a armour so there isn't a damage stat.
			return 0;
		}

		public int GetArmourHealth(){
			// IF there is an actual armour.
			if(armour != null){
				// return the armour health stat.
				return armour.Health;
			}
			// There isn't a armour so there isn't a health stat.
			return 0;
		}

		public int GetArmourMana(){
			// IF there is an actual armour.
			if(armour != null){
				// return the armour mana stat.
				return armour.Mana;
			}
			// There isn't a armour so there isn't a mana stat.
			return 0;
		}

		public float GetArmourMoveSpeed(){
			// IF there is an actual armour.
			if(armour != null){
				// return the armour movement speed stat.
				return armour.MoveSpeed;
			}
			// There isn't a armour so there isn't a movement speed stat.
			return 0f;
		}

		public int GetRingDamage(){
			// IF there is an actual ring.
			if(ring != null){
				// return the ring damage stat.
				return ring.Damage;
			}
			// There isn't a ring so there isn't a damage stat.
			return 0;
		}

		public int GetRingHealth(){
			// IF there is an actual ring.
			if(ring != null){
				// return the ring health stat.
				return ring.Health;
			}
			// There isn't a ring so there isn't a health stat.
			return 0;
		}

		public int GetRingMana(){
			// IF there is an actual ring.
			if(ring != null){
				// return the ring mana stat.
				return ring.Mana;
			}
			// There isn't a ring so there isn't a mana stat.
			return 0;
		}

		public float GetRingMoveSpeed(){
			// IF there is an actual ring.
			if(ring != null){
				// return the ring movement speed stat.
				return ring.MoveSpeed;
			}
			// There isn't a ring so there isn't a movement speed stat.
			return 0f;
		}

		public int GetBraceletDamage(){
			// IF there is an actual bracelet.
			if(bracelet != null){
				// return the bracelet damage stat.
				return bracelet.Damage;
			}
			// There isn't a bracelet so there isn't a damage stat.
			return 0;
		}

		public int GetBraceletHealth(){
			// IF there is an actual bracelet.
			if(bracelet != null){
				// return the bracelet health stat.
				return bracelet.Health;
			}
			// There isn't a bracelet so there isn't a health stat.
			return 0;
		}

		public int GetBraceletMana(){
			// IF there is an actual bracelet.
			if(bracelet != null){
				// return the bracelet mana stat.
				return bracelet.Mana;
			}
			// There isn't a bracelet so there isn't a mana stat.
			return 0;
		}

		public float GetBraceletMoveSpeed(){
			// IF there is an actual bracelet.
			if(bracelet != null){
				// return the bracelet movement speed stat.
				return bracelet.MoveSpeed;
			}
			// There isn't a bracelet so there isn't a movement speed stat.
			return 0f;
		}

		public void Save()
		{
			// Create a new Equipment_Data.
			Equipment_Data data = new Equipment_Data ();
			// Save the data.
			data.weaponID = weaponID;
			data.armourID = armourID;
			data.ringID = ringID;
			data.braceletID = braceletID;
			// Turn the Equipment_Data to Json data.
			string equipmentToJson = JsonUtility.ToJson (data);
			// Save the information.
			PlayerPrefs.SetString ("Equipment", equipmentToJson);
		}

		public void Load()
		{
			// Grab the encrypted Equipment string.
			string equipmentJson = PlayerPrefs.GetString ("Equipment");
			// IF there is nothing in this string.
			if (String.IsNullOrEmpty (equipmentJson)) {
				// GTFO of here we done son!
				return;
			}
			// Turn the json data to represent Equipment_Data.
			Equipment_Data data = JsonUtility.FromJson<Equipment_Data> (equipmentJson);

			// IF a weapon exists.
			if (data.weaponID != -1) {
				// Set the ID of the weapon.
				weaponID = data.weaponID;
				// Load the weapon Item.
				weapon = database.FetchItemByID (weaponID);
			}
			if (data.armourID != -1) {
				// Set the ID of the armour.
				armourID = data.armourID;
				// Load the armour Item.
				armour = database.FetchItemByID (armourID);
			}
			if (data.ringID != -1) {
				// Set the ID of the ring.
				ringID = data.ringID;
				// Load the ring Item.
				ring = database.FetchItemByID (ringID);
			}
			if (data.braceletID != -1) {
				// Set the ID of the bracelet.
				braceletID = data.braceletID;
				// Load the bracelet Item.
				bracelet = database.FetchItemByID (braceletID);
			}
		}
	}

	[Serializable]
	class Equipment_Data
	{	
		public int weaponID;
		public int armourID;
		public int ringID;
		public int braceletID;
	}
}
