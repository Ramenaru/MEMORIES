using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

namespace TrollBridge {

	public class Item_Data : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler {

		[ReadOnlyAttribute]
		public Item item;
		[ReadOnlyAttribute]
		public int amount;
		[ReadOnlyAttribute]
		public int slotNumber;

		private CanvasGroup canvasGroup;
		// Radius that handles the distance an object is created away from its creater (The player in this case.).
		private float RADIUS = 0.35f;


		void Awake(){
			canvasGroup = GetComponent<CanvasGroup> ();
		}

		public void OnPointerEnter(PointerEventData data)
		{
			// IF we have a tooltip.
			if(Grid.tooltip != null){
				// Show the tooltip.
				Grid.tooltip.Activate (item);
			}
		}

		public void OnPointerExit(PointerEventData data)
		{
			// IF we have a tooltip.
			if(Grid.tooltip != null){
				// Do not show the tooltip.
				Grid.tooltip.Deactivate ();
			}
		}

		public void OnPointerDown(PointerEventData data)
		{
			// Do nothing, but this is needed so that OnPointerUp works.
		}

		public void OnPointerUp(PointerEventData data)
		{
			// IF we Right Click so we can USE or EQUIP an item.
			if(item != null && data.button == PointerEventData.InputButton.Right && !data.dragging)
			{
				// Get the Player_Manager component.
				Player_Manager player = Character_Manager.GetPlayerManager ().GetComponent<Player_Manager> ();
				// IF this item is a consumable,
				if (item.Type == "Consumable") 
				{
					// Add "usage" attributes associated with this item.  At this current time if you are looking at the demo we only add HP.
					player.AddHealth ((float)item.RestoreHP);
					// Play the Pickup Sound.
					Grid.soundManager.PlaySound (item.UsedSound);
					if(amount - 1 == 0){
						// Clear the old slot.
						GetComponentInParent<Inventory>().items [slotNumber] = new Item ();
						// Destroy this gameobject as it holds no item.
						Destroy (gameObject);
						// Do not show the tooltip.
						Grid.tooltip.Deactivate ();
						return;
					}
					--amount;
					GetComponentInParent<Inventory>().slots [slotNumber].GetComponentInChildren<Text> ().text = amount.ToString();
					return;
				} 
				// IF this item is a piece of equipment, such as a Weapon, Armour, Ring or a Bracelet.
				if(item.Type == "Weapon" || item.Type == "Armour" || item.Type == "Ring" || item.Type == "Bracelet")
				{
					// Get the Equipment component.
					Equipment equipment = player.GetComponentInChildren<Equipment> ();
					// Play the Pickup Sound.
					Grid.soundManager.PlaySound (item.UsedSound);
					Item swappedItem = null;
					if (item.Type == "Weapon") 
					{
						// Set the new weapon to the player while returning the old weapon (if the player wielded one).
						swappedItem = equipment.EquipWeapon (item);
					}
					else if(item.Type == "Armour")
					{
						// Set the new Armour to the player while returning the old Armour (if the player wearing one).
						swappedItem = equipment.EquipArmour (item);
					}
					else if(item.Type == "Ring")
					{
						// Set the new Ring to the player while returning the old Ring (if the player wearing one).
						swappedItem = equipment.EquipRing (item);
					}
					else if(item.Type == "Bracelet")
					{
						// Set the new Bracelet to the player while returning the old Bracelet (if the player wearing one).
						swappedItem = equipment.EquipBracelet (item);
					}

					// Remove this item from this List of Items.
					GetComponentInParent<Inventory>().items [slotNumber] = new Item ();
					// IF we have an item being swapped out.
					if(swappedItem != null){
						// Add the swapped out equipped item to the inventory.
						GetComponentInParent<Inventory>().AddItem (swappedItem.ID, 1);
					}
					// Do not show the tooltip.
					Grid.tooltip.Deactivate ();
					// Destroy this item in the inventory list.
					Destroy (gameObject);
				}
			}
		}

		public void OnBeginDrag(PointerEventData data)
		{
			// IF there is an item.
			if(item != null && data.button == PointerEventData.InputButton.Left)
			{
				this.transform.SetParent (this.transform.parent.parent);
				this.transform.position = data.position;
				canvasGroup.blocksRaycasts = false;
			}
		}

		public void OnDrag(PointerEventData data)
		{
			// IF there is an item.
			if(item != null && data.button == PointerEventData.InputButton.Left)
			{
				// Set the position where the mouse is.
				this.transform.position = data.position;
			}
		}

		public void OnEndDrag(PointerEventData data)
		{
			if (data.button == PointerEventData.InputButton.Left) 
			{
				// IF we are not hovering the mouse over the inventory then drop the item on the ground.
				if (data.hovered.Count == 0) 
				{
					// Spawn the item from it being thrown out of the inventory.
					GameObject goItem = Grid.helper.SpawnObject (Grid.setup.GetGameObjectPrefab (item.Title), Character_Manager.GetPlayer ().transform.position, Quaternion.identity, Character_Manager.GetPlayer (), RADIUS);
					// Launch the item in a random direction.
					Grid.helper.LaunchItemAwayFromPosition (goItem, Character_Manager.GetPlayer ().transform.position);
					// Clear the old slot.
					GetComponentInParent<Inventory>().items [slotNumber] = new Item ();
					// Destroy this gameobject.
					Destroy (this.gameObject);
					// We done so lets GTFO of here.
					return;
				}
				// IF we actually are dragging an item.
				if (item != null) 
				{
					// Set this items parent back to the original slot.
					this.transform.SetParent (GetComponentInParent<Inventory>().slots [slotNumber].transform);
					// Set its local position back to 0 so it can be centered in the slot.
					this.transform.localPosition = Vector2.zero;
					// Make sure the scale is set properly to 1.
					this.transform.localScale = Vector2.one;
					// Allow us to block raycast again.
					canvasGroup.blocksRaycasts = true;
				}
			}
		}
	}
}
