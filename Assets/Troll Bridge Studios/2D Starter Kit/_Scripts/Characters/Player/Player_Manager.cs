using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TrollBridge {

	public class Player_Manager : Character, Can_Take_Damage, Can_Attack {

		public float MainAttackCooldown = 0.2f;

		public KeyCode InteractionKey;
		public KeyCode AttackKey;

		// The invert movement for the X.
		public int PlayerInvertX = 1;
		// The invert movement for the Y.
		public int PlayerInvertY = 1;

		// Is the player engaged in an Action Key Dialogue.
		public bool IsActionKeyDialogued = false;
		// The Action Key Dialogue we are currently engaged in due to it being the closest.
		public Action_Key_Dialogue ClosestAKD = null;
		// The List of Area Dialogues currently inside.
		public List<Area_Dialogue> ListOfAreaDialogues = new List<Area_Dialogue>();
		// The List of Action Key Dialogues currently inside.
		public List<Action_Key_Dialogue> ListOfActionKeyDialogues = new List<Action_Key_Dialogue>();

		private Collider2D playerCollider;
		private Equipment equipment;
		// The stats of the Player.
		private Character_Stats charStats;
		// The audio clip for when this character attacks.
		public AudioClip AttackSound;


		void Awake()
		{
			// Get the players Collider2D.
			playerCollider = characterEntity.GetComponent<Collider2D> ();
			// Assign the Animator Component.
			CharacterAnimator = characterEntity.GetComponent<Animator> ();
			// Get the Equipment Script.
			equipment = GetComponentInChildren<Equipment> ();
			// Get the Character Stats script.
			charStats = GetComponentInChildren<Character_Stats> ();
			// Load the Inventory.
			Grid.inventory.LoadInventory();
		}

		void Start()
		{
			// Load stats from items.
			LoadStatsFromItems();
			// IF there is a animator on the Player.
			if(CharacterAnimator != null)
			{
				// IF the Animator is a Four Direction Animation,
				// ELSE IF the Animator is a Eight Direction Animation.
				if(CharacterAnimator.GetLayerName(0) == "Four Base"){
					// Set true that we have a Four Direction Animation.
					FourDirAnim = true;
					// Set false that we have a Eight Direction Animation.
					EightDirAnim = false;
				}else if(CharacterAnimator.GetLayerName(0) == "Eight Base"){
					// Set false since we dont have a Four Direction Animation.
					FourDirAnim = false;
					// Set true since we dont have a Eight Direction Animation.
					EightDirAnim = true;
				}
			}
		}

		void Update(){
			// Load stats from bonus stats, default stats and items.
			LoadStatsFromItems();
			// Make the player look at the NPC it is interacting with.
			PlayerLookDirection();
			// IF the InteractionKey is the same as the AttackKey. (In this case, interaction has a higher priority than attacking.)
			if (InteractionKey == AttackKey) {
				// IF we pressed the Interaction Key.
				if (Input.GetKeyDown (InteractionKey)) {
					// IF we are close enough to an Action Key Dialogue component.
					if(DialogueInteraction()){
						// Create the dialogue with the closest GameObject with an Action Key Dialogue.
						ClosestAKD.CreateDialogue ();
						// This is what we are focusing on now so lets return.
						return;
					}
					// IF there is an Animator on the player AND we actually have a weapon to attack with.
					if (CharacterAnimator != null && equipment.GetWeapon() != null) {
						// ATTACK!!!
						Attack ("IsAttacking", AttackSound);
					}
					return;
				}
			}

			// IF we pressed the Attack Key.
			if (Input.GetKeyDown (AttackKey)) {
				// IF there is an Animator on the player AND we actually have a weapon to attack with.
				if (CharacterAnimator != null && equipment.GetWeapon() != null) {
					// ATTACK!!!
					Attack ("IsAttacking", AttackSound);
				}
			}
		}

		/// <summary>
		/// When the player attacks (As you can see I pass a AudioClip as a parameter for the sound to this GameObject as different attacks can have different sounds).  
		/// When a player is attacking we have to take care of a few things by default (atleast for this case of the demo).  
		/// The animation, attack sound any any movement restrictions applied for attacking.
		/// </summary>
		public void Attack(string animationNameValue, AudioClip clip){
			// IF we are currently not attacking.
			if (!CharacterAnimator.GetBool (animationNameValue)) 
			{
				// Set the Attack Animation.
				CharacterAnimator.SetBool (animationNameValue, true);
				// Play the attack sound (if there is one).
				Grid.soundManager.PlaySound (clip);
				// Set the IsMoving variable for the animation to false since the character will not be moving while attacking. (Personal choice)
				CharacterAnimator.SetBool ("IsMoving", false);
				// Make it to where the player cannot move. (Personal choice)
				CanMove = false;
			}
		}


		private bool DialogueInteraction(){
			// IF we are inside a Action Key Dialogue area.
			if(ListOfActionKeyDialogues.Count > 0){
				// IF we are not already engaged in a dialogue.
				if(!IsActionKeyDialogued){
					// Grab the list of dialogue gameobjects that the player is currently inside.
					List<Action_Key_Dialogue> akd = ListOfActionKeyDialogues;
					// Preset a distance variable to detect the closest action key dialogue.
					float _dist = -1f;
					// Loop through all the Action Key Dialogues.
					for(int i = 0; i < akd.Count; i++){
						// See which one is the closest.
						float dist = Vector2.Distance(characterEntity.transform.position, akd[i].gameObject.transform.position);
						// IF this is the first time in here. Also this takes care of 1 Interactive NPC in the List.
						// ELSE IF we have more interactive npcs and we need to compare distance to see which is closest.
						if(_dist == -1f){
							// Set the shortest distance.
							_dist = dist;
							// Set the closest action key dialogue.
							ClosestAKD = akd[i];
						}else if(dist < _dist){
							// Set the shortest distance.
							_dist = dist;
							// Set the closest action key dialogue.
							ClosestAKD = akd[i];
						}
					}
					// We are now engaged in a dialogue.
					IsActionKeyDialogued = true;
				}
				// We have started a dialogue so return true.
				return true;
			}
			// No dialogue started so we return false.
			return false;
		}

		/// <summary>
		/// This will make the Player look at whoever they are focused on.  Think of a dialogue if you are talking to someone
		/// lets make sure we are facing them (if you have the animations to do so, if not nothing bad will happen in terms of crashing or atleast it shouldnt :3).
		/// </summary>
		private void PlayerLookDirection(){
			if(ClosestAKD != null){
				// Store the focused objects Transform.
				Transform focTransform = ClosestAKD.transform;
				// IF we have a Four Direction Animation for this gameobject,
				// ELSE IF we have a Eight Direction Animation for this gameobject.
				if(FourDirAnim){
					Grid.helper.FourDirectionAnimation(focTransform.position.x - characterEntity.transform.position.x, 
						focTransform.position.y - characterEntity.transform.position.y,
						false,
						CharacterAnimator);
				}else if(EightDirAnim){
					Grid.helper.EightDirectionAnimation(focTransform.position.x - characterEntity.transform.position.x, 
						focTransform.position.y - characterEntity.transform.position.y,
						false,
						CharacterAnimator);
				}
			}
		}

		/// <summary>
		/// When the player takes damage we are either Dead or Alive and dealing with the actions.  I have this method broke up to be pretty easy to read.  
		/// If you want to add more code to this then add the appropriate method of either Death() or Hit().
		/// </summary>
		/// <param name="damage">Damage.</param>
		/// <param name="otherTransform">Other transform.</param>
		/// <param name="joltAmount">Jolt amount.</param>
		public void TakeDamage(float damage, Transform otherTransform, float joltAmount){
			// Remove HP.
			charStats.CurrentHealth -= damage;
			// IF we are dead.
			if(charStats.CurrentHealth <= 0f){
				// We DIEDEDED!!! NOOOOOOOO....
				Death ();
				// We are dead so lets leave.
				return;
			}
			// We are hit.
			Hit(otherTransform, joltAmount);
		}

		/// <summary>
		/// Everything you want to happen when the player dies.
		/// </summary>
		private void Death(){
			// Display health as 0.
			charStats.CurrentHealth = 0f;
			// Remove the collider to prevent any more damage being taken to cause errors.
			playerCollider.enabled = false;
			// IF we have a die sound.
			if(DieSound != null){
				// Play the die sound.
				Grid.soundManager.PlaySound (DieSound, transform.position, 1f, 1f);
			}
			// IF this animator has a state called playerDeath.
			if(CharacterAnimator.HasState(0, Animator.StringToHash ("Death"))){
				CharacterAnimator.SetBool ("IsDead", true);
			}
			// Make the player not be able to control the character while dead.
			CanMove = false;
		}

		/// <summary>
		/// Everything you want to happen when the player takes damage but doesnt die.
		/// </summary>
		/// <param name="otherTransform">Other transform.</param>
		/// <param name="joltAmount">Jolt amount.</param>
		private void Hit(Transform otherTransform, float joltAmount){
			// Play the sound from getting hit.
			if(GetHitSound != null){
				Grid.soundManager.PlaySound (GetHitSound, transform.position, 1f, 1f);
			}

			// IF this animator has a state that represents your getting Hit animation.
			if(CharacterAnimator.HasState(0, Animator.StringToHash ("Hit"))){
				StartCoroutine (HitAnimation());
			}
			// IF the character that we collided with can be knockedback.
			if (CanBeJolted) {
				// Knock GameObject back.
				Knockback (otherTransform, joltAmount);
				// Make the Hero not be able to control the character while being knockedback.
				StartCoroutine (NoCharacterControl());
			}
		}

		public void Knockback(Transform otherTransform, float joltAmount){
			// Get the relative position.
			Vector2 relativePos = characterEntity.transform.position - otherTransform.position;
			// Get the rigidbody2D
			Rigidbody2D charRigid = characterEntity.GetComponent<Rigidbody2D> ();
			// Stop the colliding objects velocity.
			charRigid.velocity = Vector3.zero;
			// Apply knockback.
			charRigid.AddForce (relativePos.normalized * joltAmount, ForceMode2D.Impulse);
		}

		private IEnumerator HitAnimation()
		{
			CharacterAnimator.SetBool("IsHit", true);
			yield return new WaitForSeconds (HitAnimationTime);
			CharacterAnimator.SetBool ("IsHit", false);
		}

		private IEnumerator NoCharacterControl()
		{
			// Make the player not be able to control the character while the knockback is happening.
			CanMove = false;
			// We are currently being knockbacked.
			currentlyJolted = true;
			// Wait for 'HitAnimationTime' before being able to control the character again.
			yield return new WaitForSeconds (HitAnimationTime);
			// Stop the knockback.
			characterEntity.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			// We can now move the character.
			CanMove = true;
			// We are not being jolted anymore.
			currentlyJolted = false;
		}
			
		/// <summary>
		/// This method handles assigning your stats when you either load your stats or equip an item as all stats need to be recalculated when something like this happens.
		/// </summary>
		public void LoadStatsFromItems()
		{
			// current damage = default/base damage + bonus damage + damage from items.
			charStats.SetCurrentDamage(charStats.GetDefaultDamage() + charStats.GetBonusDamage() + (float) (equipment.GetWeaponDamage () + equipment.GetArmourDamage () + equipment.GetRingDamage() + equipment.GetBraceletDamage ()));
			// max health = default/base max health + bonus health + health from items.
			charStats.MaxHealth = charStats.GetDefaultMaxHealth() + charStats.GetBonusHealth() + (float)(equipment.GetWeaponHealth () + equipment.GetArmourHealth () + equipment.GetRingHealth () + equipment.GetBraceletHealth ());
			// max mana = default/base max mana + bonus mana + mana from item.
			charStats.MaxMana = charStats.GetDefaultMaxMana() + charStats.GetBonusMana() + (float) (equipment.GetWeaponMana () + equipment.GetArmourMana () + equipment.GetRingMana () + equipment.GetBraceletMana ());
			// current movement speed = default movement speed + bonus movement speed + movement speed from items.
			charStats.CurrentMoveSpeed = charStats.GetDefaultMoveSpeed() + charStats.GetBonusMoveSpeed() + equipment.GetWeaponMoveSpeed () + equipment.GetArmourMoveSpeed () + equipment.GetRingMoveSpeed () + equipment.GetBraceletMoveSpeed ();
		}

		public float GetHealth(){
			return charStats.CurrentHealth;
		}
		public void AddHealth(float amount){
			if (charStats.CurrentHealth + amount >= charStats.MaxHealth) {
				charStats.CurrentHealth = charStats.MaxHealth;
			} else {
				charStats.CurrentHealth += amount;
			}
		}			

		public void SavePlayer(){
			// Save the Inventory.
			Grid.inventory.Save ();
			// Save the Character Stats.
			charStats.Save();
			// Save the Equipment.
			equipment.Save();
			// Save the types of Currencies/Money.
			GetComponentInChildren<Money>().Save ();
			// Save the Keys.
			GetComponentInChildren<Key>().Save ();
			// Save the Bombs.
			GetComponentInChildren<Bombs>().SaveBombs ();
		}
	}
}
