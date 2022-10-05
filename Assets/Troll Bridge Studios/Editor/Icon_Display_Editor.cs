using UnityEngine;
using System.Collections;
using UnityEditor;

namespace TrollBridge {

	[CanEditMultipleObjects]
	[CustomEditor(typeof(Icon_Display))]
	public class Icon_Display_Editor : Editor {
		
		SerializedProperty Icon;
		SerializedProperty iconLocation;

		SerializedProperty BounceIcon;
		SerializedProperty BounceDistance;
		SerializedProperty BounceTime;

		SerializedProperty PulseIcon;
		SerializedProperty PulseIntensity;
		SerializedProperty PulseTime;

		SerializedProperty SpinIcon;
		SerializedProperty SpinXSpeed;
		SerializedProperty SpinYSpeed;

		void OnEnable(){
			Icon = serializedObject.FindProperty ("Icon");
			iconLocation = serializedObject.FindProperty ("iconLocation");

			BounceIcon = serializedObject.FindProperty ("BounceIcon");
			BounceDistance = serializedObject.FindProperty ("BounceDistance");
			BounceTime = serializedObject.FindProperty ("BounceTime");

			PulseIcon = serializedObject.FindProperty ("PulseIcon");
			PulseIntensity = serializedObject.FindProperty ("PulseIntensity");
			PulseTime = serializedObject.FindProperty ("PulseTime");

			SpinIcon = serializedObject.FindProperty ("SpinIcon");
			SpinXSpeed = serializedObject.FindProperty ("SpinXSpeed");
			SpinYSpeed = serializedObject.FindProperty ("SpinYSpeed");
		}

		public override void OnInspectorGUI(){
			// Set the indentLevel to 0 as default (no indent).
			EditorGUI.indentLevel = 0;
			// Update
			serializedObject.Update();

			// The Dialogue title.
			EditorGUILayout.LabelField("Icon", EditorStyles.boldLabel);
			// Increase the indent.
			EditorGUI.indentLevel++;
			// Set the Icon GameObject.
			EditorGUILayout.PropertyField(Icon, new GUIContent("Icon", "The GameObject to appear as the icon."), true);
			// Set the distance above the gameobject where the icon is placed.
			EditorGUILayout.PropertyField(iconLocation, new GUIContent("Icon Spawn Location", "The initial location the Icon will spawn at."));
			// Decrease the indent.
			EditorGUI.indentLevel--;


			EditorGUILayout.Space();


			// The Icon Movement Title.
			EditorGUILayout.LabelField("Icon Movements", EditorStyles.boldLabel);
			// Increase the indent.
			EditorGUI.indentLevel++;
			// Set the toggle group for a bouncing icon.
			BounceIcon.boolValue = EditorGUILayout.BeginToggleGroup(new GUIContent("Bounce Icon", "Set to 'true' if you want the icon's movement to bounce."), BounceIcon.boolValue);
			// Increase the indent.
			EditorGUI.indentLevel++;
			// Set the bounce distance for the icon.
			EditorGUILayout.PropertyField(BounceDistance, new GUIContent("Bounce Distance", "The distance the icon moves before it moves back to its original location."));
			// Set the Bounce time for the icon.
			EditorGUILayout.PropertyField(BounceTime, new GUIContent("Bounce Time", "The time it takes for the icon to move a distance of 'Bounce Distance'"));
			// Decrease the Indent.
			EditorGUI.indentLevel--;
			// End the Toggle Group.
			EditorGUILayout.EndToggleGroup();


			EditorGUILayout.Space();


			// Set the toggle group for a pulsing icon.
			PulseIcon.boolValue = EditorGUILayout.BeginToggleGroup(new GUIContent("Pulse Icon", "Set to 'true' if you want the icon's movment to pulse."), PulseIcon.boolValue);
			// Increase the indent.
			EditorGUI.indentLevel++;
			// Set the pulse intensity.
			EditorGUILayout.PropertyField(PulseIntensity, new GUIContent("Pulse Intensity", "How intense the pulse should be.  \nIF set above 1 the pulse will go big-small-big, \nIF set under 1 the pulse will go small-big-small."));
			// Set the pulse time for the icon.
			EditorGUILayout.PropertyField(PulseTime, new GUIContent("Pulse Time", "The time it take for the icon to pulse from small-big or big-small."));
			// Decrease the Indent.
			EditorGUI.indentLevel--;
			// End the Toggle Group.
			EditorGUILayout.EndToggleGroup();


			EditorGUILayout.Space();


			// Set the toggle group for the spinning icon.
			SpinIcon.boolValue = EditorGUILayout.BeginToggleGroup(new GUIContent("Spin Icon", "Set to 'true' if you want the icon's movement to spin."), SpinIcon.boolValue);
			// Increase the indent.
			EditorGUI.indentLevel++;
			// Set the X rotation speed.
			EditorGUILayout.PropertyField(SpinXSpeed, new GUIContent("X Spin Speed", "The speed the gameobject rotates on its X."));
			// Set the y rotation speed.
			EditorGUILayout.PropertyField(SpinYSpeed, new GUIContent("Y Spin Speed", "The speed the gameobject rotates on its Y"));
			// Decrease the Indent.
			EditorGUI.indentLevel--;
			// End the Toggle Group.
			EditorGUILayout.EndToggleGroup();

			// Decrease the Indent.
			EditorGUI.indentLevel--;

			// Apply.
			serializedObject.ApplyModifiedProperties();
		}
	}
}
