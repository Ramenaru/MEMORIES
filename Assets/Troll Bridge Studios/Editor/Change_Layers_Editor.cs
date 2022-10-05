using UnityEngine;
using UnityEditor;

namespace TrollBridge {

	[CanEditMultipleObjects]
	[CustomEditor(typeof(Change_Layers))]
	public class Change_Layers_Editor : Editor {

		SerializedProperty enter;
		SerializedProperty enterLayerName;
		SerializedProperty exit;
		SerializedProperty exitLayerName;

		void OnEnable(){
			// Setup the SerializedProperties.
			enter = serializedObject.FindProperty ("enter");
			enterLayerName = serializedObject.FindProperty ("enterLayerName");
			exit = serializedObject.FindProperty ("exit");
			exitLayerName = serializedObject.FindProperty ("exitLayerName");
		}

		public override void OnInspectorGUI(){
			// Set the indentLevel to 0 as default (no indent).
			EditorGUI.indentLevel = 0;
			// Update
			serializedObject.Update();

			// Create a toggle group for On Enter Layer Changing.
			enter.boolValue = EditorGUILayout.BeginToggleGroup (new GUIContent("On Enter", "Do you want to change the layer when you enter the Trigger Collision?"), enter.boolValue);
			// Increase the indent.
			EditorGUI.indentLevel++;
			// The new layer when an On Enter collision happens.
			EditorGUILayout.PropertyField (enterLayerName, new GUIContent("Layer Name", "The new layer name when a On Enter collision happens."));
			// Decrease the indent.
			EditorGUI.indentLevel--;
			// End the toggle group.
			EditorGUILayout.EndToggleGroup ();

			// Create a toggle group for On Exit Layer Changing.
			exit.boolValue = EditorGUILayout.BeginToggleGroup (new GUIContent("On Exit", "Do you want to change the layer when you exit the Trigger Collision?"), exit.boolValue);
			// Increase the indent.
			EditorGUI.indentLevel++;
			// The new layer when an On Exit collision happens.
			EditorGUILayout.PropertyField (exitLayerName, new GUIContent("Layer Name", "The new layer name when a On Exit collision happens."));
			// Decrease the indent.
			EditorGUI.indentLevel--;
			// End the toggle group.
			EditorGUILayout.EndToggleGroup ();

			// Apply
			serializedObject.ApplyModifiedProperties();
		}
	}
}
