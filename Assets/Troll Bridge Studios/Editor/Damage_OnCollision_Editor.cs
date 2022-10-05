using UnityEngine;
using System.Collections;
using UnityEditor;

namespace TrollBridge {

	[CanEditMultipleObjects]
	[CustomEditor(typeof(Damage_OnCollision))]
	public class Damage_OnCollision_Editor : Editor {

		SerializedProperty damageTheseTypes;
		SerializedProperty characterScript;
		SerializedProperty damageAmount;
		SerializedProperty joltAmount;
		SerializedProperty collideSound;
		SerializedProperty amplifyDamage;
		SerializedProperty isCharaScript;

		void OnEnable(){
			damageTheseTypes = serializedObject.FindProperty ("DamageTheseTypes");
			characterScript = serializedObject.FindProperty ("characterStats");
			damageAmount = serializedObject.FindProperty ("DamageAmount");
			joltAmount = serializedObject.FindProperty ("JoltAmount");
			collideSound = serializedObject.FindProperty ("collideSound");
			amplifyDamage = serializedObject.FindProperty ("amplifyDamage");
			isCharaScript = serializedObject.FindProperty ("isCharaScript");
		}

		public override void OnInspectorGUI(){
			// Set the indentLevel to 0 as default (no indent).
			EditorGUI.indentLevel = 0;
			// Update.
			serializedObject.Update ();
			// Set the label width.
			EditorGUIUtility.labelWidth = 190f;

			EditorGUILayout.PropertyField (damageTheseTypes, new GUIContent("Damage These Types", "The Character Types that this will be applied to."), true);
			EditorGUILayout.PropertyField (characterScript, new GUIContent ("Character Stats (Data)", "Optional - The GameObject that holds the Character Data.\n\nIF this is left null/emtpy then the damage done will be taken from 'Damage Amount'."));
			// IF there is a character script attached to this GameObject
			// ELSE we have some object that isnt a character but does damage so we just manually set a damage for it.
			if (isCharaScript.boolValue) {
				// We want to base everything around the damage the player has so we make a amplify based around that damage so we can dish out multiple types of damage for different actions.
				EditorGUILayout.PropertyField (amplifyDamage, new GUIContent ("+Bonus Amplify Damage (%)", "Since we have a reference for our base damage in the Character script we just handle other sources of damage based on collision by amplifying the source of the damage.  This is useful as we can now have multiple types of spells/attacks/etc that can do a range of damage.  \n\n0 Bonus Amplify Damage will not alter the damage at all while 100 will double the damage."));
			} else {
				// We just set a flat damage for this GameObject.
				EditorGUILayout.PropertyField (damageAmount, new GUIContent ("Damage Amount", "The amount of damage to apply on collision."));
			}
			EditorGUILayout.PropertyField (joltAmount, new GUIContent("Knockback Amount", "The amount of force to use on our knockback."));
			EditorGUILayout.PropertyField (collideSound, new GUIContent("Collide Sound", "(OPTIONAL) - The sound clip that is played when this GameObject damages another GameObject from colliding with it."));

			// Apply
			serializedObject.ApplyModifiedProperties ();
		}
	}
}
