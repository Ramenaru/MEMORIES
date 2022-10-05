using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

namespace TrollBridge {

	[CanEditMultipleObjects]
	[CustomEditor(typeof(Options_Manager))]
	public class Options_Manager_Editor : Editor {

		SerializedProperty optionsKey;
		SerializedProperty optionsPanel;
		SerializedProperty noOptionKeyScenes;

		SerializedProperty musicSlider;
		SerializedProperty musicToggle;
		SerializedProperty sfxSlider;
		SerializedProperty sfxToggle;

		SerializedProperty scalePanelOne;
		SerializedProperty UISliderOne;
		SerializedProperty UISliderOneText;

		SerializedProperty scalePanelTwo;
		SerializedProperty UISliderTwo;
		SerializedProperty UISliderTwoText;

		SerializedProperty scalePanelThree;
		SerializedProperty UISliderThree;
		SerializedProperty UISliderThreeText;

		SerializedProperty fullscreen;

		void OnEnable(){
			optionsKey = serializedObject.FindProperty ("optionsKey");
			optionsPanel = serializedObject.FindProperty ("optionsPanel");
			noOptionKeyScenes = serializedObject.FindProperty ("noOptionKeyScenes");

			musicSlider = serializedObject.FindProperty ("musicSlider");
			musicToggle = serializedObject.FindProperty ("musicToggle");
			sfxSlider = serializedObject.FindProperty ("sfxSlider");
			sfxToggle = serializedObject.FindProperty ("sfxToggle");

			scalePanelOne = serializedObject.FindProperty ("scalePanelOne");
			UISliderOne = serializedObject.FindProperty ("UISliderOne");
			UISliderOneText = serializedObject.FindProperty ("UISliderOneText");

			scalePanelTwo = serializedObject.FindProperty ("scalePanelTwo");
			UISliderTwo = serializedObject.FindProperty ("UISliderTwo");
			UISliderTwoText = serializedObject.FindProperty ("UISliderTwoText");

			scalePanelThree = serializedObject.FindProperty ("scalePanelThree");
			UISliderThree = serializedObject.FindProperty ("UISliderThree");
			UISliderThreeText = serializedObject.FindProperty ("UISliderThreeText");

			fullscreen = serializedObject.FindProperty ("fullscreen");
		}

		public override void OnInspectorGUI(){
			// Set the indentLevel to 0 as default (no indent).
			EditorGUI.indentLevel = 0;
			// Update
			serializedObject.Update();

			EditorGUILayout.LabelField("Options Panel", EditorStyles.boldLabel);
			// Increase the indent.
			EditorGUI.indentLevel++;
			// Set the Keycode.
			EditorGUILayout.PropertyField(optionsKey, new GUIContent("Options Key", "The Key that is used to bring up the options."));
			// The options panel that the settings are displayed on.
			EditorGUILayout.PropertyField(optionsPanel, new GUIContent("Options Panel","The panel (GameObject) that the settings are displayed on."));
			// The scenes that you do not want the options hot key to work on.
			EditorGUILayout.PropertyField(noOptionKeyScenes, new GUIContent("No Options Scenes", "The scenes that you do not want the optionsKey to work on.  Examples would be cut scenes or main menus."), true);
			// Decrease the indent.
			EditorGUI.indentLevel--;


			EditorGUILayout.LabelField("Sound Options", EditorStyles.boldLabel);
			// Increase the indent.
			EditorGUI.indentLevel++;
			// The music slider that is used for dictating the volume of the music.
			EditorGUILayout.PropertyField(musicSlider, new GUIContent("Music Slider","The slider used to gauge the volume of the music"));
			// the music toggle that is used for muting and unmuting the music.
			EditorGUILayout.PropertyField(musicToggle, new GUIContent("Music Toggle","The toggle used to mute/unmute the volume of the music"));
			// The sfx slider that is used for dictating the volume of the sfx.
			EditorGUILayout.PropertyField(sfxSlider, new GUIContent("SFX Slider","The slider used to gauge the volume of the sound effects"));
			// The sfx toggle that is used for muting and unmuting the sfx.
			EditorGUILayout.PropertyField(sfxToggle, new GUIContent("SFX Toggle","The toggle used to mute/unmute the volume of the music"));

			// Decrease the indent.
			EditorGUI.indentLevel--;


			EditorGUILayout.LabelField(new GUIContent("UI Scaling", "Optional settings if you would like to have UI scaling in your game.\n\n" +
				"There are 3 Scaling Panel Tags you can set incase you want to have independent scaling on multiple panels (Minimap scaling + Action Bar scaling as an example).\n\n" +
				"Leaving these blank will not cause any problems but if you input a string into any of the 'Scaling Panel Tags' then you will need to assign a Slider and a Text (Optional) so the user can use the Slider to adjust the scale and the Text to display to the user."), EditorStyles.boldLabel);
			// Increase the indent.
			EditorGUI.indentLevel++;
			// The panel that will be scaled.
			EditorGUILayout.PropertyField(scalePanelOne, new GUIContent("Scale Panel One", "Drag the Panel GameObject you wish to be scaled."));
			// The slider which will adjust the scaling of the Scaling Panel.
			EditorGUILayout.PropertyField(UISliderOne, new GUIContent("Scaling Slider", "The slider which will adjust the scaling of the Scaling Panel."));
			// The text which displays the current scaling.
			EditorGUILayout.PropertyField(UISliderOneText, new GUIContent("Scaling Text (Optional)", "The text which displays the current scaling on the slider."));

			EditorGUILayout.Space();
			EditorGUILayout.Space();

			// The panel that will be scaled.
			EditorGUILayout.PropertyField(scalePanelTwo, new GUIContent("Scale Panel Two", "Drag the Panel GameObject you wish to be scaled."));
			// The slider which will adjust the scaling of the Scaling Panel.
			EditorGUILayout.PropertyField(UISliderTwo, new GUIContent("Scaling Slider", "The slider which will adjust the scaling of the Scaling Panel."));
			// The text which displays the current scaling.
			EditorGUILayout.PropertyField(UISliderTwoText, new GUIContent("Scaling Text (Optional)", "The text which displays the current scaling on the slider."));

			EditorGUILayout.Space();
			EditorGUILayout.Space();

			// The panel that will be scaled.
			EditorGUILayout.PropertyField(scalePanelThree, new GUIContent("Scale Panel Three", "Drag the Panel GameObject you wish to be scaled."));
			// The slider which will adjust the scaling of the Scaling Panel.
			EditorGUILayout.PropertyField(UISliderThree, new GUIContent("Scaling Slider", "The slider which will adjust the scaling of the Scaling Panel."));
			// The text which displays the current scaling.
			EditorGUILayout.PropertyField(UISliderThreeText, new GUIContent("Scaling Text (Optional)", "The text which displays the current scaling on the slider."));

			// Decrease the indent.
			EditorGUI.indentLevel--;


			EditorGUILayout.LabelField("Fullscreen Toggle", EditorStyles.boldLabel);
			// Increase the indent.
			EditorGUI.indentLevel++;
			// The toggle that makes the game fullscreen and windowed.
			EditorGUILayout.PropertyField(fullscreen, new GUIContent("Fullscreen Toggle","The toggle that makes the game go to either fullscreen or windowed."));

			// Decrease the indent.
			EditorGUI.indentLevel--;

			// Apply
			serializedObject.ApplyModifiedProperties();
		}
	}
}
