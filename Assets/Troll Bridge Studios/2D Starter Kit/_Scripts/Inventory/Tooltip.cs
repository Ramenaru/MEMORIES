using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace TrollBridge {

	public class Tooltip : MonoBehaviour {

		private Item item;
		private string data;
		public GameObject tooltip;

		void Start()
		{
			tooltip.SetActive (false);
		}

		void Update()
		{
			if(tooltip.activeInHierarchy)
			{
				tooltip.transform.position = Input.mousePosition;
			}
		}

		public void Activate(Item item)
		{
			this.item = item;
			ConstructDataString ();
			tooltip.SetActive (true);
		}

		public void Deactivate()
		{
			tooltip.SetActive (false);
		}

		/// <summary>
		/// This is where you construct your tooltip to display all the stats of your items.  You can alter text color from the example you see below. Be sure to look at what else Unity allows you to do.
		/// </summary>
		public void ConstructDataString()
		{
			// IF we have a Common rarity Item.
			// ELSE IF we have a Legendary rarity Item.
			if(item.Rarity == "Common")
			{
				data = "<color=#FFFFFFFF><b>" + item.Title + "</b></color>\n\n";
			}
			else if(item.Rarity == "Rare")
			{
				data = "<color=#2800FFFF><b>" + item.Title + "</b></color>\n\n";
			}
			else if(item.Rarity == "Legendary")
			{
				data = "<color=#FFB800FF><b>" + item.Title + "</b></color>\n\n";
			}
			// Check to see if there are any stats on this item.
			if(item.Damage != 0){
				string stat = "<color=#FFFFFFFF>Damage : +" + item.Damage + "</color>\n";
				data = string.Concat (data, stat);
			}
			if(item.Armour != 0){
				string stat = "<color=#FFFFFFFF>Armour : +" + item.Damage + "</color>\n";
				data = string.Concat (data, stat);
			}
			if(item.MagicArmour != 0){
				string stat = "<color=#FFFFFFFF>Magic Armour : +" + item.Damage + "</color>\n";
				data = string.Concat (data, stat);
			}
			if(item.MoveSpeed != 0){
				string stat = "<color=#FFFFFFFF>Movement Speed : +" + item.Damage + "</color>\n";
				data = string.Concat (data, stat);
			}
			if(item.Health != 0){
				string stat = "<color=#FFFFFFFF>Health : +" + item.Damage + "</color>\n";
				data = string.Concat (data, stat);
			}
			if(item.Mana != 0){
				string stat = "<color=#FFFFFFFF>Mana : +" + item.Damage + "</color>\n";
				data = string.Concat (data, stat);
			}
			// Display the item description.
			data = string.Concat (data,"\n" + item.Description);
			tooltip.transform.GetChild(0).GetComponent<Text>().text = data;
		}
	}
}
