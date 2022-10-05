using UnityEngine;
using System.Collections;

namespace TrollBridge {

	[RequireComponent(typeof(Camera))]
	public class Camera_Follow_Player : MonoBehaviour {

		// Used for the user to input their board section width and height.
		[Tooltip("The desired Camera Width.")]
		public int cameraWidth;
		[Tooltip("The desired Camera Height.  If you have a UI that is not transparent then you will need to incorporate the height of the UI for this.")]
		public int cameraHeight;

		// Boolean to decide if the camera can move.
		private bool canCameraMove = true;
		// 4 GameObjects used to set the boundaries of the camera.
		public GameObject bottomCameraBorder;
		public GameObject leftCameraBorder;
		public GameObject topCameraBorder;
		public GameObject rightCameraBorder;

		// Used to store the player GameObject.
		private GameObject playerGO;
		private Transform playerTransform;

		// The gameobjects Transform.
		private Transform _transform;

		// The Camera and min and max bounds.
		private Camera _camera;
		private float boundMinX;
		private float boundMaxX;
		private float boundMinY;
		private float boundMaxY;

		// Used for Camera bounds.
		private float leftBound;
		private float rightBound;
		private float bottomBound;
		private float topBound;

		// The cameras x and y.
		private float camX;
		private float camY;

		// Border float holders for the camera boundaries.
		private float bb, tb, lb, rb;

		[Tooltip("The offset based on the height of your UI.  If the background of your UI is transparent like Zelda Link to the Past then leaving this number at 0 will center the player in the middle of the monitor screen.  " +
			"If the background of your UI is not transparent then setting this number to the size of your UI height will make sure that the player is centered based on what is visably left over from the camera.")]
		public float UIVerticalOffset;
		

		void Awake(){
			// Set the transform.
			_transform = gameObject.transform;
			// Grab the Camera Component.
			_camera = Camera.main;
			// Adjust the Camera ratios.
			Grid.helper.SetCameraRatios((float)cameraWidth, (float)cameraHeight, Screen.width, Screen.height);
			// If the camera is able to move then we need to set the boundaries.
			if(canCameraMove){
				// Set the bottom, left, top and right borders.
				SetCameraBounds(bottomCameraBorder, topCameraBorder, leftCameraBorder, rightCameraBorder);
			}
			// Check to make sure the user has the scripts working correctly.
			DebugCheck();
		}

		/// <summary>
		/// We check to see whenever the player is spawned in the scene and then we act.
		/// </summary>
		void Update () {
			// IF the player gameobject is null.
			if(playerGO == null){
				// Get the player GameObject.
				playerGO = Character_Manager.GetPlayer();
				return;
			}
			// IF the players reference to its transform is null.
			if(playerTransform == null){
				// Grab the transform.
				playerTransform = playerGO.transform;
			}
			// IF the camera is capable of moving.
			if(canCameraMove){
				// Refresh the camera bounds.
				RefreshCameraBounds();
				// Clamp values between the bounds.
				camX = Mathf.Clamp(playerTransform.position.x, leftBound, rightBound);
				camY = Mathf.Clamp(playerTransform.position.y + UIVerticalOffset/200f, bottomBound, topBound);
			}
		}

		void LateUpdate(){
			// IF the camera can move.
			if(canCameraMove){
				_transform.position = new Vector3(camX, camY, _transform.position.z);
			}
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
			// IF the Bottom Camera Border is not setup with a GameObject AND we have a moving camera.
			if(bottomCameraBorder == null && canCameraMove){
				Grid.helper.DebugErrorCheck(5, this.GetType(), gameObject);
			}
			// IF the Top Camera Border is not setup with a GameObject AND we have a moving camera.
			if(topCameraBorder == null && canCameraMove){
				Grid.helper.DebugErrorCheck(6, this.GetType(), gameObject);
			}
			// IF the Left Camera Border is not setup with a GameObject AND we have a moving camera.
			if(leftCameraBorder == null && canCameraMove){
				Grid.helper.DebugErrorCheck(7, this.GetType(), gameObject);
			}
			// IF the Right Camera Border is not setup with a GameObject AND we have a moving camera.
			if(rightCameraBorder == null && canCameraMove){
				Grid.helper.DebugErrorCheck(8, this.GetType(), gameObject);
			}
			// IF the user mixed up the Top Camera Border and the Bottom Camera Border.
			if(bb > tb){
				Grid.helper.DebugErrorCheck(9, this.GetType(), gameObject);
			}
			// IF the user mixed up the Left Camera Border and the Right Camera Border.
			if(lb > rb){
				Grid.helper.DebugErrorCheck(10, this.GetType(), gameObject);
			}
		}

