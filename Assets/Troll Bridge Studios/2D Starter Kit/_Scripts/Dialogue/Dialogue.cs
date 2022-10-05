using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace TrollBridge {

	[RequireComponent(typeof(RectTransform))]
	public class Dialogue : MonoBehaviour {

		public Image dialogueImage;
		public Text dialogueText;

		private Text _dialogueText;
		private Color _dialogueColor;
		private Color _textColor;
		private float _initialDialogueUIAlpha;
		private float _initialDialogueTextAlpha;
		private Vector3 _initialDialogueScale;

		void Awake(){
			// Set the txt initially to "".
			dialogueText.text = "";
			// Grab the initial dialogue UI alpha.
			_initialDialogueUIAlpha = dialogueImage.color.a;
			// Grab the initial dialogue text alpha.
			_initialDialogueTextAlpha = dialogueText.color.a;
			// Grab the initial dialogue scale.
			_initialDialogueScale = dialogueImage.transform.localScale;
		}

		void Start(){
			// Check to make sure the user has the scripts working correctly.
			DebugCheck();
	    }

		void DebugCheck(){
			// IF user has did not set the Dialogue Image. 
			if(dialogueImage == null){
				Grid.helper.DebugErrorCheck(73, this.GetType(), gameObject);
			}
			// IF the user did not set the Dialogue Text.
			if(dialogueText == null){
				Grid.helper.DebugErrorCheck(74, this.GetType(), gameObject);
			}
		}

		public void SetDialogueUIColors(Color diaColor, Color txtColor){
			// Assign new colors to private variables.
			_dialogueColor = diaColor;
			_textColor = txtColor;

			// Set the dialogue and text colors.
			dialogueImage.color = _dialogueColor;
			dialogueText.color = _textColor;
		}

		public void SetDialogueUIAlpha(float diaAlpha){
			// When we create the dialogue box/text colors we always start with alpha at 0.
			_dialogueColor.a = diaAlpha;
			// Set the dialogue and text colors.
			dialogueImage.color = _dialogueColor;

		}

		public void SetDialogueTextAlpha(float txtAlpha){
			_textColor.a = txtAlpha;
			dialogueText.color = _textColor;
		}

		public void SetInitialDialogueUIAlpha(){
			_dialogueColor.a = _initialDialogueUIAlpha;
			dialogueImage.color = _dialogueColor;
		}
		
		public void SetInitialDialogueTextAlpha(){
			_textColor.a = _initialDialogueTextAlpha;
			dialogueText.color = _textColor;
		}

		public void SetInitialDialogueScale(){
			gameObject.transform.localScale = _initialDialogueScale;
		}

		public void SetDialogueScale(float diaXScale, float diaYScale){
			gameObject.transform.localScale = new Vector3(diaXScale, diaYScale, 1f);
		}

		public void SwitchText(string newText){
			// Change the dialogueText to newText.
			dialogueText.text = newText;
		}

		public float GetInitialDialogueUIAlpha(){
			return _initialDialogueUIAlpha;
		}

		public float GetInitialDialogueTextAlpha(){
			return _initialDialogueTextAlpha;
		}

		public Vector3 GetInitialDialogueScale(){
			return _initialDialogueScale;
		}
	}
}
