using UnityEngine;
using System.Collections;

namespace TrollBridge {

	[RequireComponent(typeof(Collider2D))]
	public class Bomb_Explode : MonoBehaviour {

//		[Tooltip("The Character Types that will be effected by the blast.")]
//		public CharacterType[] typeToEffect;
//		[Tooltip("The amount of damage this bomb will do to anyone that is able to Take Damage.")]
//		public float bombDamage;
//		[Tooltip("The knockback distance applied if something was to take damage from this explosion.")]
//		public float knockbackAmount;
		// The built in timer that destroys this gameobject when it explodes.
		private float timer = 0.1f;

		void Start(){
			Destroy (gameObject, timer);
		}

		void OnTriggerEnter2D(Collider2D coll){
//			// IF the explosion hits any Character.
//			if(coll.GetComponentInParent<Character>() != null)
//			{
//				// Grab the Character component.
//				Character chara = coll.GetComponentInParent<Character> ();
//				// Loop through our CharacterTypes to effect array.
//				for (int i = 0; i < typeToEffect.Length; i++) {
//					// IF we are not picky and want all CharacterTypes to be damaged.
//					if(typeToEffect[i] == CharacterType.All){
//						// Blow em up.
//						ExplodeThem (chara);
//						// Once we find something that matches we leave.
//						return;
//					}
//					// IF the CharacterTypes match.
//					if(typeToEffect[i] == chara.characterType){
//						// Blow em up.
//						ExplodeThem (chara);
//						// Once we find something that matches we leave.
//						return;
//					}
//				}
//			}

			// IF the explosion hits anything that is destroyable by the explosion.
			if(coll.GetComponent<Explodable> () != null)
			{
				// Deactivate the GameObject.
				Grid.helper.SetActiveGameObject(coll.gameObject, false);
			}
		}

//		private void ExplodeThem(Character chara){
//			// IF this character is the Player.
//			// ELSE IF this character is an Enemy
//			if(chara.characterType == CharacterType.Hero){
//				// Make the player take damage.
//				chara.GetComponent<Player_Manager> ().TakeDamage(bombDamage, transform, knockbackAmount);
//			}else if(chara.characterType == CharacterType.Enemy){
//				// Make the npc take damage.
//				chara.GetComponent<NPC_Manager>().TakeDamage (bombDamage, transform, knockbackAmount);
//			}
//		}
	}
}
