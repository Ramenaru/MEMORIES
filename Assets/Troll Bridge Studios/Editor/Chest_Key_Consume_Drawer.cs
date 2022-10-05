using UnityEngine;
using UnityEditor;

namespace TrollBridge {

	[CustomPropertyDrawer (typeof (Chest_Key_Consume))]
	public class Chest_Key_Consume_Drawer : PropertyDrawer {

		// Draw the property inside the given rect
		public override void OnGUI (Rect position, SerializedProperty property, GUIContent label) {
			// Using BeginProperty / EndProperty on the parent property means that
			// prefab override logic works on the entire property.
			EditorGUI.BeginProperty (position, label, property);

			// Draw label
			position = EditorGUI.PrefixLabel (position, GUIUtility.GetControlID (FocusType.Passive), label);

			// Don't make child fields be indented.
			var indent = EditorGUI.indentLevel;
			EditorGUI.indentLevel = 0;

			// Calculate rects.
			Rect name = new Rect (position.x, position.y, 175f, position.height);
			Rect consume = new Rect (position.x + 210f, position.y, 70f, position.height);

			// Draw fields - passs GUIContent.none to each so they are drawn without labels.
			EditorGUI.PropertyField (name, property.FindPropertyRelative ("name"), GUIContent.none);
			EditorGUI.PropertyField (consume, property.FindPropertyRelative ("consume"), GUIContent.none);

			// Set indent back to what it was.
			EditorGUI.indentLevel = indent;

			EditorGUI.EndProperty ();
		}
	}
}
