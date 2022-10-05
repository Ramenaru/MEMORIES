using UnityEngine;
using UnityEditor;
using System.Collections;

namespace TrollBridge {

	[CanEditMultipleObjects]
	[CustomEditor(typeof(Character_Stats))]
	public class Character_Stats_Editor : Editor {

		SerializedProperty defaultDamage;
		SerializedProperty curDamage;

		SerializedProperty defaultHealth;
		SerializedProperty defaultMaxHealth;
		SerializedProperty maxHealth;
		SerializedProperty curHealth;

		SerializedProperty defaultMana;
		SerializedProperty defaultMaxMana;
		SerializedProperty maxMana;
		SerializedProperty curMana;

		SerializedProperty defaultMoveSpeed;
		SerializedProperty curMoveSpeed;

		void OnEnable(){
			defaultDamage = serializedObject.FindProperty ("DefaultDamage");
			curDamage = serializedObject.FindProperty ("CurrentDamage");

			defaultHealth = serializedObject.FindProperty ("DefaultHealth");
			defaultMaxHealth = serializedObject.FindProperty ("DefaultMaxHealth");
			maxHealth = serializedObject.FindProperty ("MaxHealth");
			curHealth = serializedObject.FindProperty ("CurrentHealth");

			defaultMana = serializedObject.FindProperty ("DefaultMana");
			defaultMaxMana = serializedObject.FindProperty ("DefaultMaxMana");
			maxMana = serializedObject.FindProperty ("MaxMana");
			curMana = serializedObject.FindProperty ("CurrentMana");

			defaultMoveSpeed = serializedObject.FindProperty ("DefaultMoveSpeed");
			curMoveSpeed = serializedObject.FindProperty ("CurrentMoveSpeed");
		}

		public override void OnInspectorGUI(){
			// Set the indentLevel to 0 as default (no indent).
			EditorGUI.indentLevel = 0;
			// Update.
			serializedObject.Update ();
			// Set the label width.
			EditorGUIUtility.labelWidth = 180f;

			EditorGUILayout.LabelField ("Character Default Stats", EditorStyles.boldLabel);
			EditorGUI.indentLevel++;
			EditorGUILayout.PropertyField (defaultDamage, new GUIContent ("Base Damage", "This is the default damage in which this character starts off with."));
			EditorGUILayout.PropertyField (defaultMoveSpeed, new GUIContent ("Base Movement Speed", "This is the default movement speed in which this character starts off with."));
			EditorGUILayout.PropertyField (defaultHealth, new GUIContent ("Default Health", "This is the default health in which this character starts off with."));
			EditorGUILayout.PropertyField (defaultMaxHealth, new GUIContent ("Default Max Health", "This is the default max health in which this character starts off with."));
			EditorGUILayout.PropertyField (defaultMana, new GUIContent ("Default Mana", "This is the default mana in which this character starts off with."));
			EditorGUILayout.PropertyField (defaultMaxMana, new GUIContent ("Default Max Mana", "This is the default max mana in which this character starts off with."));
			EditorGUI.indentLevel--;


			EditorGUILayout.LabelField ("Character Current Stats", EditorStyles.boldLabel);
			EditorGUI.indentLevel++;
			EditorGUILayout.PropertyField (curDamage, new GUIContent ("Current Damage", "This is the current damage."));
			EditorGUILayout.PropertyField (curMoveSpeed, new GUIContent ("Current Movement Speed", "This is your current movement speed."));
			EditorGUILayout.PropertyField (curHealth, new GUIContent ("Current Health", "This is the current health."));
			EditorGUILayout.PropertyField (curMana, new GUIContent ("Current Mana", "This is your current mana."));
			EditorGUILayout.PropertyField (maxHealth, new GUIContent ("Max Health", "This is your max health."));
			EditorGUILayout.PropertyField (maxMana, new GUIContent ("Max Mana", "This is your max mana."));
			EditorGUI.indentLevel--;

			// Apply
			serializedObject.ApplyModifiedProperties ();
		}
	}
}
