using UnityEngine;
using System.Collections;

namespace TrollBridge {
	
	public class Change_Layers : MonoBehaviour {

		// The bool to decide if we should do any action on Entering the Collider.
		public bool enter = true;
		// The new Layer Name when a collision enter happens.
		public LayerMask enterLayerName;
		// The bool to decide if we should do any action on Exiting the Collider.
		public bool exit;
		// The new Layer Name when a collision exit happens.
		public LayerMask exitLayerName;


		void OnTriggerEnter2D(Collider2D coll){
			// IF we care about entering collision.
			if(enter){
				// Change the layer.
				ChangeLayer (coll, enterLayerName.value);
			}
		}

		void OnTriggerExit2D(Collider2D coll){
			// IF we care about exiting collision.
			if(exit){
				// Change the layer.
				ChangeLayer (coll, exitLayerName.value);
			}
		}

		private void ChangeLayer(Collider2D coll, int layerMaskValue){
			// Change the Collision Layer of the colliding GameObject.
			coll.gameObject.layer = (int) Mathf.Log (layerMaskValue, 2);
			// Loop through all the children.
			for (int i = 0; i < coll.gameObject.transform.childCount; i++) {
				// Get the child of this gameobject.
				GameObject child = coll.gameObject.transform.GetChild (i).gameObject;
				// Change the Collision Layer of the colliding GameObject.
				child.layer = (int) Mathf.Log (layerMaskValue, 2);
			}
		}
	}
}
