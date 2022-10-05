using UnityEngine;
using System.Collections;
using UnityEditor;

namespace TrollBridge {

	[CanEditMultipleObjects]
	[CustomEditor(typeof(Terrain_Sound_By_Distance))]
	public class Terrain_Sound_By_Distance_Editor : Editor {

		SerializedProperty soundClip;
		SerializedProperty minPitch;
		SerializedProperty maxPitch;
		SerializedProperty distance;

		void OnEnable(){
			soundClip = serializedObject.FindProperty ("soundClip");
			minPitch = serializedObject.FindProperty ("minPitch");
			maxPitch = serializedObject.FindProperty ("maxPitch");
			distance = serializedObject.FindProperty ("distance");
		}

		public override void OnInspectorGUI(){
			// Set the indentLevel to 0 as default (no indent).
			EditorGUI.indentLevel = 0;
			// Update
			serializedObject.Update();

			EditorGUILayout.LabelField("Sound", EditorStyles.boldLabel);
			// Increase the Indent the lines.
			EditorGUI.indentLevel++;
			// Layout.
			EditorGUILayout.PropertyField(soundClip, new GUIContent("Sound Clip", "The sound clip to play when Colliding."));
			EditorGUILayout.PropertyField(minPitch, new GUIContent("Minimum Pitch", "The minimum pitch this sound can be played at."));
			EditorGUILayout.PropertyField(maxPitch, new GUIContent("Maximum Pitch", "The maximum pitch this sound can be played at."));
			// Decrease the Indent of the lines.
			EditorGUI.indentLevel--;

			EditorGUILayout.LabelField("Distance For Sound", EditorStyles.boldLabel);
			// Increase the Indent the lines.
			EditorGUI.indentLevel++;
			// Layout.
			EditorGUILayout.PropertyField(distance, new GUIContent("Distance", "The distance traveled before another sound clip plays."));
			// Decrease the Indent of the lines.
			EditorGUI.indentLevel--;

			// Apply
			serializedObject.ApplyModifiedProperties();
		}
	}
}
