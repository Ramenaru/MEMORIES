using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace TrollBridge {

	public class Update_Health_Icon : MonoBehaviour {

		[Tooltip("The maximum amount of hearts per row.")]
		public int MaxHeartsPerRow;
		[Tooltip("The amount of health contained in each heart.")]
		public float HealthPerHeart;
		[Tooltip("The full heart GameObject")]
		public GameObject FullHeart;
		[Tooltip("The half heart GameObject")]
		public GameObject HalfHeart;
		[Tooltip("The missing heart GameObject")]
		public GameObject MissingHeart;
		[Tooltip("The x spacing between the hearts.")]
		public float xSpacing = 5f;
		[Tooltip("The y spacing between the hearts.")]
		public float ySpacing = 2f;

		private Player_Manager pm;
		private Character_Stats charStats;
		private float spriteWidth;
		private float spriteHeight;


		void Update(){
			// IF there isn't a player manager active on the scene.
			if (pm == null) {
				// Get the Player Manager GameObject.
				pm = Character_Manager.GetPlayerManager ().GetComponent<Player_Manager>();
				return;
			}
			// IF there isn't a Character_Stats (or any script you want that holds your characters data) for your characters data.
			if(charStats == null)
			{
				// Get your Character_Stats.
				charStats = pm.GetComponentInChildren<Character_Stats> ();
				return;
			}
			// Display your hearts.
			DisplayHearts ();
		}

		private void DisplayHearts() {
			// Delete the children of this gameobject.
			Grid.helper.DestroyGameObjectsByParent (gameObject);
			// Get the max hearts to display based on the players max health and how much each heart is worth.
			int maxHearts = (int)(charStats.MaxHealth / HealthPerHeart);
			// Get how many rows we need based on our maxHearts and how many hearts per row we can have.
			int numberOfRows = Mathf.CeilToInt ((float)maxHearts/MaxHeartsPerRow);
			// Get the amount of full hearts so we know for display purposes.
			int fullHearts = (int)(charStats.CurrentHealth / HealthPerHeart);
			// Get the amount of half hearts so we know for display purposes.
			int halfHeart = (int)(charStats.CurrentHealth % HealthPerHeart);
			GameObject heart;

			// Loop through the amount of rows
			for (int j = 0; j < numberOfRows; j++) {
				// Used for the remainder of hearts.
				int remaining;
				// IF the amount of hearts we can put in a row is less or equal than the maximum amount of hearts.
				// ELSE IF if you 
				// ELSE the amount of hearts we can put in a row is greater than or equal than the maximum amount of hearts.
				if ((j + 1) * MaxHeartsPerRow <= maxHearts) {
					// We fill up a full row.
					remaining = MaxHeartsPerRow;
				} else if (maxHearts - MaxHeartsPerRow < 0) {
					remaining = maxHearts;
				} else {
					// We fill up with the remaining hearts left.
					remaining = maxHearts - MaxHeartsPerRow;
				}
					
				// Loop through the amount of remaining hearts.
				for (int i = 0; i < remaining; i++) {
					int heartPos = j * MaxHeartsPerRow;
					// IF we have Full Hearts.
					// ELSE IF we have a half heart.
					// ELSE we have an empty heart.
					if (i + heartPos < fullHearts) {
						// Create the full heart.
						heart = Instantiate (FullHeart) as GameObject;
					} else if (i + heartPos < fullHearts + halfHeart) {
						// Create the half heart.
						heart = Instantiate (HalfHeart) as GameObject;
					} else {
						// Create the full heart.
						heart = Instantiate (MissingHeart) as GameObject;
					}
					// Set the parent.
					Grid.helper.SetParentTransform (gameObject.transform, heart);
					// Set the scale.
					heart.transform.localScale = new Vector3 (1f, 1f, 1f);
					// Get the width of this image.
					spriteWidth = heart.GetComponent<RectTransform> ().rect.width;
					spriteHeight = heart.GetComponent<RectTransform> ().rect.height;
					// Set the position.
					heart.transform.localPosition = new Vector3 (i * (xSpacing + spriteWidth), -j * (ySpacing + spriteHeight), 0f);
				}
			}
		}
	}
}
