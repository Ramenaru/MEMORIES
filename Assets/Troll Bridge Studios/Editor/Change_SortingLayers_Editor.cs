using UnityEngine;
using UnityEditor;
using System;
using UnityEditorInternal;
using System.Reflection;

namespace TrollBridge {

	[CanEditMultipleObjects]
	[CustomEditor(typeof(Change_SortingLayers))]
	public class Change_SortingLayers_Editor : Editor {

		SerializedProperty enter;
		SerializedProperty enterSortLayerName;
		SerializedProperty enterSortOrderNumber;
		SerializedProperty exit;
		SerializedProperty exitSortLayerName;
		SerializedProperty exitSortOrderNumber;

		SerializedProperty enterPopupSelection;
		SerializedProperty exitPopupSelection;

		void OnEnable(){
			// Setup the SerializedProperties.
			enter = serializedObject.FindProperty ("enter");
			enterSortLayerName = serializedObject.FindProperty ("enterSortLayerName");
			enterSortOrderNumber = serializedObject.FindProperty ("enterSortOrderNumber");
			exit = serializedObject.FindProperty ("exit");
			exitSortLayerName = serializedObject.FindProperty ("exitSortLayerName");
			exitSortOrderNumber = serializedObject.FindProperty ("exitSortOrderNumber");

			enterPopupSelection = serializedObject.FindProperty ("enterPopupSelection");
			exitPopupSelection = serializedObject.FindProperty ("exitPopupSelection");
		}

		public override void OnInspectorGUI(){
			// Set the indentLevel to 0 as default (no indent).
			EditorGUI.indentLevel = 0;
			// Update.
			serializedObject.Update();

			// Grab all the sorting layers.
			Type internalEditorUtilityType = typeof(InternalEditorUtility);
			PropertyInfo sortingLayersProperty = internalEditorUtilityType.GetProperty ("sortingLayerNames", BindingFlags.Static | BindingFlags.NonPublic);
			string[] sortingLayers = (string[])sortingLayersProperty.GetValue (null, new object[0]);

			// Create a toggle group for On Enter Sort Layer Changing.
			enter.boolValue = EditorGUILayout.BeginToggleGroup (new GUIContent("On Enter", "Do you want to change the sort layer when you enter the Trigger Collision?"), enter.boolValue);
			// Increase the indent.
			EditorGUI.indentLevel++;
			// The new sorting layer when an On Enter collision happens.
			enterPopupSelection.intValue = EditorGUILayout.Popup("Sort Layer", enterPopupSelection.intValue, sortingLayers);
			// Since we have the Popup Selection number we can just assign the string from our array.
			enterSortLayerName.stringValue = sortingLayers[enterPopupSelection.intValue];
	//		EditorGUILayout.PropertyField (enterSortLayerName, new GUIContent("New Sort Layer Name", "The new sorting layer name when a On Enter collision happens."));
			// The new sorting order when an On Enter collision happens.
			EditorGUILayout.PropertyField (enterSortOrderNumber, new GUIContent("New Sort Order Number", "The new sorting order number when a On Enter collision happens."));
			// Decrease the indent.
			EditorGUI.indentLevel--;
			// End the toggle group.
			EditorGUILayout.EndToggleGroup ();

			// Create a toggle group for On Exit Sort Layer Changing.
			exit.boolValue = EditorGUILayout.BeginToggleGroup (new GUIContent("On Exit", "Do you want to change the sort layer when you exit the Trigger Collision?"), exit.boolValue);
			// Increase the indent.
			EditorGUI.indentLevel++;
			// The new sorting layer when an On Enter collision happens.
			exitPopupSelection.intValue = EditorGUILayout.Popup("Sort Layer", exitPopupSelection.intValue, sortingLayers);
			// Since we have the Popup Selection number we can just assign the string from our array.
			exitSortLayerName.stringValue = sortingLayers[exitPopupSelection.intValue];
	//		EditorGUILayout.PropertyField (exitSortLayerName, new GUIContent("New Sort Layer Name", "The new layer name when a On Exit collision happens."));
			// The new sorting order when an On Exit collision happens.
			EditorGUILayout.PropertyField (exitSortOrderNumber, new GUIContent("New Sort Order Number", "The new sorting order number when a On Exit collision happens."));
			// Decrease the indent.
			EditorGUI.indentLevel--;
			// End the toggle group.
			EditorGUILayout.EndToggleGroup ();

			// Apply
			serializedObject.ApplyModifiedProperties();
		}
	}
}
