using UnityEngine;
using System.Collections;
using UnityEditor;

namespace TrollBridge {

	[CanEditMultipleObjects]
	[CustomEditor(typeof(Terrain_Pushable))]
	public class Terrain_Pushable_Editor : Editor {

		SerializedProperty soundClip;
		SerializedProperty minPitch;
		SerializedProperty maxPitch;
		SerializedProperty movable;
		SerializedProperty LayersThatMoveIt;
		SerializedProperty timeToPush;
		SerializedProperty moveSpeed;
		SerializedProperty showRaycast;
		SerializedProperty up;
		SerializedProperty down;
		SerializedProperty left;
		SerializedProperty right;


		void OnEnable(){
			soundClip = serializedObject.FindProperty ("soundClip");
			minPitch = serializedObject.FindProperty ("minPitch");
			maxPitch = serializedObject.FindProperty ("maxPitch");
			movable = serializedObject.FindProperty ("movable");
			LayersThatMoveIt = serializedObject.FindProperty ("LayersThatMoveIt");
			timeToPush = serializedObject.FindProperty ("timeToPush");
			moveSpeed = serializedObject.FindProperty ("moveSpeed");
			showRaycast = serializedObject.FindProperty ("showRaycast");
			up = serializedObject.FindProperty ("up");
			down = serializedObject.FindProperty ("down");
			left = serializedObject.FindProperty ("left");
			right = serializedObject.FindProperty ("right");
		}

		public override void OnInspectorGUI(){
			// Set the indentLevel to 0 as default (no indent).
			EditorGUI.indentLevel = 0;
			// Update
			serializedObject.Update();

			// Sound Label.
			EditorGUILayout.LabelField("Sound", EditorStyles.boldLabel);
			// Increase the Indent.
			EditorGUI.indentLevel++;
			// The audio clip.
			EditorGUILayout.PropertyField(soundClip ,new GUIContent("Sound Clip", "The sound clip to play while pushing."));
			// The minimum pitch.
			EditorGUILayout.PropertyField(minPitch ,new GUIContent("Minimum Pitch", "The minimum pitch this sound can be played at.  A random number between the minPitch and a maxPitch will be chosen."));
			// The maximum pitch.
			EditorGUILayout.PropertyField(maxPitch, new GUIContent("Maximum Pitch", "The maximum pitch this sound can be played at.  A random number between the minPitch and a maxPitch will be chosen."));
			// Decrease the Indent.
			EditorGUI.indentLevel--;


			// Movement Label.
			EditorGUILayout.LabelField("Movement", EditorStyles.boldLabel);
			// Increase the Indent.
			EditorGUI.indentLevel++;
			// The movable toggle.
			EditorGUILayout.PropertyField(movable, new GUIContent("Is Movable", "Is this a movable GameObject right now."));
			// The field.
			EditorGUILayout.PropertyField(LayersThatMoveIt, new GUIContent("Interactable Layers", "Which layers can actually move this GameObject."));
			EditorGUILayout.PropertyField(timeToPush, new GUIContent("Time Till Push", "The delay time it takes to push this GameObject."));
			EditorGUILayout.PropertyField(moveSpeed, new GUIContent("Move Speed", "The speed at which this gets moved when being pushed."));
			// Decrease the Indent.
			EditorGUI.indentLevel--;


			// Raycast Label
			EditorGUILayout.LabelField("Raycast", EditorStyles.boldLabel);
			// Increase the indent.
			EditorGUI.indentLevel++;
			// Do we want to show the raycast in the scene view
			EditorGUILayout.PropertyField(showRaycast, new GUIContent("Show Raycast", "Show the Raycast in the Scene View while the game is playing."));
			// The raycast point above the object.
			EditorGUILayout.PropertyField(up, new GUIContent("Up", "The Raycast distance above the GameObject."));
			// The raycast point below the object
			EditorGUILayout.PropertyField(down, new GUIContent("Down", "The Raycast distance below the GameObject."));
			// The raycast point to the left of the object.
			EditorGUILayout.PropertyField(left, new GUIContent("Left", "The Raycast distance left of the GameObject."));
			// The raycast point to the right of the object.
			EditorGUILayout.PropertyField(right, new GUIContent("Right", "The Raycast distance right of the GameObject."));
			// Decrease the indent.
			EditorGUI.indentLevel--;

			// apply
			serializedObject.ApplyModifiedProperties();
		}
	}
}
