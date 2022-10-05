using UnityEngine;
using System.Collections.Generic;

namespace TrollBridge {

	public class Follow_Closest_Type : MonoBehaviour {

		[Tooltip("The CharacterType to follow")]
		public CharacterType TypeToFollow;
		[Tooltip("How far the mob can see before aggroing on the CharacterType")]
		public float AggroDistance = 5f;

		private Character character;
		private Character_Stats charStats;
		private List<Character> listCharacter = new List<Character>();

		void Start(){
			// Get the Character component.
			character = GetComponentInParent<Character> ();
			// Get the Character Stats component.
			charStats = character.GetComponentInChildren<Character_Stats> ();
		}

		void Update () {
			// IF this character is able to move.
			if(character.CanMove){
				// Get the list of all the characters.
				listCharacter = Character_Manager.GetCharactersByType(listCharacter, TypeToFollow);
				// IF the List of CharacterTypes is greater than 0.
				if(listCharacter.Count > 0){
					// Create a GameObject variable.
					GameObject _character = null;
					// Get the closest GameObject with the CharacterType chosen and save it to _character.
					_character = Character_Manager.GetClosestCharacterType (character.transform, TypeToFollow, _character, AggroDistance);
					// IF the closest gameobject is not null.
					if(_character != null){
						// Move the actual character of this gameobject closer to _character gameobject.
						character.characterEntity.transform.position = 
							Vector2.MoveTowards(transform.position, _character.GetComponent<Character> ().characterEntity.transform.position, Time.deltaTime * charStats.CurrentMoveSpeed);
					}
				}
			}
		}
	}
}