		void SetXBounds(float minX, float maxX){
			// Set min and max x bounds.
			boundMinX = minX;
			boundMaxX = maxX;
		}

		void SetYBounds(float minY, float maxY){
			// Set min and max y bounds
			boundMinY = minY;
			boundMaxY = maxY;
		}

		void RefreshCameraBounds(){
			// Get the camera ratios.
			float camVertExtent = _camera.orthographicSize;
			float camHorzExtent = _camera.aspect * camVertExtent;
			
			// Grab the current bounds.
			leftBound   = boundMinX + camHorzExtent;
			rightBound  = boundMaxX - camHorzExtent;
			bottomBound = boundMinY + camVertExtent;
			topBound    = boundMaxY - camVertExtent;
		}

		/// <summary>
		/// Set the mode of the camera to be either controled by what this script is supposed to do or set canCameraMove to false so you can control the camera for your needs.
		/// </summary>
		/// <param name="canMove">If set to <c>true</c> can move.</param>
		public void CanControlCamera(bool canMove){
			canCameraMove = canMove;
		}

		public void SetCameraBounds(GameObject _bottomCameraBorder, GameObject _topCameraBorder, GameObject _leftCameraBorder, GameObject _rightCameraBorder){
			// Grabbing the collider2d and the renderer if they exists for future checks.
			Collider2D _BColl = _bottomCameraBorder.GetComponent<Collider2D>();
			Renderer _BRend = _bottomCameraBorder.GetComponent<Renderer>();
			// Grabbing the collider2d and the renderer if they exists for future checks.
			Collider2D _TColl = _topCameraBorder.GetComponent<Collider2D>();
			Renderer _TRend = _topCameraBorder.GetComponent<Renderer>();
			// Grabbing the collider2d and the renderer if they exists for future checks.
			Collider2D _LColl = _leftCameraBorder.GetComponent<Collider2D>();
			Renderer _LRend = _leftCameraBorder.GetComponent<Renderer>();
			// Grabbing the collider2d and the renderer if they exists for future checks.
			Collider2D _RColl = _rightCameraBorder.GetComponent<Collider2D>();
			Renderer _RRend = _rightCameraBorder.GetComponent<Renderer>();
			
			// let's check, 
			// IF the bounding border is an invisible Collider object, Border. (Collider && !Renderer),
			// ELSE IF the bounding border is a visable Sprite || Collider Sprite, Ground Tile || Wall Tile.,
			// ELSE then the user has just made a point where they do not want the camera to go past -- Just a plain GameObject.

			// Bottom Collider check.
			if((_BColl != null && _BRend == null)){
				bb = _BColl.bounds.max.y;
			}else if((_BColl != null && _BRend != null) || (_BColl == null && _BRend != null)){
				bb = _BRend.bounds.min.y;
			}else {
				bb = _bottomCameraBorder.transform.position.y;
			}

			// Top Collider check.
			if((_TColl != null && _TRend == null)){
				tb = _TColl.bounds.min.y;
			}else if((_TColl != null && _TRend != null) || (_TColl == null && _TRend != null)){
				tb = _TRend.bounds.max.y;
			}else {
				tb = _topCameraBorder.transform.position.y;
			}

			// Left Collider check.
			if((_LColl != null && _LRend == null)){
				lb = _LColl.bounds.max.x;
			}else if((_LColl != null && _LRend != null) || (_LColl == null && _LRend != null)){
				lb = _LRend.bounds.min.x;
			}else {
				lb = _leftCameraBorder.transform.position.x;
			}

			// Right Collider check.
			if((_RColl != null && _RRend == null)){
				rb = _RColl.bounds.min.x;
			}else if((_RColl != null && _RRend != null) || (_RColl == null && _RRend != null)){
				rb = _RRend.bounds.max.x;
			}else {
				rb = _rightCameraBorder.transform.position.x;
			}

			// Set the new bounds.
			SetXBounds(lb, rb);
			SetYBounds(bb, tb);
			RefreshCameraBounds();
		}
	}
}
