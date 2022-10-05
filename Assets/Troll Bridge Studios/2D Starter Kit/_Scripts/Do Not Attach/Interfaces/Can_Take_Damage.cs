using UnityEngine;
using System.Collections;

namespace TrollBridge {

	/// <summary>
	/// Interface for anything that can take damage.
	/// </summary>
	public interface Can_Take_Damage{
		void TakeDamage(float damage, Transform otherTransform, float joltAmount);
	}
}
