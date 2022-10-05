using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

namespace TrollBridge {

	public class Options_Manager : MonoBehaviour {

		// The Key that activates the Options Menu.
		public KeyCode optionsKey;
		// The Panel the options are on.
		public GameObject optionsPanel;
		// The scenes that you do not want the optionsKey to work on. These are the scenes such as Main Menu, cut scenes, etc.
		public string[] noOptionKeyScenes;

		// The music slider that is used for dictating the volume of the music.
		public Slider musicSlider;
		// the music toggle that is used for muting and unmuting the music.
		public Toggle musicToggle;
		// The sfx slider that is used for dictating the volume of the sfx.
		public Slider sfxSlider;
		// The sfx toggle that is used for muting and unmuting the sfx.
		public Toggle sfxToggle;

		// The Panel that the scaling will be applied to.
		public GameObject scalePanelOne;
		// The slider that adjusts the scaling.
		public Slider UISliderOne;
		// The text to display to notify the user what the scaling is.
		public Text UISliderOneText;
		// For faster calculation on UI scaling.
		private bool uiSliderOneTextExist;

		// The Panel that the scaling will be applied to.
		public GameObject scalePanelTwo;
		// The slider that adjusts the scaling.
		public Slider UISliderTwo;
		// The text to display to notify the user what the scaling is.
		public Text UISliderTwoText;
		// For faster calculation on UI scaling.
		private bool uiSliderTwoTextExist;

		// The Panel that the scaling will be applied to.
		public GameObject scalePanelThree;
		// The slider that adjusts the scaling.
		public Slider UISliderThree;
		// The text to display to notify the user what the scaling is.
		public Text UISliderThreeText;
		// For faster calculation on UI scaling.
		private bool uiSliderThreeTextExist;

		// The fullscreen toggle.
		public Toggle fullscreen;

		// Variables used for the scaling.
		private float uiScaleOne;
		private float uiScaleTwo;
		private float uiScaleThree;

		
		void Awake(){
			// Check to make sure the user has the scripts working correctly.
			DebugCheck();

			// Load any save game options.
			LoadOptionsSettings();

			// Set the fullscreen option to what was either made by default 
			// or saved from last session.
			if(fullscreen != null && Application.platform != RuntimePlatform.WebGLPlayer){
				fullscreen.isOn = Screen.fullScreen;
			}
		}

		void Start () {
			// Set the toggles and sliders in Sound_Manager.
			Grid.soundManager.SetSlidersAndToggles(musicSlider, musicToggle, sfxSlider, sfxToggle);
		}

		void Update() {
			// IF the user hits The Options Key.
			if(Input.GetKeyUp(optionsKey)){
				// Bool for the noOptionKeyScenes.
				bool hit = false;
				// Loop through the noOptionKeyScenes.
				for(int i = 0; i < noOptionKeyScenes.Length; i++){
					// IF our current scene matches the scenes we do not want to display options.
					if(SceneManager.GetActiveScene().name == noOptionKeyScenes[i]){
						// We have a match.
						hit = true;
					}
				}
				// IF we didn't get a match on not bring up the options.
				if(!hit){
					// IF the panel is not showing then display it and pause the game.
					// ELSE the panel is showing then remove it and unpause the game.
					if(!optionsPanel.activeInHierarchy){
						OptionsDisplay(true);
						Grid.helper.SetTimeScale(0f);
					}else{
						OptionsDisplay(false);
						Grid.helper.SetTimeScale(1f);
					}
				}
			}
		}

		void DebugCheck(){
			// IF we have set our keycode to something other than None.
			if(optionsKey != KeyCode.None){
				// IF we don't have a Panel to display the options.
				if(optionsPanel == null){
					Grid.helper.DebugErrorCheck(101, this.GetType(), gameObject);
				}
			}
		}

