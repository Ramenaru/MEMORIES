using UnityEngine;
using UnityEditor;

namespace TrollBridge {

	[CustomPropertyDrawer (typeof (Scene_Name_BGMusic))]
	public class Scene_Name_BGMusic_Drawer : PropertyDrawer {

		// Draw the property inside the given rect
		public override void OnGUI (Rect position, SerializedProperty property, GUIContent label) {
			// Using BeginProperty / EndProperty on the parent property means that
			// prefab override logic works on the entire property.
			EditorGUI.BeginProperty (position, label, property);
			
			// Draw label
			position = EditorGUI.PrefixLabel (position, GUIUtility.GetControlID (FocusType.Passive), label);
			
			// Don't make child fields be indented
			var indent = EditorGUI.indentLevel;
			EditorGUI.indentLevel = 0;
			
			// Calculate rects
			Rect nameRect = new Rect (position.x, position.y, Screen.width / 2.65f, position.height);
			Rect musicRect = new Rect (position.x + Screen.width / 2.45f, position.y, Screen.width / 2f, position.height);
			
			// Draw fields - passs GUIContent.none to each so they are drawn without labels
			EditorGUI.PropertyField (nameRect, property.FindPropertyRelative ("Name"), GUIContent.none);
			EditorGUI.PropertyField (musicRect, property.FindPropertyRelative ("BGMusic"), GUIContent.none);
			
			// Set indent back to what it was
			EditorGUI.indentLevel = indent;
			
			EditorGUI.EndProperty ();
		}
	}
}
