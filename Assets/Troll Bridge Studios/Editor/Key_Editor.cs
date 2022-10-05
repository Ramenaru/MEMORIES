using UnityEngine;
using UnityEditor;

namespace TrollBridge {
	
	[CanEditMultipleObjects]
	[CustomEditor(typeof(Key))]
	public class Key_Editor : Editor {

		SerializedProperty key;

		void OnEnable () {
			// Setup the SerializedProperties.
			key = serializedObject.FindProperty("key");
		}

		public override void OnInspectorGUI()
		{
			// Set the indentLevel to 0 as default (no indent).
			EditorGUI.indentLevel = 0;
			// Update
			serializedObject.Update();

			EditorGUILayout.PropertyField(key.FindPropertyRelative("Array.size"));
			// IF there isnt any keys we create a default of 1 since what is the point of this script if you are not dropping anything.
			if(key.arraySize == 0){
				// Set the default size of this array.
				key.arraySize = 1;
			}

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(new GUIContent("Name", "The name of the keys."), EditorStyles.boldLabel, GUILayout.Width(180f));
			EditorGUILayout.LabelField(new GUIContent("Amount", "The amount of keys."), EditorStyles.boldLabel, GUILayout.Width(90f));
			EditorGUILayout.EndHorizontal();

			for(int i = 0; i < key.arraySize; i++)
			{
				EditorGUILayout.PropertyField(key.GetArrayElementAtIndex(i), GUIContent.none);
			}

			// Apply.
			serializedObject.ApplyModifiedProperties();
		}
	}
}
