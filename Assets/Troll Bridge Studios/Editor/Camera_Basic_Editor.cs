using UnityEngine;
using System.Collections;
using UnityEditor;

namespace TrollBridge {

	[CanEditMultipleObjects]
	[CustomEditor(typeof(Camera_Basic))]
	public class Camera_Basic_Editor : Editor {

		SerializedProperty cameraWidth;
		SerializedProperty cameraHeight;

		void OnEnable(){
			cameraWidth = serializedObject.FindProperty ("cameraWidth");
			cameraHeight = serializedObject.FindProperty ("cameraHeight");
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
			// Set the layout.
			EditorGUILayout.PropertyField(cameraWidth);
			EditorGUILayout.PropertyField(cameraHeight);
			// Decrease the Indent of the lines.
			EditorGUI.indentLevel--;

			// Apply
			serializedObject.ApplyModifiedProperties();
		}
	}
}
