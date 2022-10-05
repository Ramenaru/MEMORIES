using UnityEngine;
using System.Collections;

namespace TrollBridge {

	[RequireComponent(typeof(SpriteRenderer))]
	[RequireComponent(typeof(Collider2D))]
	[RequireComponent(typeof(Rigidbody2D))]
	public class Item_GameObject : MonoBehaviour {


		[Tooltip("Set to true if this Item was manually placed by you in the scene, if not we will know this item was created after hitting play.")]
		public bool isPlaced = false;
		[Tooltip("The ID you want this Item to be from your Items.json file in the Streaming Assets folder.")]
		public int id;
		[Tooltip("The amount of this Item.")]
		public int amount = 1;
		[Tooltip("Set to true if this is an item that will go into an inventory.")]
		public bool inventoryItem = true;
		[ReadOnlyAttribute]
		public string title;
		[ReadOnlyAttribute]
		public int value;
		[ReadOnlyAttribute]
		public string rarity;
		[ReadOnlyAttribute]
		public string type;
		[ReadOnlyAttribute]
		public float r;
		[ReadOnlyAttribute]
		public float g;
		[ReadOnlyAttribute]
		public float b;
		[ReadOnlyAttribute]
		public float a;
		[ReadOnlyAttribute]
		public int damage;
		[ReadOnlyAttribute]
		public int armour;
		[ReadOnlyAttribute]
		public int magicArmour;
		[ReadOnlyAttribute]
		public float movementSpeed;
		[ReadOnlyAttribute]
		public float health;
		[ReadOnlyAttribute]
		public float mana;
		[ReadOnlyAttribute]
		public int restoreHP;
		[ReadOnlyAttribute]
		public int restoreMP;
		[ReadOnlyAttribute]
		public float bonusHealth;
		[ReadOnlyAttribute]
		public string description;
		[ReadOnlyAttribute]
		public bool stackable;
		[ReadOnlyAttribute]
		public string slug;
		[ReadOnlyAttribute]
		public AudioClip pickupSound;
		[ReadOnlyAttribute]
		public AudioClip usedSound;

		private bool canPickUp = false;
		// This is the wait time for when this GameObject can be picked up.
		private float WAITTIME = 0.75f;
		// The Rigid Body of this GameObject.
		private Rigidbody2D rigBody;
		// The Collider of this GameObject.
		private Collider2D colliderTwoD;


		void OnValidate()
		{
			// IF we have a 0 amount of this item.
			if(amount < 1)
			{
				// Change to 1 because we cant have 0 of any existing item.
				amount = 1;
			}
		}

		void Awake()
		{
			// Get the RigidBody2D on this GameObject.
			rigBody = GetComponent<Rigidbody2D> ();
			// Get the Collider2D on this GameObject;
			colliderTwoD = GetComponent<Collider2D>();
			// Since this just spawned we make it non pickupable for a short time so the player can tell wtf this item actually is lol before grabbing it.
			StartCoroutine (TimeTillPickUp());
		}

		void Start()
		{
			Item item = Grid.itemDataBase.FetchItemByID (id);

			title = item.Title;
			value = item.Value;
			rarity = item.Rarity;
			type = item.Type;
			r = item.R;
			g = item.G;
			b = item.B;
			a = item.A;
			GetComponent<SpriteRenderer> ().color = new Color (r, g, b, a);
			damage = item.Damage;
			armour = item.Armour;
			magicArmour = item.MagicArmour;
			movementSpeed = item.MoveSpeed;
			health = item.Health;
			mana = item.Mana;
			restoreHP = item.RestoreHP;
			bonusHealth = item.BonusHealth;
			description = item.Description;
			stackable = item.Stackable;
			slug = item.Slug;
			pickupSound = item.PickUpSound;
			usedSound = item.UsedSound;
		}

		void Update(){
			// IF this GameObject is not moving.
			// ELSE we turn the trigger off so it can detect walls
			if (rigBody.velocity == Vector2.zero) {
				colliderTwoD.isTrigger = true;
			} else {
				colliderTwoD.isTrigger = false;
			}
		}

		void OnTriggerEnter2D(Collider2D coll)
		{
			AddItemByCollision (coll.gameObject);
		}

		void OnTriggerStay2D(Collider2D coll)
		{
			AddItemByCollision (coll.gameObject);
		}

		void OnCollisionEnter2D(Collision2D coll)
		{
			AddItemByCollision (coll.gameObject);
		}

		void OnCollisionStay2D(Collision2D coll)
		{
			AddItemByCollision (coll.gameObject);
		}

