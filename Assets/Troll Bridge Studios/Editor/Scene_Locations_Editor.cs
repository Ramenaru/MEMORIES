using UnityEngine;
using System.Collections;
using UnityEditor;

namespace TrollBridge {
	
	[CanEditMultipleObjects]
	[CustomEditor(typeof(Scene_Locations))]
	public class Scene_Locations_Editor : Editor {

		SerializedProperty sceneSpawnLocations;

		void OnEnable(){
			sceneSpawnLocations = serializedObject.FindProperty ("sceneSpawnLocations");
		}

		public override void OnInspectorGUI(){
			// Set the indentLevel to 0 as default (no indent).
			EditorGUI.indentLevel = 0;
			// Update
			serializedObject.Update();


			// General Label.
			EditorGUILayout.LabelField("Spawn Locations", EditorStyles.boldLabel);
			// Increase the indent.
			EditorGUI.indentLevel++;
			// Set the layout.
			EditorGUILayout.PropertyField(sceneSpawnLocations, new GUIContent("Scene Spawn Locations" , "The list of Transform locations that you can spawn at when transferring to this scene."), true);
			// Decrease the Indent of the lines.
			EditorGUI.indentLevel--;

			// apply
			serializedObject.ApplyModifiedProperties();
		}
	}
}
