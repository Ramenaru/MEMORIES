using UnityEngine;
using System.Collections;
using UnityEditor;

namespace TrollBridge {

	[CanEditMultipleObjects]
	[CustomEditor(typeof(Animation_Direction))]
	public class Animation_Direction_Editor : Editor {

		SerializedProperty flipToFace;
		SerializedProperty flipX;
		SerializedProperty flipY;


		void OnEnable(){
			flipToFace = serializedObject.FindProperty ("FlipToFace");
			flipX = serializedObject.FindProperty ("FlipX");
			flipY = serializedObject.FindProperty ("FlipY");
		}

		public override void OnInspectorGUI(){
			// Set the indentLevel to 0 as default (no indent).
			EditorGUI.indentLevel = 0;
			// Update.
			serializedObject.Update ();
			// Set the label width.
			EditorGUIUtility.labelWidth = 160f;

			flipToFace.boolValue = EditorGUILayout.BeginToggleGroup (new GUIContent ("Flip To Face", "Set to 'true' if the animation to this GameObject only has 1 direction to look but you want it to be used in 2 directions if you were to 'flip' the animation.\n\n" +
				"IF this GameObject has a 4 direction ('Four Base' layer name) or 8 direction ('Eight Base' layer name) animation leave this false as this will take care of the direction this GameObject is moving."), flipToFace.boolValue);
			EditorGUI.indentLevel++;
			EditorGUILayout.PropertyField (flipX, new GUIContent ("Flip X", "Set to 'true' if you want this animation to be flipped on the X to face the direction it is moving."));
			EditorGUILayout.PropertyField (flipY, new GUIContent ("Flip Y", "Set to 'true' if you want this animation to be flipped on the Y to face the direction it is moving."));
			EditorGUI.indentLevel--;
			EditorGUILayout.EndToggleGroup ();

			// Apply
			serializedObject.ApplyModifiedProperties ();
		}
	}
}
