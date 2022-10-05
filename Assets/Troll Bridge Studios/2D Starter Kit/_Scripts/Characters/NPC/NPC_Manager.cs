using UnityEngine;
using System.Collections;

namespace TrollBridge {

	public class NPC_Manager : Character, Can_Take_Damage, Can_Attack {

	//	public int Experience = 0;
		private Character_Stats charStats;

		void Awake(){
			// Assign the Animator Component.
			CharacterAnimator = characterEntity.GetComponent<Animator>();
			charStats = GetComponentInChildren<Character_Stats> ();
		}

		/// <summary>
		/// When the GameObject takes damage we are either Dead or Alive and dealing with the actions.  I have this method broke up so it is easier to read.  
		/// If you want to add more code to this then add the appropriate method of either Death() or Hit().
		/// </summary>
		/// <param name="damage">Damage.</param>
		/// <param name="otherTransform">Other transform.</param>
		/// <param name="joltAmount">Jolt amount.</param>
		public void TakeDamage(float damage, Transform otherTransform, float joltAmount){
			// Remove HP.
			charStats.CurrentHealth -= damage;
			// IF the current health is 0 or below.
			if(charStats.CurrentHealth <= 0f){
				// Do all the stuff you want when this GameObject Dies.
				Death ();
				// GTFO.
				return;
			}
			// We are hit.
			Hit(otherTransform, joltAmount);
		}

		/// <summary>
		/// Everything you want to happen when the GameObject dies.
		/// </summary>
		private void Death(){
			// Display health as 0.
			charStats.CurrentHealth = 0f;
			// Play the Die sound.
			Grid.soundManager.PlaySound (DieSound, transform.position, 1f, 1f);
			// Drop loot if this has any.
			if(GetComponentInChildren<Loot>() != null){
				GetComponentInChildren<Loot> ().DeathDrop();
			}
			// Spawn the destroy effects.
			Grid.helper.SpawnObject(afterDeathVisual, characterEntity.transform.position, Quaternion.identity, characterEntity);
			// Destroy this gameobject.
			Destroy(gameObject);
		}

		/// <summary>
		/// Everything you want to happen when the GameObject takes damage but doesnt die.
		/// </summary>
		/// <param name="otherTransform">Other transform.</param>
		/// <param name="joltAmount">Jolt amount.</param>
		private void Hit(Transform otherTransform, float joltAmount){
			// Play the sound from getting hit.
			Grid.soundManager.PlaySound (GetHitSound, transform.position, 1f, 1f);

			// IF this animator has a state called Hit.
			if(CharacterAnimator.HasState(0, Animator.StringToHash ("Hit"))){
				// Start a coroutine to handle the timing of the GameObject being hit.
				StartCoroutine (HitAnimation());
			}

			// IF the character that we collided with can be knockedback.
			if (CanBeJolted) {
				// Get the relative position.
				Vector2 relativePos = gameObject.transform.position - otherTransform.position;
				// Get the rigidbody2D
				Rigidbody2D charRigid = characterEntity.GetComponent<Rigidbody2D> ();
				// Stop the colliding objects velocity.
				charRigid.velocity = Vector3.zero;
				// Apply knockback.
				charRigid.AddForce (relativePos.normalized * joltAmount, ForceMode2D.Impulse);
				// Make the character not be able to be controled while being knockedback.
				StartCoroutine (NoCharacterControl());
			}
		}

		/// <summary>
		/// Whenever this monster attacks we will associate a few actions.  (Keep in mind monsters that just run into the player to do damage with no attack animation should not call this method.)
		/// 1) Animation (Not all enemies have an attack animation, example being the bat in the Demo.)
		/// 2) Attack Sound (Not all enemies have an attack animation, example being the bat in the Demo.)
		/// 3) Movement Restriction
		/// </summary>
		/// <param name="clip">Clip.</param>
		public void Attack(string animationNameValue, AudioClip clip){
			// IF we are currently not attacking.
			if (!CharacterAnimator.GetBool (animationNameValue)) {
				// Set the Attack Animation.
				CharacterAnimator.SetBool (animationNameValue, true);
				// Play the attack sound (if there is one).
				Grid.soundManager.PlaySound (clip);
				// Set the IsMoving variable for the animation to false since the character will not be moving while attacking.
				CharacterAnimator.SetBool ("IsMoving", false);
				// Make it to where the GameObject cannot move.
				CanMove = false;
			}
		}

		private IEnumerator NoCharacterControl(){
			// Make the character not be able to be controlled while the knockback is happening.
			CanMove = false;
			// Wait for 'noControlTime' time before being able to control the character again.
			yield return new WaitForSeconds (HitAnimationTime);
			// Stop the knockback.
			characterEntity.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			// We can now move the character.
			CanMove = true;
		}

		private IEnumerator HitAnimation()
		{
			CharacterAnimator.SetBool("IsHit", true);
			yield return new WaitForSeconds (HitAnimationTime);
			CharacterAnimator.SetBool ("IsHit", false);
		}
	}
}
