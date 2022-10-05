using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace TrollBridge {

	public class Update_EquipmentSlot : MonoBehaviour {

		[Tooltip("The Equipment Type that you want to be displayed based on what is equipped on the player.")]
		public string itemSlot;

		private GameObject playerManager;
		private Equipment equipment;
		private Image equipmentImage;

		void Start () {
			equipmentImage = GetComponent<Image> ();
		}
		
		void Update () {
			// IF there isn't a player active on the scene.
			if (playerManager == null) {
				// Get the Player GameObject.
				playerManager = Character_Manager.GetPlayerManager ();
				return;
			}
			// IF the equipment component isn't set yet
			if(equipment == null){
				// Get the Equipment script that is on the player GameObject.
				equipment = playerManager.GetComponentInChildren<Equipment> ();
				return;
			}

			// Create a variable to hold the item.
			Item equip = null;
			// IF we have a weapon image we want to update,
			// ELSE IF we have a armour image we want to update,
			// ELSE IF we have a bracelet image we want to update,
			// ELSE IF we have a ring image we want to update.
			if(itemSlot == "Weapon"){
				// Get the weapon from the Equipment script.
				equip = equipment.GetWeapon ();
			}else if(itemSlot == "Armour"){
				// Get the armour from the Equipment script.
				equip = equipment.GetArmour ();
			}else if(itemSlot == "Bracelet"){
				// Get the bracelet from the Equipment script.
				equip = equipment.GetBracelet ();
			}else if(itemSlot == "Ring"){
				// Get the ring from the Equipment script.
				equip = equipment.GetRing ();
			}

			// IF there is an item,
			// ELSE there is not a item.
			if (equip != null) {
				// Grab the weapon.
				equipmentImage.sprite = equip.SpriteImage;
				// Set the color.
				equipmentImage.color = new Color(equip.R, equip.G, equip.B, equip.A);
			} else {
				// Set the color.
				equipmentImage.color = new Color(0f, 0f, 0f, 0f);
			}
		}
	}
}
