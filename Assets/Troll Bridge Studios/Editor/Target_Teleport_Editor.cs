using UnityEngine;
using System.Collections;
using UnityEditor;

namespace TrollBridge {

	[CanEditMultipleObjects]
	[CustomEditor(typeof(Target_Teleport))]
	public class Target_Teleport_Editor : Editor{

		SerializedProperty targetTags;

		SerializedProperty soundClip;
		SerializedProperty minPitch;
		SerializedProperty maxPitch;

		SerializedProperty teleportStartAnimation;
		SerializedProperty teleportEndAnimation;

		SerializedProperty newLocation;

		void OnEnable(){
			targetTags = serializedObject.FindProperty ("targetTags");

			soundClip = serializedObject.FindProperty ("soundClip");
			minPitch = serializedObject.FindProperty ("minPitch");
			maxPitch = serializedObject.FindProperty ("maxPitch");

			teleportStartAnimation = serializedObject.FindProperty ("teleportStartAnimation");
			teleportEndAnimation = serializedObject.FindProperty ("teleportEndAnimation");

			newLocation = serializedObject.FindProperty ("newLocation");
		}

		public override void OnInspectorGUI(){
			// Set the indentLevel to 0 as default (no indent).
			EditorGUI.indentLevel = 0;
			// Update
			serializedObject.Update();


			// Tag Label.
			EditorGUILayout.LabelField("Tags", EditorStyles.boldLabel);
			// Increase the Indent the lines.
			EditorGUI.indentLevel++;
			// Array for the Target Tags.
			EditorGUILayout.PropertyField(targetTags, new GUIContent("Tag Name", "The GameObjects with these tags can teleport.  IF this array length is 0 then there are no restrictions on teleporting and "), true);
			// Decrease the Indent.
			EditorGUI.indentLevel--;


			// Sound Label.
			EditorGUILayout.LabelField("Sound", EditorStyles.boldLabel);
			// Increase the Indent.
			EditorGUI.indentLevel++;
			// The audio clip.
			EditorGUILayout.PropertyField(soundClip, new GUIContent("Sound Clip", "The sound clip to play when teleporting."));
			// The minimum pitch.
			EditorGUILayout.PropertyField(minPitch, new GUIContent("Minimum Pitch", "The minimum pitch this sound can be played at.  A random number between the minPitch and a maxPitch will be chosen."));
			// The maximum pitch.
			EditorGUILayout.PropertyField(maxPitch, new GUIContent("Maximum Pitch", "The maximum pitch this sound can be played at.  A random number between the minPitch and a maxPitch will be chosen."));
			// Decrease the Indent.
			EditorGUI.indentLevel--;


			// Animation Label.
			EditorGUILayout.LabelField("Animation", EditorStyles.boldLabel);
			// Increase the Indent.
			EditorGUI.indentLevel++;
			// The start animation.
			EditorGUILayout.PropertyField(teleportStartAnimation, new GUIContent("Start Animation", "Start location teleport animation."));
			// The end animation.
			EditorGUILayout.PropertyField(teleportEndAnimation, new GUIContent("End Animation", "End location teleport animation."));
			// Decrease the Indent.
			EditorGUI.indentLevel--;


			// Movement Label.
			EditorGUILayout.LabelField("Teleport", EditorStyles.boldLabel);
			// Increase the Indent.
			EditorGUI.indentLevel++;
			// The new location to be teleported to.
			EditorGUILayout.PropertyField(newLocation, new GUIContent("Teleport Location", "The location to be teleported."));
			// Decrease the Indent.
			EditorGUI.indentLevel--;


			// Apply
			serializedObject.ApplyModifiedProperties();
		}
	}
}
