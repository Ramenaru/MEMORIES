using UnityEngine;
using UnityEditor;

namespace TrollBridge {

	[CanEditMultipleObjects]
	[CustomEditor(typeof(Chest))]
	public class Chest_Editor : Editor {

		SerializedProperty openChest;
		SerializedProperty closedChest;
		SerializedProperty openChestSound;
		SerializedProperty chestLoot;
		SerializedProperty requireKeyToOpen;
		SerializedProperty ckc;

		void OnEnable () 
		{
			// Setup the SerializedProperties.
			openChest = serializedObject.FindProperty("openChest");
			closedChest = serializedObject.FindProperty("closedChest");
			openChestSound = serializedObject.FindProperty("openChestSound");
			chestLoot = serializedObject.FindProperty("chestLoot");
			requireKeyToOpen = serializedObject.FindProperty("requireKeyToOpen");
			ckc = serializedObject.FindProperty("ckc");
		}

		public override void OnInspectorGUI()
		{

			// Set the indentLevel to 0 as default (no indent).
			EditorGUI.indentLevel = 0;
			// Update
			serializedObject.Update();

			EditorGUILayout.PropertyField (openChest);
			EditorGUILayout.PropertyField (closedChest);
			EditorGUILayout.PropertyField (openChestSound);
			EditorGUILayout.PropertyField (chestLoot);

			requireKeyToOpen.boolValue = EditorGUILayout.BeginToggleGroup (new GUIContent("Require Key To Open", "Does this chest require a key to open."), requireKeyToOpen.boolValue);
			// Increase the indent.
			EditorGUI.indentLevel++;

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(new GUIContent("Key Name", "The name of the key."), EditorStyles.boldLabel, GUILayout.Width(180f));
			EditorGUILayout.LabelField(new GUIContent("Consume", "Does this key get consumed when used."), EditorStyles.boldLabel, GUILayout.Width(90f));
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.PropertyField(ckc, GUIContent.none);
			// Decrease the indent.
			EditorGUI.indentLevel--;
			// End the ToggleGroup.
			EditorGUILayout.EndToggleGroup ();

			// Apply.
			serializedObject.ApplyModifiedProperties();
		}
	}
}
