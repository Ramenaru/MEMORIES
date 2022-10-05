using UnityEngine;
using System.Collections;
using UnityEditor;

namespace TrollBridge {

	[CanEditMultipleObjects]
	[CustomEditor(typeof(Camera_Static_Slide))]
	public class Camera_Static_Slide_Editor : Editor{

		SerializedProperty cameraWidth;
		SerializedProperty cameraHeight;
		SerializedProperty UIVerticalOffset;

		SerializedProperty playerSlideAmount;
		SerializedProperty cameraSlideSpeed;

		SerializedProperty bottomCameraBorder;
		SerializedProperty leftCameraBorder;

		void OnEnable(){
			cameraWidth = serializedObject.FindProperty ("cameraWidth");
			cameraHeight = serializedObject.FindProperty ("cameraHeight");
			UIVerticalOffset = serializedObject.FindProperty ("UIVerticalOffset");

			playerSlideAmount = serializedObject.FindProperty ("playerSlideAmount");
			cameraSlideSpeed = serializedObject.FindProperty ("cameraSlideSpeed");

			bottomCameraBorder = serializedObject.FindProperty ("bottomCameraBorder");
			leftCameraBorder = serializedObject.FindProperty ("leftCameraBorder");
		}

		public override void OnInspectorGUI(){
			// Set the indentLevel to 0 as default (no indent).
			EditorGUI.indentLevel = 0;
			// Update
			serializedObject.Update();
			// Edit the inspector.
			EditorGUIUtility.labelWidth = 160f;

			// Resolution label.
			EditorGUILayout.LabelField("Camera Resolution", EditorStyles.boldLabel);
			// Increase the Indent the lines.
			EditorGUI.indentLevel++;
			// The UI Offset Option.
			EditorGUILayout.PropertyField(UIVerticalOffset);
			// Set the layout.
			EditorGUILayout.PropertyField(cameraWidth);
			EditorGUILayout.PropertyField(cameraHeight);
			// Decrease the Indent of the lines.
			EditorGUI.indentLevel--;

			// Camera Slide Movement label.
			EditorGUILayout.LabelField("Camera Panning", EditorStyles.boldLabel);
			// Increase the Indent the lines.
			EditorGUI.indentLevel++;
			// Set the layout.
			EditorGUILayout.PropertyField(playerSlideAmount);
			EditorGUILayout.PropertyField(cameraSlideSpeed);
			// Decrease the Indent of the lines.
			EditorGUI.indentLevel--;


			// Scene Border label.
			EditorGUILayout.LabelField("Scene Boundaries", EditorStyles.boldLabel);
			// Increase the Indent the lines.
			EditorGUI.indentLevel++;
			// The top camera boundary.
			EditorGUILayout.PropertyField(bottomCameraBorder, new GUIContent("Bottom Boundary","Setting the bottom and left boundaries will automatically adjust your camera positioning."), true);
			// The left camera boundary
			EditorGUILayout.PropertyField(leftCameraBorder, new GUIContent("Left Boundary","Setting the bottom and left boundaries will automatically adjust your camera positioning."), true);
			// Decrease the Indent of the lines.
			EditorGUI.indentLevel--;

			// apply
			serializedObject.ApplyModifiedProperties();
		}
	}
}
