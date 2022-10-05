using UnityEngine;
using System.Collections;

namespace TrollBridge {

	public class Update_OrderInLayer_Moving : MonoBehaviour {

		private SpriteRenderer spriteRend;
		private Transform trans;

		void Start(){
			spriteRend = GetComponent<SpriteRenderer> ();
			trans = GetComponent<Transform> ();
			spriteRend.sortingOrder = (int)(trans.position.y * -1000);
		}

		void Update(){
			spriteRend.sortingOrder = (int)(trans.position.y * -1000);
		}
	}
}
