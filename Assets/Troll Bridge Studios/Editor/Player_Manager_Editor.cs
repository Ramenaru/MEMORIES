using UnityEngine;
using System.Collections;
using UnityEditor;

namespace TrollBridge {

	[CanEditMultipleObjects]
	[CustomEditor(typeof(Player_Manager))]
	public class Player_Manager_Editor : Editor {

		SerializedProperty characterType;
		SerializedProperty canBeJolted;
		SerializedProperty interactionKey;
		SerializedProperty attackKey;
		SerializedProperty characterEntity;

		SerializedProperty hitAnimationTime;

//	//	SerializedProperty baseDamage;
//		SerializedProperty defaultDamage;
//		SerializedProperty curDamage;
//
//		SerializedProperty defaultHealth;
//		SerializedProperty defaultMaxHealth;
//	//	SerializedProperty baseHealth;
//		SerializedProperty maxHealth;
//		SerializedProperty curHealth;
//
//		SerializedProperty defaultMana;
//		SerializedProperty defaultMaxMana;
//	//	SerializedProperty baseMana;
//		SerializedProperty maxMana;
//		SerializedProperty curMana;
//
//		SerializedProperty defaultMoveSpeed;
//	//	SerializedProperty baseMoveSpeed;
//		SerializedProperty curMoveSpeed;

		SerializedProperty getHitSound;
		SerializedProperty dieSound;
		SerializedProperty attackSound;

		void OnEnable(){
			characterType = serializedObject.FindProperty ("characterType");
			canBeJolted = serializedObject.FindProperty ("CanBeJolted");

			interactionKey = serializedObject.FindProperty ("InteractionKey");
			attackKey = serializedObject.FindProperty ("AttackKey");

			characterEntity = serializedObject.FindProperty ("characterEntity");

			hitAnimationTime = serializedObject.FindProperty ("HitAnimationTime");

//			defaultDamage = serializedObject.FindProperty ("DefaultDamage");
//	//		baseDamage = serializedObject.FindProperty ("BaseDamage");
//			curDamage = serializedObject.FindProperty ("CurrentDamage");
//
//			defaultHealth = serializedObject.FindProperty ("DefaultHealth");
//			defaultMaxHealth = serializedObject.FindProperty ("DefaultMaxHealth");
//	//		baseHealth = serializedObject.FindProperty ("BaseHealth");
//			maxHealth = serializedObject.FindProperty ("MaxHealth");
//			curHealth = serializedObject.FindProperty ("CurrentHealth");
//
//			defaultMana = serializedObject.FindProperty ("DefaultMana");
//			defaultMaxMana = serializedObject.FindProperty ("DefaultMaxMana");
//	//		baseMana = serializedObject.FindProperty ("BaseMana");
//			maxMana = serializedObject.FindProperty ("MaxMana");
//			curMana = serializedObject.FindProperty ("CurrentMana");
//
//			defaultMoveSpeed = serializedObject.FindProperty ("DefaultMoveSpeed");
//	//		baseMoveSpeed = serializedObject.FindProperty ("BaseMoveSpeed");
//			curMoveSpeed = serializedObject.FindProperty ("CurrentMoveSpeed");


			getHitSound = serializedObject.FindProperty ("GetHitSound");
			dieSound = serializedObject.FindProperty ("DieSound");
			attackSound = serializedObject.FindProperty ("AttackSound");
		}

