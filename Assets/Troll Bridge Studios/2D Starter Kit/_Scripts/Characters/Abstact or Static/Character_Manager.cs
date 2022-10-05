using UnityEngine;
using System.Collections.Generic;
using System;

namespace TrollBridge {

	public static class Character_Manager {

		// List of Characters.
		static List<Character> characters = new List<Character>();

		// Register the character in our List.
		public static void Register(Character character){
			if(!characters.Contains(character)){
				characters.Add (character);
			}
		}

		// Unregister the character in our List.
		public static void Unregister(Character character){
			if(characters.Contains(character)){
				characters.Remove (character);
			}
		}

		// Get all the Characters in our List.
		public static List<Character> GetAllCharacters (){
			return characters;
		}

		// Get a certain type of characters in our List.
		public static List<Character> GetCharactersByType(List<Character> items, CharacterType characterType){
			// Clear the list.
			items.Clear ();
			// Loop through the amount of Characters
			for(int i = 0; i < characters.Count; i++){
				// IF we have a matching type.
				if(characters[i].characterType == characterType){
					// Add it to the list.
					items.Add (characters[i]);
				}
			}
			// Return the list.
			return items;
		}

		// Get the characters in our List by tag.
		public static List<Character> GetCharactersByTag(List<Character> items, string tagName){
			items.Clear ();
			for(int i = 0; i < characters.Count; i++){
				if(characters[i].gameObject.tag == tagName){
					items.Add (characters[i]);
				}
			}
			return items;
		}

		// Get the parent of the Player.
		public static GameObject GetPlayerManager(){
			// Loop through the amount of Characters we have.
			for(int i = 0; i < characters.Count; i++){
				// IF we have found the Player Manager parent.
				if(characters[i].gameObject.tag == "Player Manager"){
					return characters [i].gameObject;
				}
			}
			return null;
		}

		// Get the player (Use this if you only plan on having 1 Hero)
		public static GameObject GetPlayer(){
			// Loop through the amount of Characters we have.
			for(int i = 0; i < characters.Count; i++){
				// IF we have found the Player Manager parent.
				if(characters[i].gameObject.tag == "Player Manager"){
					// Loop through each child.
					foreach(Transform child in characters[i].gameObject.transform){
						// IF we have found the Player child.
						if(child.tag == "Player"){
							// Return it.
							return child.gameObject;
						}
					}
				}
			}
			return null;
		}

		// Get the closest character based on distance supplied and the type.
		public static GameObject GetClosestCharacterType(Transform transform, CharacterType characterType, GameObject _character, float distance){
			float dist = distance;
			for(int i = 0; i < characters.Count; i++){
				if(Vector2.Distance(transform.GetComponentInParent<Character>().characterEntity.transform.position, characters[i].GetComponentInParent<Character>().characterEntity.transform.position) < dist && characters[i].characterType == characterType){
					_character = characters [i].GetComponentInParent<Character>().gameObject;
				}
			}
			return _character;
		}
	}
}