		void ScalePanelOneCheck(){
			// IF the scalePanel is equal to null,
			// ELSE we have a scalePanel.
			if(scalePanelOne == null){
				return;
			}else{
				// Do we have UISliderText.
				uiSliderOneTextExist = UISliderOneText != null;
				// Make the UISlider interactable.
				UISliderOne.interactable = true;
				// Since we have a UI panel on the scene lets grab the UI scaling from our saved settings.
				scalePanelOne.transform.localScale = new Vector3(uiScaleOne, uiScaleOne, 1f);
			}
			// Set the value of the Slider.
			UISliderOne.value = uiScaleOne;
		}

		void ScalePanelTwoCheck(){		
			// IF the scalePanel is equal to null,
			// ELSE we have a scalePanel.
			if(scalePanelTwo == null){
				return;
			}else{
				// Do we have UISliderText
				uiSliderTwoTextExist = UISliderTwoText != null;
				// Make the UISlider interactable.
				UISliderTwo.interactable = true;
				// Since we have a UI panel on the scene lets grab the UI scaling from our saved settings.
				scalePanelTwo.transform.localScale = new Vector3(uiScaleTwo, uiScaleTwo, 1f);
			}
			// Set the value of the Slider.
			UISliderTwo.value = uiScaleTwo;
		}

		void ScalePanelThreeCheck(){		
			// IF the scalePanel is equal to null,
			// ELSE we have a scalePanel.
			if(scalePanelThree == null){
				return;
			}else{
				// Do we have UISliderText
				uiSliderThreeTextExist = UISliderThreeText != null;
				// Make the UISlider interactable.
				UISliderThree.interactable = true;
				// Since we have a UI panel on the scene lets grab the UI scaling from our saved settings.
				scalePanelTwo.transform.localScale = new Vector3(uiScaleThree, uiScaleThree, 1f);
			}
			// Set the value of the Slider.
			UISliderThree.value = uiScaleThree;
		}

		public void OptionsDisplay(){
			optionsPanel.SetActive (!optionsPanel.activeInHierarchy);
		}

		public void OptionsDisplay(bool active){
			optionsPanel.SetActive (active);
		}

		public void SliderRescaleUI(int sliderNumber) {

			float scale;

			switch(sliderNumber){
			case 0:
				scale = ReScaleUI (scalePanelOne, UISliderOne, UISliderOneText, uiSliderOneTextExist);
				if(scale != -1){
					uiScaleOne = scale;
				}
				break;
			case 1:
				scale = ReScaleUI (scalePanelTwo, UISliderTwo, UISliderTwoText, uiSliderTwoTextExist);
				if(scale != -1){
					uiScaleTwo = scale;
				}
				break;
			case 2:
				scale = ReScaleUI (scalePanelThree, UISliderThree, UISliderThreeText, uiSliderThreeTextExist);
				if(scale != -1){
					uiScaleThree = scale;
				}
				break;

			default:
				Debug.Log("A wrong slider number was used for scaling the UI.  Numbers to choose are either 0, 1 or 2.  The number you chose was " + sliderNumber);
				break;
			}
		}

		float ReScaleUI(GameObject scalePanel, Slider UISlider, Text UISliderText, bool uiSliderTextExist){
			// IF there is a scale panel in the scene.
			// ELSE there isnt but we still need to set the options variables.
			if(scalePanel != null){
				// What ever the value is we need to make sure we apply this to all the children in the Panel Parent.
				scalePanel.transform.localScale = new Vector3(UISlider.value, UISlider.value, 1f);
				// IF we have slider text.
				if(uiSliderTextExist){
					// Set the text to display the UI scaling number.
					UISliderText.text = UISlider.value.ToString("F2");
				}
				
				return UISlider.value;
			}else{
				// IF we have slider text.
				if(uiSliderTextExist){
					// Set the text to display the UI scaling number.
					UISliderText.text = UISlider.value.ToString("F2");
				}
				return -1;
			}
		}

		public void MusicToggle(){
			Grid.soundManager.MuteUnMuteBGMusic(musicToggle.isOn);
		}
		
		public void MusicSlider(){
			Grid.soundManager.ChangeMusicVolume(musicSlider.value);
		}
		
