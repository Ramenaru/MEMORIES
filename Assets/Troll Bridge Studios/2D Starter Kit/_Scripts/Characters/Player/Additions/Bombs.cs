using UnityEngine;
using System.Collections;
using System;

namespace TrollBridge {

	public class Bombs : MonoBehaviour {

	//	[Tooltip("The default bomb name.")]
	//	public string defaultBombName;
		[Tooltip("The amount of bombs your player currently has.")]
		public int bombAmount;
		[Tooltip("The distance from this GameObjects pivot you want the bomb to be dropped.  The bomb will be placed in the direction the Player is facing (Based on the Direction number in the animation.)")]
		public float bombDistance;
		[Tooltip("The key you can set to activate placing a bomb.")]
		public KeyCode bombKey;
		[Tooltip("The bomb to drop.")]
		public GameObject bomb;
		[Tooltip("The AudioClip that is played when you drop a bomb.")]
		public AudioClip placeBombSound;
		[Tooltip("The player Animator.")]
		public Animator playerAnimator;
	//	[Tooltip("The name of the bomb that will be dropped.  Since you can have multiple types of bombs the bomb prefabs needs to be in the Resources folder.")]
	//	public string bombName;


		void Awake(){
			LoadBombs ();
		}

		void Update(){
			// IF we pressed the button to drop a bomb.
			if (Input.GetKeyDown (bombKey)) {
				// IF we have any bombs to place.
				if (bombAmount > 0) {
					float x = 0;
					float y = 0;
					// Get the direction the player is facing.
					int dir = playerAnimator.GetInteger ("Direction");
					// IF we are looking up,
					// ELSE IF we are looking left,
					// ELSE IF we are looking down,
					// ELSE IF we are looking right.
					if (dir == 1) {
						y = bombDistance;
					} else if (dir == 2) {
						x = -bombDistance;
					} else if (dir == 3) {
						y = -bombDistance;
					} else if (dir == 4) {
						x = bombDistance;
					}
					// Place a bomb.  Add the Bomb Distance based on the direction the player is looking.
					Grid.helper.SpawnObject (bomb, playerAnimator.transform.position + new Vector3 (x, y, 0f), Quaternion.identity, playerAnimator.gameObject);
					// Play the sound of dropping a bomb.
					Grid.soundManager.PlaySound (placeBombSound);
					// Subtract a Bomb.
					AddSubtractBomb (-1);
				}
			}
		}

		public void SaveBombs()
		{
			// Create a new data structure for our saving.
			Bomb_Data data = new Bomb_Data ();
			// Save the data.
			data.bombAmount = bombAmount;
			// Turn the data into Json data.
			string bombToJson = JsonUtility.ToJson(data);
			// Save this information.
			PlayerPrefs.SetString("Bombs", bombToJson);
		}

		private void LoadBombs()
		{
			// Get the encrypted information.
			string bombJson = PlayerPrefs.GetString ("Bombs");
			// IF we have a null or empty string.
			if(String.IsNullOrEmpty(bombJson)){
				// We do nothing.
				return;
			}
			// Cast the Json data to our Bomb_Data.
			Bomb_Data data = JsonUtility.FromJson<Bomb_Data> (bombJson);
			// Set the amount of bombs we have.
			bombAmount = data.bombAmount;
		}

		public int GetBombs()
		{
			return bombAmount;
		}
		public void AddSubtractBomb(int amount)
		{
			bombAmount += amount;
		}
	}


	[Serializable]
	class Bomb_Data
	{
		public int bombAmount;
	}
}