		/// <summary>
		/// Add item to the players collection when picked/collided up/with.
		/// </summary>
		/// <param name="coll">Coll.</param>
		private void AddItemByCollision(GameObject coll)
		{
			// IF the colliding gameobject doesnt have a Player_Manager Component.
			if (coll.GetComponentInParent<Player_Manager> () == null) 
			{
				return;
			}
			// Set a Player Manager variable for faster referencing.
			Player_Manager playerManager = coll.GetComponentInParent<Player_Manager> ();
			// IF the timer is up so that we can pick up the item.
			if(!canPickUp)
			{
				return;
			}

			// Play the Pickup Sound.
			Grid.soundManager.PlaySound (pickupSound);

			// IF this is an item that goes into your inventory.
			// ELSE there is pre set slots for certain items.  Think of keys and bombs in Binding of Isaac.
			if (inventoryItem) {
				// IF there is a Inventory System.
				if (Grid.inventory != null) {
					// Handle Inventory Stuff.  If we ended up NOT collecting the item we just leave. True if we collected the item, False if we didn't.
					if (!HandleInventory ()) {
						return;
					}
				}
			} else {
				// Handle the Types Stuff.
				HandleTypes (playerManager);
			}
				// Check if there is a State handler script.
				CheckState();
		}

		private bool HandleInventory()
		{
			// Get the inventory component.
			Inventory inv = Grid.inventory;
			// IF this item is stackable, ************(Incase you want to set a max limit on item stacks this is where you will add your code check to see if adding the amount will overflow the item stack limit.)********
			// ELSE this item is not stackable.
			if (stackable) {
				// IF this item is already in the inventory OR there is a free spot in the inventory.
				if (inv.items.Contains (Grid.itemDataBase.FetchItemByID (id)) || inv.GetFreeSpots () >= 1) {
					// Add the item to the inventory.
					inv.AddItem (id, amount);
					// Check if there is a State handler script.
					CheckState ();
					// We are placing an item in our inventory so return true.
					return true;
				}

			} else {
				// IF there are more free spots than the amount we are adding.
				if (inv.GetFreeSpots () >= amount) {
					// Add the item to the inventory.
					inv.AddItem (id, amount);
					// Check if there is a State handler script.
					CheckState ();
					// We are placing an item in our inventory so return true.
					return true;
				}
			}
			// We have an item but we have any room for it.
			return false;
		}

		private void HandleTypes(Player_Manager playerManager)
		{
			// IF we pick up a Key,
			// ELSE IF we pick up a Bomb,
			// ELSE IF we pick up a type of Currency,
			// ELSE IF we pick up a stat increase Item,
			// ELSE IF *tbc*
			if (type == "Key") {
				// Get the Key script.
				Key key = playerManager.GetComponentInChildren<Key> ();
				// Add to the keys.
				key.AddSubtractKeys (title, amount);
			} else if (type == "Bomb") {
				// Get the Drop_Bombs script.
				Bombs playerDB = playerManager.GetComponentInChildren<Bombs> ();
				// Add to the keys.
				playerDB.AddSubtractBomb (amount);
			} else if (type == "Currency") {
				// Get the Money script.
				Money money = playerManager.GetComponentInChildren<Money> ();
				// IF we have a Crystal Currency.
				if (title == "Green Crystal" || title == "Blue Crystal") {
					// Add to the currency.
					money.AddSubtractMoney ("Crystal", amount);
				}
			} else if (type == "Stat Increase") {
				// Get the Character Stats (or any data structure if you choose to make your own or use another Asset), 
				Character_Stats charStats = playerManager.GetComponentInChildren<Character_Stats> ();
				// Then Add to the stats.
				charStats.IncreaseMaxHealth (bonusHealth);
//				charStats.IncreaseMaxMana (bonusMana);
//				charStats.IncreaseBaseDamage (bonusDamage);
//				charStats.IncreaseBaseMoveSpeed (bonusMoveSpeed);
			}
		}

		private void CheckState ()
		{
			// IF this item was placed in the scene by the developer lets just make it inactive as if we want to save this item's state we cannot destroy it.
			// ELSE this item was created during gameplay, so to prevent memory leaks we unregister and destroy the gameobject so we dont save an inactive GameObject that will never be active in the scene.
			if (isPlaced) {
				// Set the gameobject inactive.
				gameObject.SetActive (false);
			} else {
				// IF there is a State Transform script.
				if (GetComponent<State_Transform> () != null) {
					// Unregister this item as when we pick it up it gets destroyed as there 
					// is no need for an item that you just picked up.
					Grid.stateManager.Unregister (gameObject);
				}
				// We destroy what is out there.
				Destroy (gameObject);
			}
		}

		/// <summary>
		/// Start a timer for how long it takes for this item to be picked up.
		/// </summary>
		/// <returns>The till pick up.</returns>
		private IEnumerator TimeTillPickUp()
		{	
			// Wait  a certain amount of time
			yield return new WaitForSeconds(WAITTIME);
			canPickUp = true;
		}
	}
}
