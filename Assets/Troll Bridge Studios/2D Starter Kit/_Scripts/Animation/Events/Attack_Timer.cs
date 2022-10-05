using UnityEngine;
using System.Collections;

namespace TrollBridge{

	public class Attack_Timer : MonoBehaviour {

		private Animator characterAnimator;
		private Player_Manager playerManager;

		void Start(){
			characterAnimator = GetComponent<Animator> ();
			playerManager = GetComponentInParent<Player_Manager> ();
		}

		public void AttackTimer(){
			characterAnimator.SetBool ("IsAttacking", false);
			playerManager.CanMove = true;
		}
	}
}
