using UnityEngine;
using System.Collections;

namespace TrollBridge {

	[RequireComponent(typeof(SpriteRenderer))]
	[RequireComponent(typeof(Animator))]
	public class Animation_Direction : MonoBehaviour {

		public bool FlipToFace;
		public bool FlipX, FlipY;

		private Animator _animator;
		private bool _fourDirAnim, _eightDirAnim = false;
		private Transform _transform;
		private Vector2 prevLocation;
		private SpriteRenderer _renderer;
		private Character _character;
		private bool isCharacter = false;

		void Awake(){
			_transform = GetComponent<Transform> ();
			prevLocation = transform.position;
		}

		void Start () {
			_renderer = GetComponent<SpriteRenderer> ();
			_animator = GetComponent<Animator> ();
			if(GetComponentInParent<Character>() != null){
				_character = GetComponentInParent<Character> ();
				// Set this bool as a reference we have a Character script
				isCharacter = true;
			}
			// IF this gameobject has an animation.
			if(_animator != null){
				// IF the Animator is a Four Direction Animation,
				// ELSE IF the Animator is a Eight Direction Animation,
				// ELSE the Animator is a one direction animaton meaning that the there is an animation that only faces in 1 direction in which we will control which way it faces based on if it is moving left vs right or up vs down (FlipX vs FlipY).
				if(_animator.GetLayerName(0) == "Four Base"){
					// Set true that we have a Four Direction Animation.
					_fourDirAnim = true;
					// Set false that we have a Eight Direction Animation.
					_eightDirAnim = false;
				}else if(_animator.GetLayerName(0) == "Eight Base"){
					// Set false since we dont have a Four Direction Animation.
					_fourDirAnim = false;
					// Set true since we dont have a Eight Direction Animation.
					_eightDirAnim = true;
				}
			}
		}
		
		void Update () {
			// IF the user has an animation set and ready to go.
			if (_animator != null) {
				// Adjust for the current location.
				Vector2 curLocation = (Vector2)_transform.position - prevLocation;
				// IF there is a Character component.
				// ELSE there isn't a Character component
				if (isCharacter) {
					// IF the character can move.
					if (_character.CanMove) {
						// Play the animation.
						PlayAnimation (curLocation.x, curLocation.y);
					}
				} else {
					// Play the animation.
					PlayAnimation (curLocation.x, curLocation.y);
				}
				// Set where we were.
				prevLocation = _transform.position;
			}
			// IF there is a Character script attached to this gameobject.
			if(isCharacter){
				// IF there is an Action Key Dialogue currently running.
				if(_character.isActionKeyDialogueRunning){
					// Handle the direction the NPC is looking.
					NPCLookDirection();
				}
			}
		}

		private void PlayAnimation(float hor, float vert){

			// IF the NPC has a Four Direction Animation,
			// ELSE IF the NPC has a Eight Direction Animation.
			if (_fourDirAnim) {
				// Play animations.
				Grid.helper.FourDirectionAnimation (hor, vert, _animator);
			} else if (_eightDirAnim) {
				// Play animation.
				Grid.helper.EightDirectionAnimation (hor, vert, _animator);
			} else if (FlipToFace) {
				// IF we want to flip the sprite renderers X.
				if(FlipX){
					// IF we are moving to the left,
					// ELSE IF we are moving to the right,
					if (hor < 0f) {
						_renderer.flipX = false;
					} else if (hor > 0f) {
						_renderer.flipX = true;
					}
				}

				// IF we want to flip the sprite renderers Y.
				if(FlipY){
					// IF we are moving Down,
					// ELSE IF we are moving Up,
					if (vert < 0f) {
						_renderer.flipY = false;
					} else if (vert > 0f) {
						_renderer.flipY = true;
					}
				}
			}
		}

		private void NPCLookDirection(){
			// Store the focused objects Transform.
			Transform focTransform = _character.actionKeyFocusTarget.transform;
			// IF we have a Four Direction Animation for this gameobject,
			// ELSE IF we have a Eight Direction Animation for this gameobject.
			if(_fourDirAnim){
				// Make this gameobjects animation face in the direction desired.
				Grid.helper.FourDirectionAnimation(focTransform.position.x - _transform.position.x, 
					focTransform.position.y - _transform.position.y,
					false,
					_animator);
			}else if(_eightDirAnim){
				// Make this gameobjets animation face in the direction desired.
				Grid.helper.EightDirectionAnimation(focTransform.position.x - _transform.position.x, 
					focTransform.position.y - _transform.position.y,
					false,
					_animator);
			}
		}
	}
}
