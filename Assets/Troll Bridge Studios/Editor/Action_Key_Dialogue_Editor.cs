using UnityEngine;
using System.Collections;
using UnityEditor;

namespace TrollBridge {

	[CanEditMultipleObjects]
	[CustomEditor(typeof(Action_Key_Dialogue))]
	public class Action_Key_Dialogue_Editor : Editor {

		SerializedProperty showAreaInScene;
		SerializedProperty rangeCollider;
		SerializedProperty areaColor;

		SerializedProperty dialogueBox;
		SerializedProperty dialogueColor;

		SerializedProperty multipleTransitions;
		SerializedProperty isInstantDialogue;
		SerializedProperty isFadeDialogue;
		SerializedProperty fadeTime;
		SerializedProperty isGrowShrinkDialogue;
		SerializedProperty growShrinkTime;

		SerializedProperty dialogueTextColor;
		SerializedProperty instantText;
		SerializedProperty fadeText;
		SerializedProperty textFadeTime;
		SerializedProperty typedText;
		SerializedProperty dialogueTextPause;
		SerializedProperty typeSound;
		SerializedProperty dialogue;


		void OnEnable(){
			showAreaInScene = serializedObject.FindProperty ("showAreaInScene");
			rangeCollider = serializedObject.FindProperty ("rangeCollider");
			areaColor = serializedObject.FindProperty ("areaColor");

			dialogueBox = serializedObject.FindProperty ("dialogueBox");
			dialogueColor = serializedObject.FindProperty ("dialogueColor");

			multipleTransitions = serializedObject.FindProperty ("multipleTransitions");
			isInstantDialogue = serializedObject.FindProperty ("isInstantDialogue");
			isFadeDialogue = serializedObject.FindProperty ("isFadeDialogue");
			fadeTime = serializedObject.FindProperty ("fadeTime");
			isGrowShrinkDialogue = serializedObject.FindProperty ("isGrowShrinkDialogue");
			growShrinkTime = serializedObject.FindProperty ("growShrinkTime");

			dialogueTextColor = serializedObject.FindProperty ("dialogueTextColor");
			instantText = serializedObject.FindProperty ("instantText");
			fadeText = serializedObject.FindProperty ("fadeText");
			textFadeTime = serializedObject.FindProperty ("textFadeTime");
			typedText = serializedObject.FindProperty ("typedText");
			dialogueTextPause = serializedObject.FindProperty ("dialogueTextPause");
			typeSound = serializedObject.FindProperty ("typeSound");
			dialogue = serializedObject.FindProperty ("dialogue");
		}

