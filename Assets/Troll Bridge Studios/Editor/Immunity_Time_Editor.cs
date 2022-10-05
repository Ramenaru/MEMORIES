using UnityEngine;
using UnityEditor;

namespace TrollBridge {

	[CanEditMultipleObjects]
	[CustomEditor(typeof(Immunity_Time))]
	public class Immunity_Time_Editor : Editor {

		SerializedProperty invulnerabilityTime;
		SerializedProperty flashRenderer;
		SerializedProperty flashIntervalTime;
		SerializedProperty showFIT;

		void OnEnable () {
			// Setup the SerializedProperties.
			invulnerabilityTime = serializedObject.FindProperty("invulnerabilityTime");
			flashRenderer = serializedObject.FindProperty("flashRenderer");
			flashIntervalTime = serializedObject.FindProperty("flashIntervalTime");
			showFIT = serializedObject.FindProperty("showFIT");
		}

		public override void OnInspectorGUI()
		{
			// Set the indentLevel to 0 as default (no indent).
			EditorGUI.indentLevel = 0;
			// Update
			serializedObject.Update();

			EditorGUILayout.PropertyField (invulnerabilityTime, new GUIContent ("Invulnerability Time", "The amount of time before you can be damaged again after taking damage."));
			EditorGUILayout.PropertyField (flashRenderer, new GUIContent ("Flash Renderer", "Optional - The Sprite Renderer you want to have flashing."));
			// IF we need to display a flash time interval
			if(showFIT.boolValue){
				EditorGUI.indentLevel++;
				EditorGUILayout.PropertyField (flashIntervalTime, new GUIContent ("Flash Speed", "The flash speed on the Sprite Renderer 'Flash Renderer'."));
				EditorGUI.indentLevel--;
			}

			// Apply.
			serializedObject.ApplyModifiedProperties();
		}
	}
}
