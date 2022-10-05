using UnityEngine;
using System.Collections;

namespace TrollBridge {

	public class Loot : MonoBehaviour {

		public Loot_Dropped[] LootDropped;

		public void DeathDrop(){
			// Loop through the LootDropped Array.
			for(int i = 0; i < LootDropped.Length; i++){
				// IF there is a GameObject to drop as loot.
				if(LootDropped[i].LootGameObject != null){
					// Multiplier to handle decimal drop rates.
					int multiplier = 0;
					// Get the percent of the this item to drop.
					float percent = LootDropped [i].Percent;

					// WHILE we have a decimal.
					while(percent % 10 != 0){
						// Increase the multiplier.
						++multiplier;
						// Multiply the drop percent by 10 and recheck to see if we still have a decimal.
						percent *= 10;
					}

					// Based on the multiplier lets get our top range.
					int topRange = (int) Mathf.Pow (10f, 2 + multiplier);
					// Get a random number from 1 to the topRange.
					int dropNumber = Random.Range(1, topRange + 1);
					// IF the odds are in the drops favor.
					if(dropNumber <= percent){
						// Loop the amount of times we want to drop this loot.
						for(int j = 0; j < LootDropped[i].Amount; j++){
							// Drop the item.
							DropItem (LootDropped[i].LootGameObject);
						}
					}
				}
			}
		}

		private void DropItem(GameObject loot){
			// Spawn the drop.
			GameObject item = Grid.helper.SpawnObject (loot, GetComponentInParent<Character>().characterEntity.transform.position, Quaternion.identity, GetComponentInParent<Character>().characterEntity);
			// IF there is a Rigidbody2D.
			if(item.GetComponent<Rigidbody2D>() != null){
				// Launch the loot around.
				Grid.helper.LaunchItem (item, -1f, 1f, -1f, 1f);
			}
		}
	}
}
