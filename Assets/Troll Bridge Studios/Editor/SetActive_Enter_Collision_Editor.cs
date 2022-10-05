using UnityEngine;
using System.Collections;
using UnityEditor;

namespace TrollBridge {

	[CanEditMultipleObjects]
	[CustomEditor(typeof(SetActive_Enter_Collision))]
	public class SetActive_Enter_Collision_Editor : Editor {

		SerializedProperty setInactive;
		SerializedProperty activateGameObjects;
		SerializedProperty onEnterGameObjects;

		void OnEnable(){
			setInactive = serializedObject.FindProperty ("setInactive");
			activateGameObjects = serializedObject.FindProperty ("activateGameObjects");
			onEnterGameObjects = serializedObject.FindProperty ("onEnterGameObjects");
		}


		public override void OnInspectorGUI(){
			// Set the indentLevel to 0 as default (no indent).
			EditorGUI.indentLevel = 0;
			// Update.
			serializedObject.Update ();
			// Set the label width.
			EditorGUIUtility.labelWidth = 180f;

			EditorGUILayout.LabelField ("Set Targeted GameObjects Active or Inactive", EditorStyles.boldLabel);
			EditorGUI.indentLevel++;
			EditorGUILayout.PropertyField (setInactive, new GUIContent ("Set Inactive", "Set to 'true' if you want to set the Targeted GameObjects InActive else it will make the Targeted GameObjects Active on Entering Collision."));
			EditorGUI.indentLevel--;

			EditorGUILayout.LabelField ("Trigger GameObjects", EditorStyles.boldLabel);
			EditorGUI.indentLevel++;
			EditorGUILayout.PropertyField (activateGameObjects, new GUIContent ("Trigger GameObjects", "The GameObjects that trigger the Event on entering collision."), true);
			EditorGUI.indentLevel--;

			EditorGUILayout.LabelField ("Targeted GameObjects", EditorStyles.boldLabel);
			EditorGUI.indentLevel++;
			EditorGUILayout.PropertyField (onEnterGameObjects, new GUIContent ("Target GameObjects", "The GameObjects that will be altered."), true);
			EditorGUI.indentLevel--;

			// Apply
			serializedObject.ApplyModifiedProperties ();
		}
	}
}
