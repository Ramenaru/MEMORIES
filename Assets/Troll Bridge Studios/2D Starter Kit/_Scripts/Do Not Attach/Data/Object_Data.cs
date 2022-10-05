using UnityEngine;
using System;

namespace TrollBridge {

	[Serializable]
	public class Object_Data {
		
		// Transform information.
		public bool active;
		public string name;
		public int layer;
		public float xPos;
		public float yPos;
		public float zPos;

		// Sprite Renderer Information
		public string spriteName = "";
		public string sortLayerName = "";
		public int sortLayerOrder = 0;

		// Collider2D Information.
		public bool isCollider = false;
		public bool activeCollider;

		public void StoreData(GameObject go){
			// Save the activeness.
			active = go.activeInHierarchy;
			// Save the GameObjects name.
			name = go.name;
			// Save the GameObjects layer.
			layer = go.layer;
			// Save the GameObjects position.
			xPos = go.transform.position.x;
			yPos = go.transform.position.y;
			zPos = go.transform.position.z;
			// IF this GameObject has a sprite renderer,
			// ELES it doesn't have a sprite renderer.
			if (go.GetComponent<SpriteRenderer> () != null) {
				// Save the sprite name, sorting layer name and the sorting layer order.
				spriteName = go.GetComponent<SpriteRenderer> ().sprite.name;
				sortLayerName = go.GetComponent<SpriteRenderer> ().sortingLayerName;
				sortLayerOrder = go.GetComponent<SpriteRenderer> ().sortingOrder;
			}
			// IF there is a Collider2D attached,
			// ELSE there is not a Collider2D attached.
			if (go.GetComponent<Collider2D> () != null) {
				isCollider = true;
				activeCollider = go.GetComponent<Collider2D> ().enabled;
			}
		}
	}
}
