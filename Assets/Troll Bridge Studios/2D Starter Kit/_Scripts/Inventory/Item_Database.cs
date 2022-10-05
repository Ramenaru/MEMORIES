using UnityEngine;
using System.Collections.Generic;
using LitJson;
using System.IO;
using System;

namespace TrollBridge {

	public class Item_Database : MonoBehaviour {

		private List<Item> database = new List<Item>();
		private JsonData itemData;


		void Start(){
			itemData = JsonMapper.ToObject (File.ReadAllText(Application.dataPath + "/StreamingAssets/Items.json"));
			ConstructItemDatabase ();
		}

		public Item FetchItemByID(int id)
		{
			for(int i = 0; i < database.Count; i++)
			{
				if(database[i].ID == id)
				{
					return database[i];
				}
			}
			return null;
		}

		private void ConstructItemDatabase()
		{
			for (int i = 0; i < itemData.Count; i++) {
				database.Add (new Item (
					(int)itemData [i] ["id"],
					itemData [i] ["title"].ToString (), 
					(int)itemData [i] ["value"],
					itemData [i] ["rarity"].ToString (),
					itemData [i] ["type"].ToString (),
					(int)itemData [i] ["color"] ["r"],
					(int)itemData [i] ["color"] ["g"],
					(int)itemData [i] ["color"] ["b"],
					(int)itemData [i] ["color"] ["a"],
					(int)itemData [i] ["stats"] ["damage"],
					(int)itemData [i] ["stats"] ["armour"],
					(int)itemData [i] ["stats"] ["magicarmour"],
					(int)itemData [i] ["stats"] ["movespeed"],
					(int)itemData [i] ["stats"] ["health"],
					(int)itemData [i] ["stats"] ["mana"],
					(int)itemData [i] ["usage"] ["restorehp"],
					(int)itemData [i] ["stats"] ["bonushealth"],
					itemData [i] ["description"].ToString (),
					(bool)itemData [i] ["stackable"],
					itemData [i] ["slug"].ToString (),
					itemData [i] ["pickupsound"].ToString (),
					itemData [i] ["usedsound"].ToString ()
				));
			}
		}
	}
		
	public class Item
	{
		public int ID { get; set; }
		public string Title { get; set; }
		public int Value { get; set; }
		public string Rarity { get; set; }
		public string Type { get; set; }
		public float R { get; set; }
		public float G { get; set; }
		public float B { get; set; }
		public float A { get; set; }
		public int Damage { get; set; }
		public int Armour { get; set; }
		public int MagicArmour { get; set; }
		public float MoveSpeed { get; set; }
		public int Health { get; set; }
		public int Mana { get; set; }
		public int RestoreHP { get; set; }
		public float BonusHealth { get; set; }
		public string Description { get; set; }
		public bool Stackable { get; set; }
		public string Slug { get; set; }
		public Sprite SpriteImage { get; set; }
		public AudioClip PickUpSound { get; set; }
		public AudioClip UsedSound { get; set; }

		public Item(int id, string title, int value, string rarity, string type, float r, float g, float b, float a, int damage, int armour, int magicArmour, float moveSpeed, int health, int mana, int restoreHP, float bonusHealth, string description, bool stackable, string slug, string pickupSound, string usedSound)
		{
			this.ID = id;
			this.Title = title;
			this.Value = value;
			this.Rarity = rarity;
			this.Type = type;
			this.R = r / 255f;
			this.G = g / 255f;
			this.B = b / 255f;
			this.A = a / 255f;
			this.Damage = damage;
			this.Armour = armour;
			this.MagicArmour = magicArmour;
			this.MoveSpeed = moveSpeed;
			this.Health = health;
			this.Mana = mana;
			this.RestoreHP = restoreHP;
			this.BonusHealth = bonusHealth;
			this.Description = description;
			this.Stackable = stackable;
			this.Slug = slug;
			this.SpriteImage = Grid.setup.GetSprite (slug);
			this.PickUpSound = Resources.Load<AudioClip> ("Sounds/" + pickupSound);
			this.UsedSound = Resources.Load<AudioClip> ("Sounds/" + usedSound);
		}

		public Item()
		{
			this.ID = -1;
		}
	}
}
