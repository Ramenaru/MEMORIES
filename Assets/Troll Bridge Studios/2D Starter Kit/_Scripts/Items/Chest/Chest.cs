using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace TrollBridge {

	[RequireComponent(typeof(SpriteRenderer))]
	public class Chest : MonoBehaviour {

		[Tooltip("The sprite of the chest when it is opened.")]
		public Sprite openChest;
		[Tooltip("The sprite of the chest when it is closed.")]
		public Sprite closedChest;
		[Tooltip("The sound the chest makes when it is opened.")]
		public AudioClip openChestSound;
		[Tooltip("The loot that is inside the chest.")]
		public GameObject chestLoot;
		[Tooltip("Does this chest require a key to open.")]
		public bool requireKeyToOpen = false;
		public Chest_Key_Consume ckc;


		void Start(){
			// IF this item has a icon hover AND the chest is opened then we remove it.
			if(GetComponentInChildren<Icon_Display>() != null && GetComponent<SpriteRenderer>().sprite == openChest){
				GetComponentInChildren<Icon_Display> ().gameObject.SetActive (false);
			}
		}

		void OnCollisionEnter2D(Collision2D coll){
			// IF the chest is already opened.
			if(GetComponent<SpriteRenderer>().sprite == openChest){
				return;
			}
			// IF the colliding gameobjects tag isnt the player.
			if(coll.gameObject.tag != "Player"){
				return;
			}
			// IF the colliding gameobject has a Key component,
			// ELSE the colliding gameobject does not have a Key component.
			if (coll.gameObject.GetComponentInParent<Player_Manager> ().gameObject.GetComponentInChildren<Key> () != null) {
				// Grab the Key script.
				Key key = coll.gameObject.GetComponentInParent<Player_Manager> ().gameObject.GetComponentInChildren<Key> ();

				// IF the player does not have any keys AND this chest requires a key to open.
				if (key.GetKeys (ckc.name) == 0 && requireKeyToOpen) {
					return;
				}
				// The prerequisites have been met so lets reduce the key by 1, open the chest and shoot the item from the chest.
				// IF this chest required a key to open.
				if(ckc.consume){
					// Remove a key.
					key.AddSubtractKeys(ckc.name, -1);
				}
			}
			// Open the Chest.
			OpenChest (coll.gameObject);
		}

		public void OpenChest(GameObject collidingObject){
			// Change the sprite of the chest.
			GetComponent<SpriteRenderer>().sprite = openChest;
			// Play the chest sound.
			Grid.soundManager.PlaySound (openChestSound);
			// Spawn the loot from the chest.
			GameObject chestGO = Grid.helper.SpawnObject (chestLoot, transform.position, Quaternion.identity, gameObject);
			// Launch the item in a direction away from the colliding object.
			Grid.helper.LaunchItemAwayFromPosition (chestGO, collidingObject.transform.position);
			// IF this item has a icon hover then we remove it.
			if(GetComponentInChildren<Icon_Display>() != null){
				// Set the hovering icon GameObject inactive.
				GetComponentInChildren<Icon_Display> ().gameObject.SetActive (false);
			}
		}
	}
}
