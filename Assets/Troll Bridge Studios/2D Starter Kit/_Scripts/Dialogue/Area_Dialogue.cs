using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace TrollBridge {

	public class Area_Dialogue : MonoBehaviour {

		/// Set to 'true' if you want to see the area that the Player can interact with the NPC
		public bool showAreaInScene = false;
		/// The Collider2D that represents the range for the Interaction to happen.
		public Collider2D rangeCollider;
		/// Sets the color of the Collider2D
		public Color areaColor = Color.black;

		/// The Dialogue UI GameObject that will be displayed.
		public GameObject dialogueBox;
		/// The color alteration for the 'Dialogue UI'.  Leaving this color 'white' will keep the same look for your 'Dialogue UI' GameObject.
		public Color dialogueColor = Color.white;
		/// This time dictates how long before the dialogue can be displayed again after it has been completed/destroyed.
		public float inactiveTime = 3;

		/// Set to 'true' if you want dialogue UI transitions to happen after each dialogue string in the 'Dialogue' array.
		public bool multipleTransitions = false;
		/// The time each dialogue is visible before transitioning to the next dialogue.
		public float chatDuration = 4.20f;
		/// Set to 'true' if you want the dialogue UI transition to appear/disappear instantly.
		public bool isInstantDialogue = true;
		/// Set to 'true' if you want the dialogue UI transition to fade.
		public bool isFadeDialogue;
		/// The fade time for when a dialogue box fades in and fades out.
		public float fadeTime = 0.5f;
		/// Set to 'true' if you want the dialogue UI transition to grow and shrink.
		public bool isGrowShrinkDialogue;
		/// The grow/shrink time for when a dialogue box grows in and shrinks out.
		public float growShrinkTime = 0.6f;

		/// The color of the dialogue text.
		public Color dialogueTextColor = Color.black;
		/// Set to 'true' if you want the text transition to to appear/disappear instantly.
		public bool instantText;
		/// Set to 'true' if you want the text transition to be faded in and out.
		public bool fadeText;
		/// The time at which the text is faded in and out.
		public float textFadeTime = 0.8f;
		/// Set to 'true' if you want the text transition to be typed out.
		public bool typedText;
		/// The time it takes for the next letter to be displayed. 
		/// Increasing this number slows the typing speed of the dialogue text while decreasing this number speeds up the typing speed of the dialogue.
		public float dialogueTextPause = 0.1f;
		/// The sound that plays when each character is typed in the dialogue.
		public AudioClip typeSound;
		[Tooltip("The text that is displayed in the Dialogue UI.")]
		[Multiline]
		public string[] dialogue;

		// The index in the dialogue.
		private int dialogueIndex = 0;
		// The dialogue component.
		private Dialogue dialogueComponent;
		// Get the manager of this gameobject.
		private Character _character;
		// The Player State.
		private Player_Manager _playerManager;
		// The boolean that lets us know when the timer is finished.
		private bool isFinished = true;


		void Awake(){
			// Get the Dialogue Component.
			dialogueComponent = dialogueBox.GetComponent<Dialogue>();
			// Check to make sure the user has the scripts working correctly.
			DebugCheck();
			// Set the color for the dialogue box.
			dialogueComponent.SetDialogueUIColors(dialogueColor, dialogueTextColor);
		}

		void DebugCheck(){
			// IF user has the show area in scene 
			if(showAreaInScene && (rangeCollider == null)){
				Grid.helper.DebugErrorCheck(70, this.GetType(), gameObject);
			}
			// IF the user didnt set a dialogue box gameobject to be shown.
			if(dialogueBox == null){
				Grid.helper.DebugErrorCheck(71, this.GetType(), gameObject);
			}
			// IF the user didn't set any dialogue for the NPC.
			if(dialogue.Length == 0){
				Grid.helper.DebugErrorCheck(72, this.GetType(), gameObject);
			}
			// IF the user didnt set a Dialogue componenet on the dialogueBox.
			if(dialogueComponent == null){
				Grid.helper.DebugErrorCheck (79, this.GetType(), gameObject);
			}
		}

		void OnDrawGizmos(){
	#if UNITY_EDITOR
			// This is used for Scene view.
			if(showAreaInScene && rangeCollider != null){
				// IF we have a CircleCollider2D,
				// ELSE IF we have a BoxCollider2D.
				if(rangeCollider.GetType() == typeof(CircleCollider2D)){
					// Display the Circle Collider on the scene view.
					SceneCircleCollider(rangeCollider.GetComponent<CircleCollider2D>(), areaColor);
				}else if(rangeCollider.GetType() == typeof(BoxCollider2D)){
					// Display the Box Collider on the scene view.
					SceneBoxCollider(rangeCollider.GetComponent<BoxCollider2D>(), areaColor);
				}
			}
	#endif
		}

		void OnTriggerEnter2D(Collider2D coll){
			// Attempt to grab the Player_Manager script
			Player_Manager _player = coll.GetComponentInParent<Player_Manager>();
			// IF the colliding object doesnt have the Player Manager script.
			if(_player == null){
				return;
			}
			// IF the colliding object's tag isn't Player.
			if(coll.tag != "Player"){
				return;
			}
			// Assign the Player Manager script.
			_playerManager = _player;
			// Since we have a Player Manager script we assign the state.
			_playerManager.ListOfAreaDialogues.Add(this);
			// We begin the dialogue.
			CreateDialogue();
		}

		void OnTriggerStay2D(Collider2D coll){
			// Attempt to grab the Player_Manager script
			Player_Manager _player = coll.GetComponentInParent<Player_Manager>();
			// IF the colliding object doesnt have the Player Manager script.
			if(_player == null){
				return;
			}
			// IF the colliding object's tag isn't Player.
			if(coll.tag != "Player"){
				return;
			}
			// While we are in this trigger lets keep trying to create a dialogue until it lets us.
			CreateDialogue ();
		}

		void OnTriggerExit2D(Collider2D coll){
			// Attempt to grab the Player_Manager script
			Player_Manager _player = coll.GetComponentInParent<Player_Manager> ();
			// IF the colliding object doesnt have the Player Manager script.
			if (_player == null) {
				return;
			}
			// IF the colliding object's tag isn't Player.
			if (coll.tag != "Player") {
				return;
			}
			// Since the dialogue is being destroyed we assign the state.
			_playerManager.ListOfAreaDialogues.Remove (this);
			// IF the dialogueBox is active in the hierarchy.
			if (dialogueBox.activeInHierarchy) {
				// Stop all coroutines happening on this script
				StopAllCoroutines ();
				// Finish up the last transition, destroy and start the timer.
				StartCoroutine (EndDialogue ());
			}
		}


		// Create the Dialogue box.
		private void CreateDialogue(){
			// IF we are engaged in a action key dialogue OR we currently have this dialogue running.
			if (_playerManager.IsActionKeyDialogued || !isFinished) {
				return;
			}
			// IF the dialogue box is not active.
			if (!dialogueBox.activeInHierarchy) {
				// Display the dialogue box.
				dialogueBox.SetActive (true);
				// Boolean to let us know we will have to wait till this is finished.
				isFinished = false;
				// Since we created a dialogue GameObject we go to the next part of the dialogue chat.
				StartCoroutine (GoToNextDialogue ());
			}
		}


		// The final frontier of dialogue then BAM its set inactive and a timer starts before it can re-appear again.
		private IEnumerator EndDialogue(){
			// Reset the dialogueIndex.
			dialogueIndex = 0;
			// The last transition.
			yield return StartCoroutine(DialogueOut());
			// Set the dialogue box inactive.
			dialogueBox.SetActive(false);
			// Suspend the coroutine for 'inactiveTime' seconds.
			yield return new WaitForSeconds(inactiveTime);
			// Boolean to let us know we are done waiting.
			isFinished = true;
		}


		// Go to the next part of the dialogue.
		private IEnumerator GoToNextDialogue(){
			// WHILE we are not at the end.
			while (dialogueIndex < dialogue.Length) {
				// IF we are on the first message so display it.
				// ELSE we are in the middle of a dialogue so we do a full transition.
				if (dialogueIndex == 0) {
					// The first transition.
					yield return StartCoroutine (DialogueIn ());
				} else {
					// IF we want multiple dialogue transitions,
					// ELSE we just want the text to be changed.
					if (multipleTransitions) {
						// Take away the dialogue background.
						yield return StartCoroutine (DialogueOut ());
						// Bring in the dialogue background.
						yield return StartCoroutine (DialogueIn ());
					} else {
						// Take away the dialogue text.
						yield return StartCoroutine (DialogueTextOut ());
						// Bring in the dialogue text.
						yield return StartCoroutine (DialogueTextIn ());
					}
				}
				// Increase the dialogueIndex.
				dialogueIndex++;
				// Suspend the coroutine for 'chatDuration' seconds.
				yield return new WaitForSeconds (chatDuration);
			}
			// Finish up the last transition, destroy and start the timer.
			yield return StartCoroutine (EndDialogue());
			yield break;
		}

		private IEnumerator DialogueIn(){
			// Based on the transition, Set the start variables.
			InitDialogue();
			// IF we want to fade in the dialogue,
			// ELSE IF we want to grow the dialogue,
			// ELSE IF we want to instantly show the dialogue.
			if(isFadeDialogue){
				// Fade in image.
				StartCoroutine(GUI_Helper.FadeImage(dialogueComponent.dialogueImage, fadeTime, 0f, dialogueComponent.GetInitialDialogueUIAlpha()));
				// IF we have fading text as well.
				if(fadeText){
					// Switch to the new text.
					dialogueComponent.SwitchText(dialogue[dialogueIndex]);
					// Fade in text.
					StartCoroutine(GUI_Helper.FadeText(dialogueComponent.dialogueText, textFadeTime, 0f, dialogueComponent.GetInitialDialogueTextAlpha()));
					// Wait for the longer time of the dialogue fade or the dialogue fade text time.
					yield return new WaitForSeconds(Mathf.Max(fadeTime, textFadeTime));
					yield break;
				}
				// Wait for the length of the Dialogue Box fade.
				yield return new WaitForSeconds(fadeTime);
			}else if(isGrowShrinkDialogue){
				// Grow the dialogue.
				yield return StartCoroutine(GUI_Helper.GrowShrinkImage(dialogueComponent.dialogueImage, growShrinkTime, dialogueComponent.dialogueImage.transform.localScale.x, dialogueComponent.dialogueImage.transform.localScale.y, dialogueComponent.GetInitialDialogueScale().x, dialogueComponent.GetInitialDialogueScale().y));
			}else if(isInstantDialogue){
				// No Coroutines needed but leaving this here incase you want to implement something.
			}

			// Switch to the new text.
			dialogueComponent.SwitchText(dialogue[dialogueIndex]);
			// IF we want to fade the text,
			// ELSE IF we want the text to be typed out,
			// ELSE IF we want the text to be displayed instantly.
			if(fadeText){
				// Start the fade on the text.
				yield return StartCoroutine(GUI_Helper.FadeText(dialogueComponent.dialogueText, textFadeTime, 0f, dialogueComponent.GetInitialDialogueTextAlpha()));
			}else if(typedText){
				// Dont move forward until the typing of the text is finished.
				yield return StartCoroutine(GUI_Helper.TypeText(dialogueComponent.dialogueText, dialogueTextPause, dialogue[dialogueIndex], typeSound));
			}else if(instantText){
				// No Coroutine needed but leaving this here incase you want to implement something.
			}
		}	

		private IEnumerator DialogueOut(){
			// IF we want the dialogue to fade,
			// ELSE IF we want to shrink the dialogue,
			// ELSE IF we want the dialogue to disappear instantly.
			if(isFadeDialogue){
				// Fade out the dialogue.
				StartCoroutine(GUI_Helper.FadeImage(dialogueComponent.dialogueImage, fadeTime, dialogueComponent.dialogueImage.color.a, 0f));
				// We fade the text.
				yield return StartCoroutine(GUI_Helper.FadeText(dialogueComponent.dialogueText, fadeTime, dialogueComponent.dialogueText.color.a, 0f));
			}else if(isGrowShrinkDialogue){
				// Shrink the dialogue.
				yield return StartCoroutine(GUI_Helper.GrowShrinkImage(dialogueComponent.dialogueImage, growShrinkTime, dialogueComponent.dialogueImage.transform.localScale.x, dialogueComponent.dialogueImage.transform.localScale.y, 0f, 0f));
			}else if(isInstantDialogue){
				// No Coroutine needed but leaving this here incase you want to implement something.
			}
			// Switch to a blank text.
			dialogueComponent.SwitchText("");
		}

		private IEnumerator DialogueTextIn(){
			// Switch the dialogue text.
			dialogueComponent.SwitchText(dialogue[dialogueIndex]);
			// IF we want to fade the text,
			// ELSE IF we want the text to be typed out,
			// ELSE IF we want the text to appear instantly.
			if(fadeText){
				yield return StartCoroutine(GUI_Helper.FadeText(dialogueComponent.dialogueText, textFadeTime, 0f, dialogueComponent.GetInitialDialogueTextAlpha()));
			}else if(typedText){
				// Dont move forward until the typing of the text is finished.
				yield return StartCoroutine(GUI_Helper.TypeText(dialogueComponent.dialogueText, dialogueTextPause, dialogue[dialogueIndex], typeSound));
			}else if(instantText){
				// No Coroutine needed but leaving this here incase you want to implement something.
			}
		}

		private IEnumerator DialogueTextOut(){
			// IF we want the dialogue to fade,
			// ELSE IF we have the typed text OR instant text to transition out.
			if (fadeText) {
				// We fade the text.
				yield return StartCoroutine (GUI_Helper.FadeText (dialogueComponent.dialogueText, textFadeTime, dialogueComponent.dialogueText.color.a, 0f));
			} else if (typedText) {
				// No Coroutine needed but leaving this here incase you want to implement something.
			} else if (instantText) {
				// No Coroutine needed but leaving this here incase you want to implement something.
			}
			// Switch to a blank.
			dialogueComponent.SwitchText("");
		}

		/// <summary>
		/// Handle the default settings for when the dialogue box is being shown.
		/// </summary>
		private void InitDialogue(){
			// IF we have a fading dialogue,
			// ELSE IF we have a grow/shrinking dialogue,
			// ELSE IF we have a instant dialogue.
			if(isFadeDialogue){
				// Set the alpha to 0 since we fade in.
				dialogueComponent.SetDialogueUIAlpha(0f);
				// Set the initial scaling of this Dialogue Box.
				dialogueComponent.SetInitialDialogueScale ();
			}else if(isGrowShrinkDialogue){
				// Set the alpha to what it was initially.
				dialogueComponent.SetInitialDialogueUIAlpha ();
				// Set the scale to 0 as we will be growing the dialogue box.
				dialogueComponent.SetDialogueScale(0f, 0f);
			}else if(isInstantDialogue){
				// Set the initial alpha.
				dialogueComponent.SetInitialDialogueUIAlpha ();
				// Set the initial scaling of this Dialogue Box.
				dialogueComponent.SetInitialDialogueScale ();
			}

			// IF we want the text to fade,
			// ELSE IF we want the text to by typed out,
			// ELSE IF we want hte text to by instantly displayed.
			if(fadeText){
				dialogueComponent.SetDialogueTextAlpha(0f);
			} else if(typedText){
				// Set the initial alpha of the text to what the developer had before runtime.
				dialogueComponent.SetInitialDialogueTextAlpha();
			} else if(instantText){
				// Set the initial alpha of the text to what the developer had before runtime.
				dialogueComponent.SetInitialDialogueTextAlpha();
			}
		}

		public void ResetDialogue(){
			// Stop the coroutines for resetting the dialogue.
			StopAllCoroutines();
			// Immediatley set this dialogue inactive with DialogueOut results.
			ShutDownDialogue();

		}

		private void ShutDownDialogue(){
			// Reset the dialogueIndex.
			dialogueIndex = 0;
			// Set the dialogue box inactive.
			dialogueBox.SetActive(false);
			// Boolean to let us know we are done waiting.
			isFinished = true;
			// IF we want the dialogue to fade,
			// ELSE IF we want to shrink the dialogue,
			// ELSE IF we want the dialogue to disappear instantly.
			if(isFadeDialogue){
				// Fade out the dialogue.
				dialogueComponent.SetDialogueUIAlpha (0f);
				// We fade the text.
				dialogueComponent.SetDialogueTextAlpha (0f);
			}else if(isGrowShrinkDialogue){
				// Shrink the dialogue.
				dialogueComponent.SetDialogueScale(0f, 0f);
			}else if(isInstantDialogue){
				// Nothing.
			}
			// Switch to a blank text.
			dialogueComponent.SwitchText("");
		}

		// Used for displaying collider information on the Scene View.
		private void SceneCircleCollider(CircleCollider2D coll, Color areaColor){
			#if UNITY_EDITOR
			// Set the color.
			UnityEditor.Handles.color = areaColor;
			// Get the offset.
			Vector3 offset = coll.offset;
			// Get the position of the collider gameobject.
			Vector3 discCenter = coll.transform.position;
			// Scaling incase the gameobject has been scaled.
			float scale;
			// IF the x scale is larger than the y scale.
			if(transform.lossyScale.x > transform.lossyScale.y){
				// Make scale the size of the x.
				scale = transform.lossyScale.x;
			}else{
				// Make scale the size of the y.
				scale = transform.lossyScale.y;
			}

			// Draw the Disc on the Scene View.
			UnityEditor.Handles.DrawWireDisc(discCenter + offset, Vector3.back, coll.radius * scale);
			#endif
		}
		
		// Used for displaying collider information on the Scene View.
		private void SceneBoxCollider(BoxCollider2D coll, Color areaColor){
			// Set the color.
			Gizmos.color = areaColor;
			// Get the offset.
			Vector3 offset = coll.offset;
			// Get the position of the collider gameobject.
			Vector3 boxCenter = coll.transform.position;
			// Draw the Box on the Scene View.
			Gizmos.DrawWireCube(boxCenter + offset, new Vector2(coll.size.x * transform.lossyScale.x, coll.size.y * transform.lossyScale.y));
		}
	}
}
