using UnityEngine;
using System.Collections;

namespace TrollBridge {

	[RequireComponent(typeof(Collider2D))]
	public class Damage_OnCollision : MonoBehaviour {

		// Apply to these types of character types.
		public CharacterType[] DamageTheseTypes;
		// The Character Component in which we will retrieve our base damage from, if not we take DamageAmount.
		// Because not everything that does damage has to be a character.
		public Character_Stats characterStats;
		// The damage done by colliding.
		public float DamageAmount = 0f;
		// The amount of force to apply.
		public float JoltAmount = 50f;
		// (OPTIONAL) - The sound clip that is played when this GameObject damages another GameObject.
		public AudioClip collideSound;
		// The amplify damage for this collision if there is a Character script or if you make your own make 
		// sure to link your current damage variable in your script with this amplifyDamage variable.
		public float amplifyDamage = 0f;
		// The boolean value to notify we have Character script.
		public bool isCharaScript = false;

		void OnValidate(){
			// IF we have a script that holds data for us to use,
			// ELSE we do not.
			if (characterStats != null) {
				// Let everything know through this variable.
				isCharaScript = true;
			} else {
				isCharaScript = false;
			}
		}

		void OnCollisionEnter2D(Collision2D coll){
			CollideDamage (coll.gameObject);
		}

		void OnCollisionStay2D(Collision2D coll){
			CollideDamage (coll.gameObject);
		}

		void OnTriggerEnter2D(Collider2D coll){
			CollideDamage (coll.gameObject);
		}

		void OnTriggerStay2D(Collider2D coll){
			CollideDamage (coll.gameObject);
		}

		private void CollideDamage(GameObject coll){
			// IF we are colliding with another gameobject that does damage.
			if(coll.GetComponent<Damage_OnCollision>() != null){
				return;
			}
			// Grab the Character Component from the colliding object.
			Character otherChar = coll.GetComponentInParent<Character> ();
			// IF there is not a Character component.
			if (otherChar == null) {
				// We return because we only care about damaging a gameobject that has the Character Component.
				return;
			}
			// Since we have a character lets get the stats script.
			Character_Stats charStats = otherChar.GetComponentInChildren<Character_Stats> ();
			// IF there is not a script to hold the stats of this Character.
			if(charStats == null){
				// Then there is nothing we can damage we are attacking a Character but this character is statless so its like a town friendly NPC.
				return;
			}
			// Loop through all the Character Types this GameObject collides with to see if we are allowed to damage them.
			for (int i = 0; i < DamageTheseTypes.Length; i++) {
				// IF the colliding objects characterType matches one of the types that can take damage or if the selection is an ALL choice.
				if (otherChar.characterType == DamageTheseTypes [i] || DamageTheseTypes[i] == CharacterType.All) {
					// Check for Immunity.
					Immunity_Time _ITS = coll.GetComponentInParent<Immunity_Time> ();
					// IF the colliding object has the Immunity_Time script.
					// ELSE just damage the target.
					if (_ITS != null) {
						// IF we are Vulnerable.
						if (_ITS.GetVulnerability ()) {
							// We passed the prerequisites to do damage... Yay!
							DamageCharacter (otherChar);
							// IF the player dies while taking damage.
							if(charStats.CurrentHealth <= 0f){
								// We just leave as there is no need to alter the Change Vulnerability.
								return;
							}
							// Since there is a Immunity_Time script on the colliding object we need to make sure we set the vulnerability of the colliding gameobject to false;
							_ITS.ChangeVulnerability (false);
						}
					} else {
						// We passed the prerequisites to do damage... Yay!.
						DamageCharacter (otherChar);
					}
					// Found 1 so we return.
					return;
				}
			}
		}

		void DamageCharacter(Character otherCharacter){
			// Variable to help us get the final damage to be done to the other GameObject.
			float damage = 0f;
			// IF we have a character script component to help us determine how much damage to do,
			// ELSE we dont have a character script component and we just do a flat damage based on DamageAmount.
			if (isCharaScript) {
				// Set the damage based on the Amplify Damage.
				damage = characterStats.CurrentDamage + (characterStats.CurrentDamage * (amplifyDamage / 100f));
			} else {
				// Set the damage based on what we set rather than from another script.
				damage = DamageAmount;
			}
			// Play the collide sound.
			Grid.soundManager.PlaySound (collideSound);
			// IF we are damaging the player,
			// ELSE IF we are damaging the enemy.
			if (otherCharacter.characterType == CharacterType.Hero) {
				// Make the player take damage.
				otherCharacter.GetComponent<Player_Manager> ().TakeDamage (damage, transform, JoltAmount);
			} else if (otherCharacter.characterType == CharacterType.Enemy) {
				// Make the npc take damage.
				otherCharacter.GetComponent<NPC_Manager> ().TakeDamage (damage, transform, JoltAmount);
			}
		}
	}
}
