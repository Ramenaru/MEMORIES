using UnityEngine;

namespace TrollBridge{

	static class Grid {

		public static Helper_Manager helper;
		public static Sound_Manager soundManager;
		public static State_Manager stateManager;
		public static Item_Database itemDataBase;
		public static Inventory inventory;
		public static Tooltip tooltip;
		public static Options_Manager optionManager;
		public static Setup setup;
		public static Move_GameObject deathPanel;


		static Grid(){
			GameObject g;
			g = SafeFind("_Holder");
			helper = (Helper_Manager)SafeComponent (g, "Helper_Manager");
			itemDataBase = (Item_Database)SafeComponent (g, "Item_Database");
			inventory = (Inventory)SafeComponent (g, "Inventory");
			tooltip = (Tooltip)SafeComponent (g, "Tooltip");
			stateManager = (State_Manager)SafeComponent (g, "State_Manager");
			setup = (Setup)SafeComponent (g, "Setup");

			g = SafeFind ("Sound_Manager");
			soundManager = (Sound_Manager)SafeComponent (g, "Sound_Manager");

			g = SafeFind ("OptionsCanvas");
			optionManager = (Options_Manager)SafeComponent (g, "Options_Manager");

			g = SafeFind ("Death Panel");
			deathPanel = (Move_GameObject)SafeComponent (g, "Move_GameObject");
		}

		private static GameObject SafeFind(string s){
			GameObject g = GameObject.Find (s);
			if(g == null){
				BigProblem ("The " +s+ " GameObject is not in this scene.");
			}
			return g;
		}

		private static Component SafeComponent(GameObject g, string s){
			Component c = g.GetComponent (s);
			if(c == null){
				BigProblem ("The " +s+ " Component is not attached to the " +g.name+ " GameObject.");
			}
			return c;
		}

		private static void BigProblem(string error){
			Debug.Log ("Cannot proceed : " + error);
			Debug.Break ();
		}
	}
}