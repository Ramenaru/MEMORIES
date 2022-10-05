using UnityEngine;
using UnityEditor;

namespace TrollBridge {

	[CanEditMultipleObjects]
	[CustomEditor(typeof(Loot))]
	public class Loot_Editor : Editor {

		SerializedProperty lootDropped;

		void OnEnable () {
			// Setup the SerializedProperties.
			lootDropped = serializedObject.FindProperty("LootDropped");
		}

		public override void OnInspectorGUI(){

			// Set the indentLevel to 0 as default (no indent).
			EditorGUI.indentLevel = 0;
			// Update
			serializedObject.Update();

			EditorGUILayout.PropertyField(lootDropped.FindPropertyRelative("Array.size"));
			// IF there isnt any loot we create a default of 1 since what is the point of this script if you are not dropping anything.
			if(lootDropped.arraySize == 0){
				// Set the default size of this array.
				lootDropped.arraySize = 1;
			}

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(new GUIContent("Loot GameObject", "The loot (GameObjects) that will be dropped when this GameObject dies."), EditorStyles.boldLabel, GUILayout.Width(180f));
			EditorGUILayout.LabelField(new GUIContent("Amount", "The amount of loot that is dropped."), EditorStyles.boldLabel, GUILayout.Width(90f));
			EditorGUILayout.LabelField(new GUIContent("Percent", "The percent chance to drop the loot.  You can input any float from 0-100 (Example : 0.001, 5, 12.66, 90.25)."), EditorStyles.boldLabel, GUILayout.Width(70f));
			EditorGUILayout.EndHorizontal();

			for(int i = 0; i < lootDropped.arraySize; i++)
			{
				EditorGUILayout.PropertyField(lootDropped.GetArrayElementAtIndex(i), GUIContent.none);
			}

			// Apply.
			serializedObject.ApplyModifiedProperties();
		}
	}
}
