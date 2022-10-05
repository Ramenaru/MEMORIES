using UnityEngine;
using System.Collections;

namespace TrollBridge {
	
	public class Change_SortingLayers : MonoBehaviour {

		public bool enter;
		public string enterSortLayerName;
		public int enterSortOrderNumber;
		public bool exit;
		public string exitSortLayerName;
		public int exitSortOrderNumber;
		public int enterPopupSelection = 0;
		public int exitPopupSelection = 0;


		void OnTriggerEnter2D(Collider2D coll){
			// IF we care about entering collision.
			if(enter){
				// Change the sorting layer.
				ChangeSortingLayers (coll, enterSortLayerName, enterSortOrderNumber);
			}
		}

		void OnTriggerExit2D(Collider2D coll){
			// IF we care about exiting collision.
			if(exit){
				// Change the sorting layer.
				ChangeSortingLayers (coll, exitSortLayerName, exitSortOrderNumber);
			}
		}

		private void ChangeSortingLayers (Collider2D coll, string newSortLayerName, int newSortOrderNumber){
			// Apply changes to the parent GameObject.
			Change (coll.gameObject, newSortLayerName, newSortOrderNumber);
			// Loop through all the children.
			for (int i = 0; i < coll.gameObject.transform.childCount; i++) {
				// Get the child of this gameobject.
				GameObject child = coll.gameObject.transform.GetChild (i).gameObject;
				// Apply changes.
				Change (child, newSortLayerName, newSortOrderNumber);
			}
		}

		private void Change(GameObject coll, string newSortLayerName, int newSortOrderNumber){
			// If this GameObject has a SpriteRenderers
			if (coll.GetComponent<SpriteRenderer> () != null) {
				// Change the Sort Layer and Order Number.
				coll.GetComponent<SpriteRenderer> ().sortingLayerName = newSortLayerName;
				coll.GetComponent<SpriteRenderer> ().sortingOrder = newSortOrderNumber;
			}
		}
	}
}
