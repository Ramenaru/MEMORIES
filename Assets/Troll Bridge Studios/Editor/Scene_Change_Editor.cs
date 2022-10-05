using UnityEngine;
using System.Collections;
using UnityEditor;

namespace TrollBridge {
	
	[CanEditMultipleObjects]
	[CustomEditor(typeof(Scene_Change))]
	public class Scene_Change_Editor : Editor {

		SerializedProperty targetTags;
		SerializedProperty newScene;
		SerializedProperty sceneSpawnLocation;

		void OnEnable(){
			targetTags = serializedObject.FindProperty ("targetTags");
			newScene = serializedObject.FindProperty ("newScene");
			sceneSpawnLocation = serializedObject.FindProperty ("sceneSpawnLocation");
		}

		public override void OnInspectorGUI(){
			// Set the indentLevel to 0 as default (no indent).
			EditorGUI.indentLevel = 0;
			// Update
			serializedObject.Update();
			
			// The Tag label.
			EditorGUILayout.LabelField("Tags", EditorStyles.boldLabel);
			// Increase the Indent the lines.
			EditorGUI.indentLevel++;
			// The tags that can activate the scene change.
			EditorGUILayout.PropertyField(targetTags, new GUIContent("Tags That Activate", "The tags that can only activate the Scene Change."), true);
			// Decrease the Indent of the lines.
			EditorGUI.indentLevel--;

			// Edit the widths of the fields and labels.
			EditorGUIUtility.labelWidth = 180f;
			EditorGUIUtility.fieldWidth = 20f;
			EditorGUILayout.LabelField("Scene Change", EditorStyles.boldLabel);
			// Increase the Indent the lines.
			EditorGUI.indentLevel++;
			EditorGUILayout.PropertyField(newScene, new GUIContent("New Scene Name", "The name of the scene that will be loaded."));
			EditorGUILayout.PropertyField(sceneSpawnLocation, new GUIContent("Scene Spawn Location", "The location in the new scene that the player will be spawned at.  This number is correlated to the Transform locations on your Scene Manager Script."));
			// Decrease the Indent of the lines.
			EditorGUI.indentLevel--;

			// Apply
			serializedObject.ApplyModifiedProperties();
		}
	}
}