		public override void OnInspectorGUI(){
			// Set the indentLevel to 0 as default (no indent).
			EditorGUI.indentLevel = 0;
			// Update.
			serializedObject.Update ();
			// Set the label width.
			EditorGUIUtility.labelWidth = 180f;

			EditorGUILayout.PropertyField (characterType, new GUIContent ("Character Type", "What type of character do you want this GameObject to be?"));
			EditorGUILayout.PropertyField (canBeJolted, new GUIContent ("Can Be Knocked Back", "Does this character suffer from a knockback when taking damage?"));
			EditorGUILayout.PropertyField (interactionKey, new GUIContent ("Interaction Key", "The key you can set to activate interactions with your environment."));
			EditorGUILayout.PropertyField (attackKey, new GUIContent ("Attack Key", "The key you can set to activate attacking for your character."));
			EditorGUILayout.PropertyField (characterEntity, new GUIContent ("Character GameObject", "The character that is associated to this Manager."));

//			EditorGUILayout.LabelField ("Player Default Stats", EditorStyles.boldLabel);
//			EditorGUI.indentLevel++;
//			EditorGUILayout.PropertyField (defaultDamage, new GUIContent ("Default Damage", "This is the default damage in which this character starts off with."));
//			EditorGUILayout.PropertyField (defaultHealth, new GUIContent ("Default Health", "This is the default health in which this character starts off with."));
//			EditorGUILayout.PropertyField (defaultMaxHealth, new GUIContent ("Default Max Health", "This is the default max health in which this character starts off with."));
//			EditorGUILayout.PropertyField (defaultMana, new GUIContent ("Default Mana", "This is the default mana in which this character starts off with."));
//			EditorGUILayout.PropertyField (defaultMaxMana, new GUIContent ("Default Max Mana", "This is the default max mana in which this character starts off with."));
//			EditorGUILayout.PropertyField (defaultMoveSpeed, new GUIContent ("Default Movement Speed", "This is the default movement speed in which this character starts off with."));
//			EditorGUI.indentLevel--;

	//		EditorGUILayout.LabelField ("Player Base Stats", EditorStyles.boldLabel);
	//		EditorGUI.indentLevel++;
	//		EditorGUILayout.PropertyField (baseDamage, new GUIContent ("Base Damage", "This is your base damage. Any multiplier from items/skills will be added to this and will result in a change in your Current Damage."));
	//		EditorGUILayout.PropertyField (baseHealth, new GUIContent ("Base Health", "This is your base health.  Any multiplier from items/skills will be added to this and will result in a change in your Max Health."));
	//		EditorGUILayout.PropertyField (baseMana, new GUIContent ("Base Mana", "This is your base mana.  Any multiplier from items/skills will be added to this and will result in a change in your Max Mana."));
	//		EditorGUILayout.PropertyField (baseMoveSpeed, new GUIContent ("Base Movement Speed", "This is your base movement speed.  Any multiplier from items/skills will be added to this and will result in a change in your Current Movement Speed."));
	//		EditorGUI.indentLevel--;

//			EditorGUILayout.LabelField ("Player Current Stats", EditorStyles.boldLabel);
//			EditorGUI.indentLevel++;
//			EditorGUILayout.PropertyField (curDamage, new GUIContent ("Current Damage", "This is your current damage. This is the result of any items/skills and your Base Damage."));
//			EditorGUILayout.PropertyField (curHealth, new GUIContent ("Current Health", "This is your current health."));
//			EditorGUILayout.PropertyField (maxHealth, new GUIContent ("Max Health", "This is your max health.  This is the result of any items/skills and your Base Health."));
//			EditorGUILayout.PropertyField (curMana, new GUIContent ("Current Mana", "This is your current mana."));
//			EditorGUILayout.PropertyField (maxMana, new GUIContent ("Max Mana", "This is your max mana.  This is the result of any items/skills and your Base Mana."));
//			EditorGUILayout.PropertyField (curMoveSpeed, new GUIContent ("Current Movement Speed", "This is your current movement speed."));
//			EditorGUI.indentLevel--;

			EditorGUILayout.LabelField ("Player Sounds", EditorStyles.boldLabel);
			EditorGUI.indentLevel++;
			EditorGUILayout.PropertyField (getHitSound, new GUIContent ("Get Hit Sound", "The sound that is made when this GameObject takes damage."));
			EditorGUILayout.PropertyField (dieSound, new GUIContent ("Die Sound", "The sound that is made when this GameObject dies."));
			EditorGUILayout.PropertyField (attackSound, new GUIContent ("Attack Sound", "The sound that is made when this GameObject attacks."));
			EditorGUI.indentLevel--;

			EditorGUILayout.LabelField ("Player Getting Hit", EditorStyles.boldLabel);
			EditorGUI.indentLevel++;
			EditorGUILayout.PropertyField (hitAnimationTime, new GUIContent ("Hit Animation Time", "Optional - The time your character is in its 'Take Damage' / 'Getting Hit' Animation state."));
			EditorGUI.indentLevel--;

			// Apply
			serializedObject.ApplyModifiedProperties ();
		}
	}
}
