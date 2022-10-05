using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

namespace TrollBridge {

	public class Item_Slot : MonoBehaviour, IDropHandler {
		[ReadOnlyAttribute]
		public int slotNumber;
		private Inventory inv;

		void Start(){
			inv = GetComponentInParent<Inventory>();
		}

		public void OnDrop (PointerEventData data)
		{
			if (data.button == PointerEventData.InputButton.Left) {
				// Get the Item_Data component from the GameObject that was being dragged and now being dropped via the mouse.
				Item_Data droppedItem = data.pointerDrag.GetComponent<Item_Data> ();

				// IF we are dropping an item on an empty inventory slot.
				// ELSE IF we are dropping an item on a inventory slot that is occupied.
				if (inv.items [slotNumber].ID == -1) {
					// Clear the old slot.
					inv.items [droppedItem.slotNumber] = new Item ();
					// Set the item to the new slot.
					inv.items [slotNumber] = droppedItem.item;
					// Set this slot number to the new slot number.
					droppedItem.slotNumber = slotNumber;
					// Rename to the appropriate slot number based on the slot it was moved to.
					droppedItem.name = "Item Slot " + slotNumber + " - " + inv.items[slotNumber].Title;
				} else if (droppedItem.slotNumber != slotNumber) {
					// Get the Transform of the child, which is the Item currently in this slot.
					Transform item = transform.GetChild (0);
					// Set the slot number of the item in this current slot to be set to the slot of the dragged item.
					item.GetComponent<Item_Data> ().slotNumber = droppedItem.slotNumber;
					// Set the parent of this item to the slot number of the dragged item.
					item.transform.SetParent (inv.slots [droppedItem.slotNumber].transform);
					// Set the scale to 1.
					item.transform.localScale = Vector2.one;
					// Set the position of the item to where the dragged item was.
					item.transform.position = inv.slots [droppedItem.slotNumber].transform.position;

					// Set the dragged items slot number to this current slot number.
					droppedItem.slotNumber = slotNumber;
					// Set the parent of the dragged item to the transform of this gameobject.
					droppedItem.transform.SetParent (transform);
					// Set the scale to 1.
					droppedItem.transform.localScale = Vector2.one;
					// Set the position of the dragged item to the position of this gameobject.
					droppedItem.transform.position = transform.position;

					// Swap the items in the inventory array.
					inv.items [droppedItem.slotNumber] = droppedItem.item;
					inv.items [item.GetComponent<Item_Data> ().slotNumber] = item.GetComponent<Item_Data> ().item;

					// Change the text of the Item_Data GameObject.
					item.name = "Item Slot " + item.GetComponent<Item_Data> ().slotNumber + " - " + item.GetComponent<Item_Data> ().item.Title;
					droppedItem.name = "Item Slot " + slotNumber + " - " + inv.items[slotNumber].Title;
				}
			}
		}
	}
}
