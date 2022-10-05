using UnityEngine;

namespace TrollBridge {

	public enum CharacterType {Hero, Enemy, Neutral, All}
	public abstract class Character : MonoBehaviour {

		// The type of character this is. The hero (Player), Enemy (normal mobs / bosses) or Neutral.
		public CharacterType characterType;
		// The GameObject that represents the character entity.
		public GameObject characterEntity;

		public bool CanMove = true;
		public float AlterSpeed = 1f;
		public bool CanAttack = true;
		public bool ImmuneToSlow = false;
		public bool ImmuneToStun = false;
		public bool ImmuneToSilence = false;
		public bool Interactable = false;

		// The Character Animator.
		public Animator CharacterAnimator;
		// Is there a Four Direction Animation.
		public bool FourDirAnim = false;
		// Is there a Eight Direction Animation.
		public bool EightDirAnim = false;

		// Is there a Action Key Dialogue currently running.
		public bool isActionKeyDialogueRunning = false;
		// The focus target for the Action Key Dialogue.
		public GameObject actionKeyFocusTarget;

		// Can this character be jolted (knockbacked) when taking damage;
		public bool CanBeJolted;
		// Are we currently being jolted.
		public bool currentlyJolted = false;
		// Options for player interactions.
		public float HitAnimationTime = 0.2f;

//		// The characters base damage.
//		public float DefaultDamage = 0f;
//	//	[ReadOnlyAttribute]
//	//	public float BaseDamage;
//		[ReadOnlyAttribute]
//		public float CurrentDamage;
//
//		// The characters base health.
//		public float DefaultHealth = 3f;
//		public float DefaultMaxHealth = 5f;
//	//	[ReadOnlyAttribute]
//	//	public float BaseHealth;
//		[ReadOnlyAttribute]
//		public float MaxHealth;
//		[ReadOnlyAttribute]
//		public float CurrentHealth;
//
//		// The characters base mana.
//		public float DefaultMana = 20f;
//		public float DefaultMaxMana = 20f;
//	//	[ReadOnlyAttribute]
//	//	public float BaseMana;
//		[ReadOnlyAttribute]
//		public float MaxMana;
//		[ReadOnlyAttribute]
//		public float CurrentMana;
//
//		// The characters base movement speed.
//		public float DefaultMoveSpeed = 1f;
//	//	[ReadOnlyAttribute]
//	//	public float BaseMoveSpeed;
//		[ReadOnlyAttribute]
//		public float CurrentMoveSpeed;

		// The audio clip for when this character gets hit.
		public AudioClip GetHitSound;
		// The audio clip for when this character dies.
		public AudioClip DieSound;
		// The after effects from dying. (Think of like a poof of clouds when something dies.)
		public GameObject afterDeathVisual;

		void OnEnable () {
			// Add this to our List.
			Character_Manager.Register (this);
		}

		void OnDisable () {
			// Remove from our List.
			Character_Manager.Unregister (this);
		}
	}
}
