using UnityEngine;
using UnityEditor;

namespace TrollBridge {
	
	[CanEditMultipleObjects]
	[CustomEditor(typeof(Money))]
	public class Money_Editor : Editor {

		SerializedProperty currency;

		void OnEnable () {
			// Setup the SerializedProperties.
			currency = serializedObject.FindProperty("currency");
		}

		public override void OnInspectorGUI(){

			// Set the indentLevel to 0 as default (no indent).
			EditorGUI.indentLevel = 0;
			// Update
			serializedObject.Update();

			EditorGUILayout.PropertyField(currency.FindPropertyRelative("Array.size"));
			// IF there isnt any money we create a default of 1 since what is the point of this script if you are not dropping anything.
			if(currency.arraySize == 0){
				// Set the default size of this array.
				currency.arraySize = 1;
			}

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(new GUIContent("Name", "The name of the currency."), EditorStyles.boldLabel, GUILayout.Width(180f));
			EditorGUILayout.LabelField(new GUIContent("Amount", "The amount of the currency."), EditorStyles.boldLabel, GUILayout.Width(90f));
			EditorGUILayout.EndHorizontal();

			for(int i = 0; i < currency.arraySize; i++)
			{
				EditorGUILayout.PropertyField(currency.GetArrayElementAtIndex(i), GUIContent.none);
			}

			// Apply.
			serializedObject.ApplyModifiedProperties();
		}
	}
}
