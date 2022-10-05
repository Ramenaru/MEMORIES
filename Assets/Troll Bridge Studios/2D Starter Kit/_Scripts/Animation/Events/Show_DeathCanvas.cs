using UnityEngine;
using System.Collections;

namespace TrollBridge {

public class Show_DeathCanvas : MonoBehaviour {

		public void ShowDeathCanvas()
		{
			// Start a coroutine that moves the Death Panel from 1 point to another point.
			StartCoroutine(Grid.deathPanel.MoveStartEnd(0.5f));
		}
	}
}
