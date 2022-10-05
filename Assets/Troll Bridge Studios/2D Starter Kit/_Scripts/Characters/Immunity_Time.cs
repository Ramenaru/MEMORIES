using UnityEngine;
using System.Collections;

namespace TrollBridge {

	public class Immunity_Time : MonoBehaviour {

		// The amount of time before you can be hit again.
		public float invulnerabilityTime = 1.5f;
		// The Sprite Renderer you want to have flashing.
		public SpriteRenderer flashRenderer;
		// The time for the flash effect on the Sprite Renderer 'Flash Renderer'.
		public float flashIntervalTime = 0.05f;
		// Used for our editor script to display certain variables.
		public bool showFIT;

		private float invulnerTime;
		private float flashTime;
		private bool isVulnerable = true;


		void OnValidate(){
			// IF we have a sprite renderer to flash,
			// ELSE we dont have a sprite renderer to flash.
			if (flashRenderer != null) {
				showFIT = true;
			} else {
				showFIT = false;
			}
		}

		void Awake(){
			// Check for user errors.
			DebugCheck();
		}

		void Start(){
			// Set the time to be invulernable for countdown.
			invulnerTime = invulnerabilityTime;
		}
		
		void Update(){
			// If we are not vulnerable then we start the counter.
			if (!isVulnerable) {
				// Countdown.
				invulnerTime -= Time.deltaTime;
				// If there is a Sprite Renderer.
				if (flashRenderer != null) {
					// Countdown.
					flashTime -= Time.deltaTime;
					// IF the flashTime is past due for a switch AND the flashIntervalTime set is not 0 (Meaning we want the immunity effect but not the flash effect.)
					if (flashTime <= 0f && flashIntervalTime != 0f) {
						// Make the gameobject flash.
						flashRenderer.enabled = !flashRenderer.enabled;
						// Reset the timer.
						flashTime = flashIntervalTime;
					}
				}
				// When the time is up.
				if (invulnerTime <= 0f) {
					// We are now able to be hit.
					isVulnerable = true;
					// Make the sprite renderer be enabled.
					flashRenderer.enabled = true;
					// Reset the time.
					invulnerTime = invulnerabilityTime;
				}
			}
		}

		void DebugCheck(){
			// IF the user forgot to apply a immune time.
			if(invulnerabilityTime == 0){
				Grid.helper.DebugErrorCheck(52, this.GetType(), gameObject);
			}
		}

		public void ChangeVulnerability(bool vulernable){
			isVulnerable = vulernable;
		}

		public bool GetVulnerability(){
			return isVulnerable;
		}
	}
}
