using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEditor;

namespace TrollBridge {
	
	[CanEditMultipleObjects]
	[CustomEditor(typeof(Sound_Manager))]
	public class Sound_Manager_Editor : Editor {

		SerializedProperty bgMusicSource;
		SerializedProperty musicOn;
		SerializedProperty sfxOn;
		SerializedProperty sfxVolume;

		void OnEnable(){
			bgMusicSource = serializedObject.FindProperty ("bgMusicSource");
			musicOn = serializedObject.FindProperty ("musicOn");
			sfxOn = serializedObject.FindProperty ("sfxOn");
			sfxVolume = serializedObject.FindProperty ("sfxVolume");
		}

		public override void OnInspectorGUI(){
			// Set the indentLevel to 0 as default (no indent).
			EditorGUI.indentLevel = 0;
			// Update
			serializedObject.Update();

			// General Label.
			EditorGUILayout.LabelField("General", EditorStyles.boldLabel);
			// Increase the indent.
			EditorGUI.indentLevel++;
			// The Background.
			EditorGUILayout.PropertyField(bgMusicSource, new GUIContent("BG Music Source","The background music AudioSource.  Drag the AudioSource in the inspector that is attached to this GameObject to this variable."));
			// Set the layout.
			EditorGUILayout.PropertyField(musicOn, new GUIContent("Music On"));
			EditorGUILayout.PropertyField(sfxOn, new GUIContent("SFX On"));
			// The sound volume.
			EditorGUILayout.PropertyField(sfxVolume, new GUIContent("SFX Volume", "The volume of the sound effects."));
			// Decrease the indent.
			EditorGUI.indentLevel--;

			// apply
			serializedObject.ApplyModifiedProperties();
		}
	}
}