		public override void OnInspectorGUI(){
			// Set the indentLevel to 0 as default (no indent).
			EditorGUI.indentLevel = 0;
			// Update
			serializedObject.Update();

			// Scene View title.
			EditorGUILayout.LabelField("Scene View", EditorStyles.boldLabel);
			// Increase the indent.
			EditorGUI.indentLevel++;
			// Get the togglegroup used to display the Collider2D area in the Scene View.
			showAreaInScene.boolValue = EditorGUILayout.BeginToggleGroup(new GUIContent("Show Area In Scene", "Set to 'true' if you want to see the area that the Player can interact with in the scene view."), showAreaInScene.boolValue);
			// Increase the Indent.
			EditorGUI.indentLevel++;
			// Get and Set the Collider2D used for the Interaction range.
			EditorGUILayout.PropertyField(rangeCollider, new GUIContent("Area Collider", "The Collider2D that represents the range for the Interaction to happen."));
			// Get and Set the color used to display the Collider2D area in the Scene View.
			EditorGUILayout.PropertyField(areaColor, new GUIContent("Area Color", "Set the color of the Collider2D that is being shown in your scene view."));
			// Decrease the Indent.
			EditorGUI.indentLevel--;
			// End the Toggle Group.
			EditorGUILayout.EndToggleGroup();
			// Decrease the indent.
			EditorGUI.indentLevel--;
			
			
			EditorGUILayout.Space();


			// The Dialogue title.
			EditorGUILayout.LabelField("Dialogue", EditorStyles.boldLabel);
			// Increase the indent.
			EditorGUI.indentLevel++;
			// Get and Set the Dialogue Box GameObject.
			EditorGUILayout.PropertyField(dialogueBox, new GUIContent("Dialogue UI", "The Dialogue UI GameObject that will be displayed."));
			// Get and Set the dialogue background color.
			EditorGUILayout.PropertyField(dialogueColor, new GUIContent("Dialogue Color", "The color alteration for the 'Dialogue UI'.  Leaving this color 'white' will keep the same look for your 'Dialogue UI' GameObject."));
			// Decrease the indent.
			EditorGUI.indentLevel--;
			
			
			EditorGUILayout.Space();

			
			// The Dialogue Transition title.
			EditorGUILayout.LabelField("Dialogue Transition", EditorStyles.boldLabel);
			// Increase the indent.
			EditorGUI.indentLevel++;
			// Get and Set the multiple transition boolean.
			EditorGUILayout.PropertyField(multipleTransitions, new GUIContent("Multiple Transitions", "Set to 'true' if you want dialogue UI transitions to happen after each dialogue string in the 'Dialogue' array."));
			// Get and Set the instant dialogue transition boolean.
			isInstantDialogue.boolValue = EditorGUILayout.BeginToggleGroup(new GUIContent("Instant Transition", "Set to 'true' if you want the dialogue UI transition to appear/disappear instantly."), (!isFadeDialogue.boolValue && !isGrowShrinkDialogue.boolValue));
			// End the Toggle Group.
			EditorGUILayout.EndToggleGroup();
			// Get and Set the fade dialogue transition boolean.
			isFadeDialogue.boolValue = EditorGUILayout.BeginToggleGroup(new GUIContent("Fade Transition", "Set to 'true' if you want the dialogue UI transition to fade."), (!isInstantDialogue.boolValue && !isGrowShrinkDialogue.boolValue) && (isFadeDialogue.boolValue && !isInstantDialogue.boolValue && !isGrowShrinkDialogue.boolValue));
			// Increase the Indent.
			EditorGUI.indentLevel++;
			// Get and Set the Fade Time.
			EditorGUILayout.Slider(fadeTime, 0.1f, 2f, new GUIContent("Fade Time", "The fade time for when a dialogue box fades in and fades out."));
	//		Mathf.Round(fadeTime.floatValue * 100.0f) / 100f
			// Decrease the Indent.
			EditorGUI.indentLevel--;
			// End the Toggle Group.
			EditorGUILayout.EndToggleGroup();
			// Get and Set the Grow/Shrink dialogue transition boolean.
			isGrowShrinkDialogue.boolValue = EditorGUILayout.BeginToggleGroup(new GUIContent("Grow/Shrink Transition", "Set to 'true' if you want the dialogue UI transition to grow and shrink."), (!isInstantDialogue.boolValue && !isFadeDialogue.boolValue) && (isGrowShrinkDialogue.boolValue && !isFadeDialogue.boolValue && !isInstantDialogue.boolValue));
			// Increase the Indent.
			EditorGUI.indentLevel++;
			// Get and Set the Grow/Shrink Time.
			EditorGUILayout.Slider(growShrinkTime, 0.1f, 1.0f, new GUIContent("Grow/Shrink Time", "The grow/shrink time for when a dialogue box grows in and shrinks out."));
	//		Mathf.Round(growShrinkTime.floatValue * 100.0f) / 100f
			// Decrease the Indent.
			EditorGUI.indentLevel--;
			// End the Toggle Group.
			EditorGUILayout.EndToggleGroup();
			// Decrease the Indent.
			EditorGUI.indentLevel--;
			
			
			EditorGUILayout.Space();
			
			
			// The Dialogue Text title.
			EditorGUILayout.LabelField("Dialogue Text", EditorStyles.boldLabel);
			// Increase the indent.
			EditorGUI.indentLevel++;
			// Get and Set the dialogue text color.
			EditorGUILayout.PropertyField(dialogueTextColor, new GUIContent("Text Color", "The color of the dialogue text."));
			// Get and Set the instant text boolean.
			instantText.boolValue = EditorGUILayout.BeginToggleGroup(new GUIContent("Instant Text", "Set to 'true' if you want the text transition to appear/disappear instantly."), (!fadeText.boolValue && !typedText.boolValue));
			// End the Toggle Group.
			EditorGUILayout.EndToggleGroup();
			// Get and Set the fade text boolean.
			fadeText.boolValue = EditorGUILayout.BeginToggleGroup(new GUIContent("Faded Text", "Set to 'true' if you want the text transition to be faded in and out."), (!typedText.boolValue && !instantText.boolValue) && (fadeText.boolValue && !instantText.boolValue && !typedText.boolValue));
			// Increase the Indent.
			EditorGUI.indentLevel++;
			// Get and Set the text fade time.
			EditorGUILayout.Slider(textFadeTime, 0.1f, 5.0f, new GUIContent("Fade Time", "The time at which the text is faded in and out."));
	//		Mathf.Round(textFadeTime.floatValue * 10.0f) / 10f
			// Decrease the Indent.
			EditorGUI.indentLevel--;
			// End the Toggle Group.
			EditorGUILayout.EndToggleGroup();
			// Get and Set the typed text boolean.
			typedText.boolValue = EditorGUILayout.BeginToggleGroup(new GUIContent("Typed Text", "Set to 'true' if you want the text transition to be typed out."), (!fadeText.boolValue && !instantText.boolValue) && (!fadeText.boolValue && !instantText.boolValue && typedText.boolValue));
			// Increase the Indent.
			EditorGUI.indentLevel++;
			// Get and Set the typed text pause time.
			EditorGUILayout.Slider(dialogueTextPause, 0.01f, 0.5f, new GUIContent("Pause Time", "The time it takes for the next letter to be displayed.  Increasing this number slows the typing speed of the dialogue text while decreasing this number speeds up the typing speed of the dialogue."));
	//		Mathf.Round(dialogueTextPause.floatValue * 100.0f) / 100f
			// Get and Set the type sound.
			EditorGUILayout.PropertyField(typeSound, new GUIContent("Type Sound", "(Optional) The sound that plays when each character is typed in the dialogue."));
			// Decrease the indent.
			EditorGUI.indentLevel--;
			// End the Toggle Group.
			EditorGUILayout.EndToggleGroup();
			// Get and Set the dialogue text.
			EditorGUILayout.PropertyField(dialogue, true);
			// Decrease the indent.
			EditorGUI.indentLevel--;
			
			
			EditorGUILayout.Space();
			
			
			// Apply.
			serializedObject.ApplyModifiedProperties();
		}
	}
}
