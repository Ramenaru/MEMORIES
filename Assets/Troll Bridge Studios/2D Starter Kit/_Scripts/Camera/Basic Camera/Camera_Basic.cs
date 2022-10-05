using UnityEngine;
using System.Collections;

namespace TrollBridge {

	[RequireComponent(typeof(Camera))]
	public class Camera_Basic : MonoBehaviour {

		// Used for the user to input their board section width and height.
		[Tooltip("The desired Camera Width.")]
		public int cameraWidth;
		[Tooltip("The desired Camera Height.")]
		public int cameraHeight;


		void Awake () {
			// Set the camera ratios.
			Grid.helper.SetCameraRatios((float)cameraWidth, (float)cameraHeight, 
			                                        Screen.width, Screen.height);
		}

		void Start(){
			// Check to make sure the user has the scripts working correctly.
			DebugCheck();
		}

		void DebugCheck(){
			// IF the user forgot to put in the Cameras Width.
			if(cameraWidth <= 0){
				Grid.helper.DebugErrorCheck(2, this.GetType(), gameObject);
			}
			// IF the user forgot to put in the Cameras Height.
			if(cameraHeight <= 0){
				Grid.helper.DebugErrorCheck(3, this.GetType(), gameObject);
			}
		}
	}
}
