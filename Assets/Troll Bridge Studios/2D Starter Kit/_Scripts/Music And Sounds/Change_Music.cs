using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

namespace TrollBridge {

	[RequireComponent(typeof(AudioSource))]
	[RequireComponent(typeof(Sound_Manager))]
	public class Change_Music : MonoBehaviour {

		// Do we want to fade the music when changing to a scene with a different background song.
		public bool fadeTransition;
		// The time it takes to fade before playing the next song.
		public float fadeTime = 0.25f;
		// The drawer for setting up the Scene Name and the BG Music.
		public Scene_Name_BGMusic[] NameMusic;
		// Float volume for fading.
		private float savedVolume;
		// Float volume for current volume.
		private float currentVolume;
		// Saved int for the level.
		private int currentLevel;
		// Bool to know if the FadeBGMusic is running.
		private bool isFadeRunning = false;


		void OnEnable(){
			// Add a delegate.
			SceneManager.sceneLoaded += OnLoadedLevel;
		}

		void OnDisable(){
			// Remove the delegate.
			SceneManager.sceneLoaded -= OnLoadedLevel;
		}

	    void Awake()
	    {
	        // IF we have a BG Music set for this Scene.
	        if (NameMusic[SceneManager.GetActiveScene().buildIndex].BGMusic != null)
	        {
	            // Play the BG Music.
				Grid.soundManager.PlayBGMusic(NameMusic[SceneManager.GetActiveScene().buildIndex].BGMusic);
	        }
	    }

		void OnLoadedLevel(Scene scene, LoadSceneMode mode){
			// IF a different sound is going to be played.
			if(Grid.soundManager.bgMusicSource.clip != NameMusic[scene.buildIndex].BGMusic){
				// Stop all Coroutines.
				StopAllCoroutines();
				// Save the current level in int form for other methods to use.
				currentLevel = scene.buildIndex;
				// Do we want to fade the music inbetween scene changes?
				if(fadeTransition){
					// IF the FadeBGMusic is not currently running.
					// Reason for this check is for the savedVolume, if the player goes to another area and right back to another area very fast it will mess with the volume for the game.  
					// So we only know we have a safe volume to save if that Coroutine is NOT running and when it is NOT running we know the volume is currently not being adjusted.
					if (!isFadeRunning) {
						// Save the current volume.
						savedVolume = Grid.soundManager.bgMusicSource.volume;
						currentVolume = savedVolume;
					} else {
						currentVolume = Grid.soundManager.bgMusicSource.volume;
					}
					// Start the fading of the music.
					StartCoroutine (FadeBGMusic());
					// GTFO Porkchop sandwiches. lol.
					return;
				}
				// Play the BG Music.
				Grid.soundManager.PlayBGMusic(NameMusic[scene.buildIndex].BGMusic);
			}
		}

		private IEnumerator FadeBGMusic(){
			// Notifier to let us know this is currently running.
			isFadeRunning = true;
			// Fade the current song out.
			for(float x = 0f; x <= 1.0f; x += Time.deltaTime / (fadeTime/2f)){
				// Smooth step the volume out to 0.
				Grid.soundManager.bgMusicSource.volume = Mathf.SmoothStep(currentVolume, 0f, x);
				yield return null;
			}
			// Make sure the sound change is consistent so we make sure we put a 1f for our time incase our for loop x didnt exactly land on 1f.
			Grid.soundManager.bgMusicSource.volume = Mathf.SmoothStep(currentVolume, 0f, 1f);
			// Play the BG Music.
			Grid.soundManager.PlayBGMusic(NameMusic[currentLevel].BGMusic);
			// Fade the next song in.
			for(float x = 0f; x <= 1.0f; x += Time.deltaTime / (fadeTime/2f)){
				// Smooth step the volume back to savedVolume.
				Grid.soundManager.bgMusicSource.volume = Mathf.SmoothStep(0f, savedVolume, x);
				yield return null;
			}
			// Make sure the sound change is consistent so we make sure we put a 1f for our time incase our for loop x didnt exactly land on 1f.
			Grid.soundManager.bgMusicSource.volume = Mathf.SmoothStep(0f, savedVolume, 1f);
			// Notifier to let us know this is not currently running.
			isFadeRunning = false;
		}

		#if UNITY_EDITOR
		private static Scene_Name_BGMusic[] GetNames() {
			// Create an empty list of Scene_Name_BGMusic.
			List<Scene_Name_BGMusic> temp = new List<Scene_Name_BGMusic> ();
			// Loop through all the scenes in your Build Settings.
			foreach (UnityEditor.EditorBuildSettingsScene S in UnityEditor.EditorBuildSettings.scenes) {
				// IF this scene enabled.
				if (S.enabled) {
					// Get its path.
					string name = S.path.Substring (S.path.LastIndexOf ('/') + 1);
					// Get the scene name.
					name = name.Substring (0, name.Length - 6);
					// Create a new Scene_NameBGMusic.
					Scene_Name_BGMusic snb = new Scene_Name_BGMusic ();
					// Set the name.
					snb.Name = name;
					// Add to the List.
					temp.Add (snb);
				}
			}
			return temp.ToArray ();
		}
		
		[UnityEditor.MenuItem("CONTEXT/Change_Music/Refresh Scene Names")]
		private static void RefreshNames(UnityEditor.MenuCommand command) {
			// Grab the script.
			Change_Music context = (Change_Music)command.context;
			// Store the current array of Scene_Name_BGMusic drawers.
			Scene_Name_BGMusic[] oldSNB = context.NameMusic;
			// Grab the new list of Scene_Name_BGMusic drawers.
			Scene_Name_BGMusic[] currentSNB = GetNames ();
			// Compare the Two and if we have a match we give it the old AudioClip.
			for (int i = 0; i < oldSNB.Length; i++) {
				for (int j = 0; j < currentSNB.Length; j++) {
					if (oldSNB [i].Name == currentSNB [j].Name) {
						currentSNB [j].BGMusic = oldSNB [i].BGMusic;
						break;
					}
				}
			}

			context.NameMusic = currentSNB;
		}
		
		private void Reset() {
			NameMusic = GetNames ();
		}
		#endif
	}
}
