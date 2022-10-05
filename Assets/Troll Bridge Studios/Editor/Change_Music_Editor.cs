using UnityEngine;
using UnityEditor;

namespace TrollBridge {

	[CanEditMultipleObjects]
	[CustomEditor(typeof(Change_Music))]
	public class Change_Music_Editor : Editor {

		SerializedProperty sceneNames;
		SerializedProperty fadeTransition;
		SerializedProperty fadeTime;

		void OnEnable () {
			// Setup the SerializedProperties.
			sceneNames = serializedObject.FindProperty("NameMusic");
			fadeTransition = serializedObject.FindProperty("fadeTransition");
			fadeTime = serializedObject.FindProperty("fadeTime");
		}

		public override void OnInspectorGUI(){

			// Set the indentLevel to 0 as default (no indent).
			EditorGUI.indentLevel = 0;
			// Update
			serializedObject.Update();
			// Get field for the user to put in the size of the array.
			EditorGUILayout.PropertyField(sceneNames.FindPropertyRelative("Array.size"));
			// Display the Fade Time and Fade Transition.
			fadeTransition.boolValue = EditorGUILayout.BeginToggleGroup(new GUIContent("Fade Transition", "Do you want the background music to fade in and out when changing?"), fadeTransition.boolValue);
			// Increase the indent.
			EditorGUI.indentLevel++;
			EditorGUILayout.PropertyField (fadeTime, new GUIContent("Fade Time", "The time it takes for full fade out and back in.  Half the time will be fading out and the other half will be fading the next background music in."));
			// Decrease the indent.
			EditorGUI.indentLevel--;
			EditorGUILayout.EndToggleGroup ();

			// Start a horizontal layout.
			EditorGUILayout.BeginHorizontal();
			// Label fields to describe drawer.
			EditorGUILayout.LabelField("Scene Name", EditorStyles.boldLabel, GUILayout.Width(Screen.width / 2.48f));
			EditorGUILayout.LabelField("BG Music", EditorStyles.boldLabel);
			// End horizontal layout.
			EditorGUILayout.EndHorizontal();
			// Loop through the size of the sceneNames array.
			for(int i = 0; i < sceneNames.arraySize; i++)
			{
				EditorGUILayout.PropertyField(sceneNames.GetArrayElementAtIndex(i), GUIContent.none);
			}		
			// Apply.
			serializedObject.ApplyModifiedProperties();
		}
	}
}
