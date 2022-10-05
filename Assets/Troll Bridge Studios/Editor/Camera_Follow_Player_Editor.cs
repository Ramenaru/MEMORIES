using UnityEngine;
using System.Collections;
using UnityEditor;

namespace TrollBridge {

	[CanEditMultipleObjects]
	[CustomEditor(typeof(Camera_Follow_Player))]
	public class Camera_Follow_Player_Editor : Editor {

		SerializedProperty cameraWidth;
		SerializedProperty cameraHeight;
		SerializedProperty UIVerticalOffset;

		SerializedProperty bottomCameraBorder;
		SerializedProperty leftCameraBorder;
		SerializedProperty topCameraBorder;
		SerializedProperty rightCameraBorder;

		void OnEnable(){
			cameraWidth = serializedObject.FindProperty ("cameraWidth");
			cameraHeight = serializedObject.FindProperty ("cameraHeight");
			UIVerticalOffset = serializedObject.FindProperty ("UIVerticalOffset");

			bottomCameraBorder = serializedObject.FindProperty ("bottomCameraBorder");
			leftCameraBorder = serializedObject.FindProperty ("leftCameraBorder");
			topCameraBorder = serializedObject.FindProperty ("topCameraBorder");
			rightCameraBorder = serializedObject.FindProperty ("rightCameraBorder");
		}

		public override void OnInspectorGUI(){
			// Set the indentLevel to 0 as default (no indent).
			EditorGUI.indentLevel = 0;
			// Update
			serializedObject.Update();

			// Resolution label.
			EditorGUILayout.LabelField("Camera Resolution", EditorStyles.boldLabel);
			// Increase the Indent the lines.
			EditorGUI.indentLevel++;
			// The UI Offset Option.
			EditorGUILayout.PropertyField(UIVerticalOffset);
			// Set the layout.
			EditorGUILayout.PropertyField (cameraWidth);
			EditorGUILayout.PropertyField (cameraHeight);
			// Decrease the Indent of the lines.
			EditorGUI.indentLevel--;


			// Scene Boundaries Label.
			EditorGUILayout.LabelField("Scene Boundaries", EditorStyles.boldLabel);
			// Increase the Indent the lines.
			EditorGUI.indentLevel++;
			// The top camera border.
			EditorGUILayout.PropertyField(bottomCameraBorder, new GUIContent("Bottom Boundary","Bottom border for the Camera."), true);
			// The left camera border
			EditorGUILayout.PropertyField(leftCameraBorder, new GUIContent("Left Boundary","Left border for the Camera."), true);
			// The top camera border
			EditorGUILayout.PropertyField(topCameraBorder, new GUIContent("Top Boundary","Top border for the Camera."), true);
			// The right camera border
			EditorGUILayout.PropertyField(rightCameraBorder, new GUIContent("Right Boundary","Right border for the Camera."), true);
			// Decrease the Indent of the lines.
			EditorGUI.indentLevel--;

			// Apply
			serializedObject.ApplyModifiedProperties();
		}
	}
}
