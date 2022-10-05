using UnityEngine;
using System.Collections;

namespace TrollBridge {

	public class End_Of_Animation_Destroy : MonoBehaviour {

		[Tooltip("Place the Audio Clip that you want to be played when this GameObject is destroyed from reaching the end of its animation.")]
		public AudioClip destroySound;
		[Tooltip("Place the GameObjects that you want to spawn when this GameObject is destroyed from reaching the end of its animation.")]
		public GameObject[] destroyEffects;

		/// <summary>
		/// So far only 3 things need to be thought about when a GameObject is destroyed via it being the end of its Animation.
		/// 1) Play any sounds
		/// 2) Play any after effects
		/// 3) Destroy this GameObject
		/// </summary>
		public void DestroyAnimation(){
			// Play any sounds.
			PlaySound();
			// Instantiate any after effects when this GameObject dies.
			PlayAfterEffects();
			// Destroy the GameObjects.
			Destroy (gameObject);
		}

		// Place in here anything you would like to do when this GameObject gets destroyed from completing the Animation.
		private void PlaySound(){
			// Play the after Animation destroy sound.
			Grid.soundManager.PlaySound(destroySound);
		}

		// When this GameObject dies do you want to have any other GameObjects to spawn?  Example being when a bomb explodes you can have clouds/smoke spawn from where the bomb exploded.
		private void PlayAfterEffects(){
			// Loop through all the destroy Effects.
			for (int i = 0; i < destroyEffects.Length; i++) {
				// Spawn the destroy effects.
				Grid.helper.SpawnObject(destroyEffects[i], transform.position, Quaternion.identity, gameObject);
			}
		}
	}
}