		public void SFXToggle(){
			Grid.soundManager.MuteUnMuteSound(sfxToggle.isOn);
		}
		
		public void SFXSlider(){
			Grid.soundManager.ChangeSFXVolume(sfxSlider.value);
		}

		public void ChangeScreenMode(){
			Grid.helper.ChangeScreenMode(fullscreen.isOn);
		}
		
		public void SaveOptionSettings(){
			// Create a new State_Data.
			Options_Settings GameOptions = new Options_Settings ();
			// Save the information.
			GameOptions.SFXToggle = sfxToggle.isOn;
			GameOptions.SFXVolume = sfxSlider.value;
			GameOptions.MusicVolume = musicSlider.value;
			GameOptions.MusicToggle = musicToggle.isOn;
			GameOptions.UIOneScaling = uiScaleOne;
			GameOptions.UITwoScaling = uiScaleTwo;
			GameOptions.UIThreeScaling = uiScaleThree;
			// Turn the data into Json data.
			string optionsToJson = JsonUtility.ToJson (GameOptions);
			// Store the data.
			PlayerPrefs.SetString ("Options", optionsToJson);
		}
		
		public void LoadOptionsSettings(){
			// Load the json data.
			string optionsJson = PlayerPrefs.GetString ("Options");
			// Load the data structure.
			Options_Settings GameOptions = new Options_Settings();
			// IF there is nothing in this string,
			// ELSE there is stuff in this string.
			if (String.IsNullOrEmpty (optionsJson)) {
				// Load the Defaults.
				GameOptions.Default ();
			} else {
				// Load the saved Json data to our class data.
				GameOptions = JsonUtility.FromJson<Options_Settings> (optionsJson);
			}
			// Set the options.
			SetOptionsSettings(GameOptions);
		}

		/// <summary>
		/// Since these are global settings across the board and apply to every scene we assign these here.
		/// You may notice I do not assign anything with the UI settings because it is handled in the ScalePanelCheck 
		/// functions as that checks for the existance of the UI and assigns the scaling before it is displayed to the user.
		/// </summary>
		void SetOptionsSettings(Options_Settings GameOptions){
			// Set the music volume and toggle.
			musicSlider.value = GameOptions.MusicVolume;
			musicToggle.isOn = GameOptions.MusicToggle;
			Grid.soundManager.ChangeMusicVolume(musicSlider.value);
			Grid.soundManager.MuteUnMuteBGMusic(musicToggle.isOn);

			// Set the SFX volume and toggle.
			sfxSlider.value = GameOptions.SFXVolume;
			sfxToggle.isOn = GameOptions.SFXToggle;
			Grid.soundManager.ChangeSFXVolume(sfxSlider.value);
			Grid.soundManager.MuteUnMuteSound(sfxToggle.isOn);

			// Set the scaling of the panels.
			uiScaleOne = GameOptions.UIOneScaling;
			uiScaleTwo = GameOptions.UITwoScaling;
			uiScaleThree = GameOptions.UIThreeScaling;
			ScalePanelOneCheck ();
			ScalePanelTwoCheck ();
			ScalePanelThreeCheck ();
		}
	}

	[System.Serializable]
	class Options_Settings {
		// The Music toggle
		public bool MusicToggle;
		// The Music volume.
		public float MusicVolume;
		// The SFX Toggle.
		public bool SFXToggle;
		// The SFX volume.
		public float SFXVolume;
		// The scaling of UI 1 out of 3.
		public float UIOneScaling;
		// The scaling of UI 2 out of 3.
		public float UITwoScaling;
		// The scaling of UI 3 out of 3.
		public float UIThreeScaling;

		public void Default () {
			// Set the defaults of our variables.
			MusicToggle = false;
			MusicVolume = 0.5f;

			SFXToggle = false;
			SFXVolume = 0.5f;

			UIOneScaling = 1f;

			UITwoScaling = 1f;

			UIThreeScaling = 1f;
		}
	}
}
