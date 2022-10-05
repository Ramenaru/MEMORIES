using UnityEngine;
using System.Collections;

namespace TrollBridge {

	[RequireComponent (typeof (Rigidbody2D))]
	public class Multi_Direction_Movement : MonoBehaviour {

		// Vector direction we are moving.
		private Vector2 movement;
		// The GameObjects Rigidbody.
		private Rigidbody2D rb;
		// The Player State
		private Player_Manager _playerManager;
		// The Character Stats.
		private Character_Stats charStats;
		// Holders for the movements.
		private float moveHorizontal;
		private float moveVertical;


		void Awake(){
			// Get the Player State.
			_playerManager = GetComponentInParent<Player_Manager>();
			// Get the Players Stats as we use that to potentially alter movement.
			charStats = _playerManager.GetComponentInChildren<Character_Stats> ();
			// Get the Rigidbody2D Component.
			rb = GetComponent<Rigidbody2D>();
		}

		void Update(){
			// IF we are allowed to move.
			if(_playerManager.CanMove){
				// Get a -1, 0 or 1.
				moveHorizontal = Input.GetAxis ("Horizontal");
				moveVertical = Input.GetAxis ("Vertical");
			}
		}
		
		void FixedUpdate() {
			// IF we are able to move.
			// ELSE IF we cannot move.
			if(_playerManager.CanMove){
				// Get Vector2 direction.
				movement = new Vector2(moveHorizontal * _playerManager.PlayerInvertX, moveVertical * _playerManager.PlayerInvertY);
				// Apply direction with speed, alterspeed and if we have the ability to even move.
				movement *= charStats.CurrentMoveSpeed * _playerManager.AlterSpeed;
				// IF the user has an animation set and ready to go.
				PlayAnimation(moveHorizontal, moveVertical);
				// Apply force.
				rb.AddForce(movement);
			}else if(!_playerManager.currentlyJolted){
				rb.velocity = Vector2.zero;
			}
		}

		void PlayAnimation(float hor, float vert){
			// IF the user has an animation set and ready to go.
			if (_playerManager.CharacterAnimator != null) {
				// IF the player has a Four Direction Animation,
				// ELSE IF the player has a Eight Direction Animation.
				if (_playerManager.FourDirAnim) {
					// Play animations.
					Grid.helper.FourDirectionAnimation (hor * _playerManager.PlayerInvertX, vert * _playerManager.PlayerInvertY, _playerManager.CharacterAnimator);
				} else if (_playerManager.EightDirAnim) {
					// Play animation.
					Grid.helper.EightDirectionAnimation (hor * _playerManager.PlayerInvertX, vert * _playerManager.PlayerInvertY, _playerManager.CharacterAnimator);
				}
			}
		}
	}
}
