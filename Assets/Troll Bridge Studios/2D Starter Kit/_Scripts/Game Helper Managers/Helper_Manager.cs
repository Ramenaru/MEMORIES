using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;
using System.Linq;


namespace TrollBridge {

	public class Helper_Manager : MonoBehaviour {

		public void SetTimeScale(float timeScale){
			// Set the time scaling.
			Time.timeScale = timeScale;
		}

		/// <summary>
		/// Instantiates 'objectToSpawn' at position 'pos' and set its layer to 'layer'.
		/// </summary>
		/// <returns>The object.</returns>
		/// <param name="objectToSpawn">Object to spawn.</param>
		/// <param name="pos">Position.</param>
		/// <param name="layer">Layer.</param>
		public GameObject SpawnObject(GameObject objectToSpawn, Vector3 pos, int layer){
			// Spawn the Object.
			GameObject goObject = Instantiate (objectToSpawn) as GameObject;
			// Set the position.
			goObject.transform.position = pos;
			// Set the layer to the GameObject that created this Object.
			goObject.layer = layer;
			// Return the Object.
			return goObject;
		}

		/// <summary>
		/// We create a methode for spawning GameObjects due to the monotonous work that would be done when anything is spawned.  
		/// We need to make the GameObject and its children's layers that are being created be the same as the GameObject who 
		/// spawned it and its Sprite Renderer sorting layers name needs to be the same as well.
		/// </summary>
		/// <returns>The Object Spawned.</returns>
		/// <param name="objectToSpawn">Object to spawn.</param>
		/// <param name="pos">Position.</param>
		/// <param name="quat">Quat.</param>
		/// <param name="dropper">Dropper.</param>
		public GameObject SpawnObject(GameObject objectToSpawn, Vector3 pos, Quaternion quat, GameObject dropper){
			// Spawn the Object.
			GameObject goObject = Instantiate (objectToSpawn, pos, quat) as GameObject;
			// Set the layer to the GameObject that created this Object.
			goObject.layer = dropper.layer;
			// IF the goObject GameObject has a Sprite Renderer AND the GameObject that spawned the goObject has a Sprite Renderer as well.
			if(goObject.GetComponent<SpriteRenderer> () != null && dropper.GetComponent<SpriteRenderer> () != null)
			{
				// Set the sorting layer name to the GameObject that created this Object.
				goObject.GetComponent<SpriteRenderer> ().sortingLayerName = dropper.GetComponent<SpriteRenderer> ().sortingLayerName;
			}
			// Loop through the amount of children this gameobject has.
			for(int i = 0; i < goObject.transform.childCount; i++)
			{
				// Set the childrens layer to the layer of the entity that dropped this GameObject.
				goObject.transform.GetChild(i).gameObject.layer = dropper.layer;
				// IF the goObject GameObject has a Sprite Renderer AND the GameObject that spawned the goObject has a Sprite Renderer as well.
				if(goObject.transform.GetChild(i).GetComponent<SpriteRenderer> () != null && dropper.GetComponent<SpriteRenderer> () != null)
				{
					// Set the sorting layer name to the GameObject that created this Object.
					goObject.transform.GetChild(i).GetComponent<SpriteRenderer> ().sortingLayerName = dropper.GetComponent<SpriteRenderer> ().sortingLayerName;
				}
			}
			// Return the Object.
			return goObject;
		}

		/// <summary>
		/// We create a methode for spawning GameObjects due to the monotonous work that would be done when anything is spawned.  
		/// We need to make the GameObject and its children's layers that are being created be the same as the GameObject who 
		/// spawned it and its Sprite Renderer sorting layers name needs to be the same as well.
		/// 
		/// This method spawns GameObjects on the circumference of a circle with a radius of "float radius".
		/// </summary>
		/// <returns>The object.</returns>
		/// <param name="objectToSpawn">Object to spawn.</param>
		/// <param name="pos">Position.</param>
		/// <param name="quat">Quat.</param>
		/// <param name="dropper">Dropper.</param>
		/// <param name="radius">Radius.</param>
		public GameObject SpawnObject(GameObject objectToSpawn, Vector3 pos, Quaternion quat, GameObject dropper, float radius){
			// Get a random spot with a radius around the pos.
			Vector3 randomPos = RandomCircle (pos, radius);
			// Spawn the Object.
			GameObject goObject = Instantiate (objectToSpawn, randomPos, quat) as GameObject;
			// Set the layer to the GameObject that created this Object.
			goObject.layer = dropper.layer;
			// IF the goObject GameObject has a Sprite Renderer AND the GameObject that spawned the goObject has a Sprite Renderer as well.
			if(goObject.GetComponent<SpriteRenderer> () != null && dropper.GetComponent<SpriteRenderer> () != null)
			{
				// Set the sorting layer name to the GameObject that created this Object.
				goObject.GetComponent<SpriteRenderer> ().sortingLayerName = dropper.GetComponent<SpriteRenderer> ().sortingLayerName;
			}
			// Loop through the amount of children this gameobject has.
			for(int i = 0; i < goObject.transform.childCount; i++)
			{
				// Set the childrens layer to the layer of the entity that dropped this GameObject.
				goObject.transform.GetChild(i).gameObject.layer = dropper.layer;
				// IF the goObject GameObject has a Sprite Renderer AND the GameObject that spawned the goObject has a Sprite Renderer as well.
				if(goObject.transform.GetChild(i).GetComponent<SpriteRenderer> () != null && dropper.GetComponent<SpriteRenderer> () != null)
				{
					// Set the sorting layer name to the GameObject that created this Object.
					goObject.transform.GetChild(i).GetComponent<SpriteRenderer> ().sortingLayerName = dropper.GetComponent<SpriteRenderer> ().sortingLayerName;
				}
			}
			// Return the Object.
			return goObject;
		}

//		/// <summary>
//		/// Method that will generate a random 1 word string.  The allowed characters are a-z, A-Z and 0-9.  You can set the length of the string by the int "length".
//		/// </summary>
//		/// <returns>A random 1 word string.</returns>
//		/// <param name="length">Length.</param>
//		/// <param name="allowedChars">Allowed chars.</param>
//		public string GenerateRandomString(int length, string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"){
//			// IF the length is 0.
//			if(length == 0) throw new System.ArgumentOutOfRangeException("length", "Length cannot be less than 0.");
//			// IF the allowedChars is empty or null.
//			if(System.String.IsNullOrEmpty(allowedChars)) throw new System.ArgumentException("allowedChars cannot be empty.");
//			// Create a constant variable for our byteSize (0x100 = 256, 0-255).
//			const int byteSize = 0x100;
//			// Create an array of the HastSets of allowedChars.
//			var allowedCharSet = new HashSet<char>(allowedChars).ToArray();
//			// IF our allowedCharSet length is larger than our byteSyze.
//			if(byteSize < allowedCharSet.Length) throw new ArgumentException(String.Format("allowedChars may contain no more than {0} characters", byteSize));
//
//			// Use a cryptographically-secure random number generator, so now the caller is protected.
//			using (var rngg = new System.Security.Cryptography.RNGCryptoServiceProvider())
//			{
//				// Create a StringBuilder
//				var result = new System.Text.StringBuilder ();
//				var buff = new byte[128];
//				// While the length of the result is less than the length of how long we want our string to be.
//				while(result.Length < length)
//				{
//					// Have our rng get the bytes of our byte[].
//					rngg.GetBytes (buff);
//					// For as long as our i is less than the length of our byte[] AND the result of the length is less than the length of the string we want.
//					for(var i = 0; i < buff.Length && result.Length < length; ++i)
//					{
//						// Divide the byte into allowedCharSet sized groups.
//						var outOfRangeStart = byteSize - (byteSize % allowedCharSet.Length);
//						// IF we have biasing.
//						if (outOfRangeStart <= buff [i])
//							// Go to next iteration.
//							continue;
//						// Add the character to our result.
//						result.Append (allowedCharSet[buff[i] % allowedCharSet.Length]);
//					}
//				}
//				// Return our final string.
//				return result.ToString ();
//			}
//		}

//		/// <summary>
//		/// Generate a set of keys for encrypting and decrypting the data to be harder to manipulate.
//		/// </summary>
//		public void GenerateNewKeys(){
//			// Our security keys for Cryptography security.
//			string publicKey;
//			string publicAndPrivateKey;
//			// This is where the magic happens for pooping out our keys.
//			AsymmetricEncryption.GenerateKeys (1024, out publicKey, out publicAndPrivateKey);
//			// Save these keys.
//			PlayerPrefs.SetString ("PK", publicKey);
//			PlayerPrefs.SetString ("PAPK", publicAndPrivateKey);
//		}

		/// <summary>
		/// Returns a random Vector3 that is at position 'center' and 'radius' distance away.
		/// </summary>
		/// <returns>The circle.</returns>
		/// <param name="center">Center.</param>
		/// <param name="radius">Radius.</param>
		public Vector3 RandomCircle(Vector3 center, float radius){
			float ang = UnityEngine.Random.Range(0, 360);
			Vector3 pos = Vector3.zero;
			pos.x = center.x + radius * Mathf.Cos (ang * Mathf.Deg2Rad);
			pos.y = center.y + radius * Mathf.Sin (ang * Mathf.Deg2Rad);
			pos.z = center.z;
			return pos;
		}

		/// <summary>
		/// Currently in the Demo we save valuable information when we transfer to another scene only as this is to prevent any unwanted hiccups while playing.  
		/// When you enter an area that is where you will start if your game were to crash or if the player wants to stop playing and come back later 
		/// (Remember this is only in the demo, you can have your save setup however you want).
		/// </summary>
		/// <param name="newScene">New scene.</param>
		public void ChangeScene(string newScene, int spawnLocation){
			// Save the player setup.
			SavePlayerSetup();
			// Save any scene based information.
			SaveSceneSetup (newScene, spawnLocation);

			// Change the scene.
			SceneManager.LoadScene (newScene);
		}

		private void SavePlayerSetup(){
			// Save the player data.
			Character_Manager.GetPlayerManager().GetComponent<Player_Manager>().SavePlayer ();
		}

		private void SaveSceneSetup(string newScene, int spawnLocation){
			// Set the scene the player is about to go to.
			Grid.setup.SetSceneStartName(newScene);
			// Set the sceneSpawnLocation.
			Grid.setup.SetSceneSpawnLocation(spawnLocation);
			// Save the Setup.
			Grid.setup.Save();

			// Save the changes to the State Handler.
			Grid.stateManager.Save();
			// Clear the list of our State Handlers.
			Grid.stateManager.ClearList ();
		}

		/// <summary>
		/// Launches a GameObject in the direction based on the x and y min/max.
		/// </summary>
		/// <param name="item">Item.</param>
		/// <param name="xMin">X minimum.</param>
		/// <param name="xMax">X max.</param>
		/// <param name="yMin">Y minimum.</param>
		/// <param name="yMax">Y max.</param>
		public void LaunchItem(GameObject item, float xMin, float xMax, float yMin, float yMax)
		{
			float x = UnityEngine.Random.Range (xMin, xMax);
			float y = UnityEngine.Random.Range (yMin, yMax);
			item.GetComponent<Rigidbody2D> ().AddForce (new Vector2(x, y) * 400f);
		}

		/// <summary>
		/// Launches a GameObject in the opposite direction of 'pos'.
		/// </summary>
		/// <param name="item">Item.</param>
		/// <param name="pos">Position.</param>
		public void LaunchItemAwayFromPosition(GameObject item, Vector3 pos){
			Vector2 normPos = new Vector2 (item.transform.position.x - pos.x, item.transform.position.y - pos.y).normalized;
			item.GetComponent<Rigidbody2D> ().AddForce (normPos * 400f);
		}

		// Make a method for setting parent for objects.
		public void SetParentTransform(Transform parent, GameObject child){
			// IF we actually have a child gameobject and a parent to place it in.
			if(child != null && parent != null){
				// Set its parent to the current child GameObject.
				child.transform.SetParent(parent);
			}
		}

		// Remove all the gameobjects by tags.
		public void DestroyGameObjectsByTags(string[] destroyTags){
			// Loop through all the destroy tags.
			for(int i = 0; i < destroyTags.Length; i++){
				// Grab all GameObjects with the tags for being destroyed.
				GameObject[] objects = GameObject.FindGameObjectsWithTag(destroyTags[i]);
				// Loop through the array.
				for(int j = 0; j < objects.Length; j++){
					// Destroy each object.
					Destroy (objects[j]);
				}
			}
		}

		// Destroy gameobjects by parent.
		public void DestroyGameObjectsByParent(GameObject parent){
			// Get each child.
			foreach(Transform child in parent.transform){
				// Destroy each child.
				Destroy (child.gameObject);
			}
		}

		// Set the gameobjects activity by tags.
		public void SetActiveGameObjectsByTag(string activeTag, bool isActive){
			// Grab all GameObjects with this tag.
			GameObject[] objects = GameObject.FindGameObjectsWithTag (activeTag);
			// Loop through all the GameObjects with this same tag.
			for (int j = 0; j < objects.Length; j++) {
				// Play any sounds when activating or deactivating.
				PlaySoundActiveness (objects [j], isActive);
				// Set the active of each object.
				objects [j].SetActive (isActive);
			}
		}

		// Set the gameobjects activity by tags.
		public void SetActiveGameObjectsByTags(string[] activeTags, bool isActive){
			// Loop through all the active tags.
			for(int i = 0; i < activeTags.Length; i++){
				// Grab all GameObjects with this tag.
				GameObject[] objects = GameObject.FindGameObjectsWithTag(activeTags[i]);
				// Loop through all the GameObjects with this same tag.
				for(int j = 0; j < objects.Length; j++){
					// Play any sounds when activating or deactivating.
					PlaySoundActiveness(objects[j], isActive);
					// Set the active of each object.
					objects[j].SetActive(isActive);
				}
			}
		}

		// Set the gameobjects activity by parent
		public void SetActiveGameObjectsByParent(GameObject parent, bool isActive){
			// Get the amount of children.
			int children = parent.transform.childCount;
			// Loop the amount of times as you have children.
			for(int i = 0; i < children; i++){
				// Play any sounds when activating or deactivating.
				PlaySoundActiveness(parent.transform.GetChild (i).gameObject, isActive);
				// Set the actual activity of this gameobject.
				parent.transform.GetChild (i).gameObject.SetActive (isActive);
			}
		}


		// Set the GameObjects activity.
		public void SetActiveGameObject(GameObject gameObjectToActivate, bool isActive){
			// Play any sounds when activating or deactivating.
			PlaySoundActiveness(gameObjectToActivate, isActive);
			// Set the actual activity of this gameobject.
			gameObjectToActivate.SetActive (isActive);
		}


		// Set the gameobjects activity.
		public void SetActiveGameObjects(GameObject[] gameObjectsToActivate, bool isActive){
			// Loop through all the gameobjects.
			for (int i = 0; i < gameObjectsToActivate.Length; i++) {
				// Play any sounds when activating or deactivating.
				PlaySoundActiveness(gameObjectsToActivate[i], isActive);
				// Set the activity of the gameobject.
				gameObjectsToActivate [i].SetActive (isActive);
			}
		}


		private void PlaySoundActiveness(GameObject gameObjectToActivate, bool isActive){
			// IF we have 1 component of Play_SoundOnActivation.
			if (gameObjectToActivate.GetComponent<Play_SoundOnActivation> () != null) {
				// Grab all the Play Sound components
				Play_SoundOnActivation[] playSound = gameObjectToActivate.GetComponents<Play_SoundOnActivation> ();
				// Loop through all the activate sounds.
				for (int j = 0; j < playSound.Length; j++) {
					// IF we are activating the GameObject,
					// ELSE we are deactivating the GameObject.
					if (isActive) {
						// Play the sound when the gameobject is activated.
						playSound [j].PlayActiveSounds ();
					} else {
						// Play the sound whent he gameobject is deactivated.
						playSound [j].PlayInactiveSounds ();
					}
				}
			}
		}

		/// <summary>
		/// Changes the screen mode to either windowed or fullscreen.
		/// </summary>
		/// <param name="isFullscreen">If set to <c>true</c> is fullscreen.</param>
		public void ChangeScreenMode(bool isFullscreen){
			Screen.fullScreen = isFullscreen;
		}

	//	public void ChangeResolution(int screenWidth, int screenHeight, bool fullscreen){
	//		// Set the resolution to be changed at the end of the execution.
	//		Screen.SetResolution(screenWidth, screenHeight, fullscreen);
	//		// Now adjust the visuals with the new settings.
	//		SetCameraRatios(camWidth, camHeight, screenWidth, screenHeight);
	//	}

		public string RemoveClone(string name){
			// See if there is a (Clone) string in the name.
			int index = name.IndexOf ("(Clone)");
			// IF there is a (Clone) string in the name.
			if(index != -1){
				// Remove part of the string.
				name = name.Remove (index);
				return name;
			}
			return name;
		}

		public void SetCameraRatios(float cameraWidth, float cameraHeight, int screenWidth, int screenHeight){
			// Grab the main camera.
			Camera _camera = Camera.main;
			// Set the desired aspect ratio.
			float targetAspect = cameraWidth / cameraHeight;
			// Determine the game window's current aspect ratio.
			float windowAspect = (float)screenWidth / (float)screenHeight;
			// Current viewport height should be scaled by this amount.
			float scaleHeight = windowAspect / targetAspect;
//			// Set the camera height to the desired height of the cameraHeight.
//			_camera.orthographicSize = cameraHeight/200f;
			
			// IF scaled height is larger than the width,
			// ELSE IF scaled width is larger than the height.
			// ELSE scaled height is the same as scaled width.
			if (scaleHeight < 1.0f)	{
				// Get Camera Viewport Rect
				Rect rect = _camera.rect;
				// Set width.
				rect.width = 1.0f;
				// Set height to scaleHeight.
				rect.height = scaleHeight;
				// Set the x.
				rect.x = 0;
				// Set the y.
				rect.y = (1.0f - scaleHeight) / 2.0f;
				// Apply changes.
				_camera.rect = rect;

			}else if(scaleHeight > 1.0f) {
				// Get the scaled width.
				float scaleWidth = 1.0f / scaleHeight;
				// Get Camera Viewport Rect.
				Rect rect = _camera.rect;
				// Set width.
				rect.width = scaleWidth;
				// Set height.
				rect.height = 1.0f;
				// Set x.
				rect.x = (1.0f - scaleWidth) / 2.0f;
				// Set y.
				rect.y = 0;
				// Apply changes.
				_camera.rect = rect;
			}
		}

		public void FourDirectionAnimation(float moveHorizontal, float moveVertical, Animator anim){
			// IF we are moving we set the animation IsMoving to true,
			// ELSE we are not moving.
			if(moveHorizontal != 0 || moveVertical != 0){
				anim.SetBool("IsMoving", true);
			}else{
				anim.SetBool("IsMoving", false);
				return;
			}

			// We are wanting to go in the positive X direction.
			if(moveHorizontal > 0 && Mathf.Abs(moveVertical) <= Mathf.Abs(moveHorizontal)){
				anim.SetInteger("Direction", 4);
			// We are wanting to move in the negative X direction.
			}else if(moveHorizontal < 0 && Mathf.Abs(moveVertical) <= Mathf.Abs(moveHorizontal)){
				anim.SetInteger("Direction", 2);
			// We are wanting to move in the negative Y direction.
			}else if(moveVertical < 0 && Mathf.Abs(moveVertical) > Mathf.Abs(moveHorizontal)){
				anim.SetInteger("Direction", 3);
			// We are wanting to move in the positive Y direction.
			}else if(moveVertical > 0 && Mathf.Abs(moveVertical) > Mathf.Abs(moveHorizontal)){
				anim.SetInteger("Direction", 1);
			}
		}

		public void FourDirectionAnimation(float moveHorizontal, float moveVertical, bool isMoving, Animator anim){
			// Manually set if this GameObject is moving for animation.
			anim.SetBool("IsMoving", isMoving);
			
			// We are wanting to go in the positive X direction.
			if(moveHorizontal > 0 && Mathf.Abs(moveVertical) <= Mathf.Abs(moveHorizontal)){
				anim.SetInteger("Direction", 4);
				// We are wanting to move in the negative X direction.
			}else if(moveHorizontal < 0 && Mathf.Abs(moveVertical) <= Mathf.Abs(moveHorizontal)){
				anim.SetInteger("Direction", 2);
				// We are wanting to move in the negative Y direction.
			}else if(moveVertical < 0 && Mathf.Abs(moveVertical) > Mathf.Abs(moveHorizontal)){
				anim.SetInteger("Direction", 3);
				// We are wanting to move in the positive Y direction.
			}else if(moveVertical > 0 && Mathf.Abs(moveVertical) > Mathf.Abs(moveHorizontal)){
				anim.SetInteger("Direction", 1);
			}
		}

		public void EightDirectionAnimation(float moveHorizontal, float moveVertical, Animator anim){
			// IF we are moving we set the animation IsMoving to true,
			// ELSE we are not moving.
			if(moveHorizontal != 0 || moveVertical != 0){
				anim.SetBool("IsMoving", true);
			}else{
				anim.SetBool("IsMoving", false);
				return;
			}

			// IF we are going bottom right - Direction 8.
			// ELSE IF we are going bottom left - Direction 7.
			// ELSE IF we are going top left - Direction 6.
			// ELSE IF we are going top right - Direction 5.
			// ELSE IF we are going right - Direction 4.
			// ELSE IF we are going down - Direction 3.
			// ELSE IF we are going left - Direction 2.
			// ELSE IF we are going up - Direction 1.
			if(moveHorizontal > 0 && moveVertical < 0){
				// Set the down right animation.
				anim.SetInteger("Direction", 8);

			}else if(moveHorizontal < 0 && moveVertical < 0){
				// Set the down left animation.
				anim.SetInteger("Direction", 7);

			}else if(moveHorizontal < 0 && moveVertical > 0){
				// Set the up left animation.
				anim.SetInteger("Direction", 6);

			}else if(moveHorizontal > 0 && moveVertical > 0){
				// Set the up right animation.
				anim.SetInteger("Direction", 5);

			}else if(moveHorizontal > 0){
				// Set the right animation.
				anim.SetInteger("Direction", 4);

			}else if(moveVertical < 0){
				// Set the down animation.
				anim.SetInteger("Direction", 3);

			}else if(moveHorizontal < 0){
				// Set the left animation.
				anim.SetInteger("Direction", 2);

			}else if(moveVertical > 0){
				// Set the up animation.
				anim.SetInteger("Direction", 1);
			}
		}

		public void EightDirectionAnimation(float moveHorizontal, float moveVertical, bool isMoving, Animator anim){
			// Manually set if this GameObject is moving for animation.
			anim.SetBool("IsMoving", isMoving);
			
			// IF we are going bottom right - Direction 8.
			// ELSE IF we are going bottom left - Direction 7.
			// ELSE IF we are going top left - Direction 6.
			// ELSE IF we are going top right - Direction 5.
			// ELSE IF we are going right - Direction 4.
			// ELSE IF we are going down - Direction 3.
			// ELSE IF we are going left - Direction 2.
			// ELSE IF we are going up - Direction 1.
			if(moveHorizontal > 0 && moveVertical < 0){
				// Set the down right animation.
				anim.SetInteger("Direction", 8);
				
			}else if(moveHorizontal < 0 && moveVertical < 0){
				// Set the down left animation.
				anim.SetInteger("Direction", 7);
				
			}else if(moveHorizontal < 0 && moveVertical > 0){
				// Set the up left animation.
				anim.SetInteger("Direction", 6);
				
			}else if(moveHorizontal > 0 && moveVertical > 0){
				// Set the up right animation.
				anim.SetInteger("Direction", 5);
				
			}else if(moveHorizontal > 0){
				// Set the right animation.
				anim.SetInteger("Direction", 4);
				
			}else if(moveVertical < 0){
				// Set the down animation.
				anim.SetInteger("Direction", 3);
				
			}else if(moveHorizontal < 0){
				// Set the left animation.
				anim.SetInteger("Direction", 2);
				
			}else if(moveVertical > 0){
				// Set the up animation.
				anim.SetInteger("Direction", 1);
			}
		}

		public void ClearConsole(){
			var logEnt = System.Type.GetType ("UnityEditorInternal.LogEntries, UnityEditor.dll");
			var clearMeth = logEnt.GetMethod ("Clear", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
			clearMeth.Invoke (null, null);
		}

		public void DebugErrorCheck(int choice, System.Type script, GameObject _gameObject){

			switch (choice) {
			// One of the camera scripts was placed on a GameObject that does not have a Camera Component.
			case 0:
				Debug.Log ("The '" + _gameObject.name + "' GameObject does not have a Camera Component attached to it.  " +
				"Remove the '" + script + "' that is attached to your '" + _gameObject.name + " GameObject' and " +
				"attach it to your Camera GameObject. ", _gameObject);
				break;
			// A empty 'Player Tag'.
			case 1:
				Debug.Log ("The 'Player Tag' is currently empty in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " +
				"Put the Tag that is associated with your Player GameObject for the 'Player Tag' in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
				break;
			// Setting the Camera Width on a camera script is less than or equal to 0.
			case 2:
				Debug.Log ("The 'Width' is set less than or equal to 0 in your '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " +
				"Change the 'Width' in your '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject to the desired width of your Camera.", _gameObject);
				break;
			// Setting the Camera Width on a camera script is less than or equal to 0.
			case 3:
				Debug.Log ("The 'Height' is set less than or equal to 0 in your '" + script + "' that is attached on your '" + _gameObject.name + "' GameObject.  " +
				"Change the 'Height' in your '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject to the desired height of your Camera.", _gameObject);
				break;
			// A camera slide speed that is set to 0, so the camera will not Pan if the player touches the edge of the camera.
			case 4:
				Debug.Log ("The 'Camera Slide Speed' is set at 0 in your '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " +
				"This will result in the camera not panning when you touch the edges of the camera.  " +
				"Set the 'Camera Slide Speed' in the '" + script + "' greater than 0 to get a desired effect.  " +
				"If this is intended ignore this message.", _gameObject);
				break;
			// The Bottom Camera Border is not set and is equal to null.
			case 5:
				Debug.Log ("The 'Bottom Camera Border' is not assigned and equal to 'null' in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " +
				"Select a GameObject (Bottom Tile, Collider or Empty GameObject) for your chosen boundary in your scene for 'Bottom Camera Border.'  " +
				"If your ground tile consist of 1 large tile (Same size or bigger than the camera dimensions.) then apply that tile to all Camera Borders.  " +
				"If not having a boundary is intended then ignore this message", _gameObject);
				break;
			// The Top Camera Border is not set and is equal to null.
			case 6:
				Debug.Log ("The 'Top Camera Border' is not assigned and equal to 'null' in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " +
				"Select a GameObject (Top Tile, Collider or Empty GameObject) for your chosen boundary in your scene for 'Top Camera Border.'  " +
				"If your ground tile consist of 1 large tile (Same size or bigger than the camera dimensions.) then apply that tile to all Camera Borders.  " +
				"If not having a boundary is intended then ignore this message", _gameObject);
				break;
			// The Left Camera Border is not set and is equal to null.
			case 7:
				Debug.Log ("The 'Left Camera Border' is not assigned and equal to 'null' in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " +
				"Select a GameObject (Left Tile, Collider or Empty GameObject) for your chosen boundary in your scene for 'Left Camera Border.'  " +
				"If your ground tile consist of 1 large tile (Same size or bigger than the camera dimensions.) then apply that tile to all Camera Borders.  " +
				"If not having a boundary is intended then ignore this message", _gameObject);
				break;
			// The Right Camera Border is not set and is equal to null.
			case 8:
				Debug.Log ("The 'Right Camera Border' is not assigned and equal to 'null' in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " +
				"Select a GameObject (Right Tile, Collider or Empty GameObject) for your chosen boundary in your scene for 'Right Camera Border.'  " +
				"If your ground tile consist of 1 large tile (Same size or bigger than the camera dimensions.) then apply that tile to all Camera Borders." +
				"If not having a boundary is intended then ignore this message", _gameObject);
				break;
			// The Top Camera Border is lower than the Bottom Camera Border.
			case 9:
				Debug.Log ("The 'Top Camera Border' is lower than the 'Bottom Camera Border' in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  ", _gameObject);
				break;
			// The Right Camera Border is lower than the Left Camera Border.
			case 10:
				Debug.Log ("The 'Right Camera Border' is lower than the 'Left Camera Border' in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  ", _gameObject);
				break;


			// A empty 'On Enter Activation Tag'
			case 20:
				Debug.Log ("The 'On Enter Activation Tag' Array is currently empty in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " +
				"Put the Tag(s) that you want to activate this script in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
				break;
			// A empty 'On Exit Activation Tag'
			case 21:
				Debug.Log ("The 'On Exit Activation Tag' Array is currently empty in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " +
				"Put the Tag(s) that you want to activate this script in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
				break;
			// A empty 'On Enter Parent' Array.
			case 22:
				Debug.Log ("The 'On Enter Parents' Array is currently empty in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " +
				"Put the Parent GameObject(s) that you want so the Children can be destroyed in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
				break;
			// A empty 'On Exit Parent' Array.
			case 23:
				Debug.Log ("The 'On Exit Parents' Array is currently empty in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " +
				"Put the Parent GameObject(s) that you want so the Children can be destroyed in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
				break;
			// A empty 'On Enter Destroy Tag' Array.
			case 24:
				Debug.Log ("The 'On Enter Destroy Tag' Array is currently empty in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " +
				"Put the tag of the GameObject(s) that you want so they can be destroyed in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
				break;
			// A empty 'On Exit Destroy Tag' Array.
			case 25:
				Debug.Log ("The 'On Exit Destroy Tag' Array is currently empty in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " +
				"Put the tag of the GameObject(s) that you want so they can be destroyed in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
				break;
			// A empty 'On Enter GameObjects Destroy' Array.
			case 26:
				Debug.Log ("The 'On Enter GameObjects Destroy' Array is currently empty in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " +
				"Put the GameObject(s) you want so they can be manually destroyed in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
				break;
			// A empty 'On Exit GameObjects Destroy' Array.
			case 27:
				Debug.Log ("The 'On Exit GameObjects Destroy' Array is currently empty in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " +
				"Put the GameObject(s) you want so they can be manually destroyed in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
				break;
	//		// Both 'OnEnter' and 'OnExit' are set to false which makes this script do nothing, so notify the user.
	//		case 29:
	//			Debug.Log("Both 'OnEnter' and 'OnExit' are set to false which makes this script do nothing in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " + 
	//			          "Set 'OnEnter' and/or 'OnExit' equal to TRUE for this script to work unless you plan on turning this on manually at a later time then ignore this message in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
	//			break;


			// A empty 'Target Tags'.
			case 30:
				Debug.Log ("The 'Target Tags' is currently empty in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " +
				"Put the Tag that you want to activate the Scene Change on Collision in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
				break;
			// A empty 'New Scene' when transferring, for Scene Change.
			case 31:
				Debug.Log ("The 'New Scene' is currently empty in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " +
				"Put the Scene Name you want to go to in 'New Scene' in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
				break;
			// A null 'New Location' for the Teleport Script.
			case 32:
				Debug.Log ("The 'New Location' is currently 'null' in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " +
				"Assign a Location so GameObject(s) can be teleported in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
				break;
			// A empty 'Destroyable By Tags'.
			case 33:
				Debug.Log ("The 'Destroyable By Tags' is currently empty in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " +
				"Assign the Tags for 'Destroyable By Tags' so that this GameObject can be destroyed in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
				break;
			// The 'HP' was initially set to 0.
			case 34:
				Debug.Log ("The 'HP' is initially set to 0 in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " +
				"Set the 'HP' greater than 0 in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
				break;
			// The 'moveSpeed' is set to 0.
			case 35:
				Debug.Log ("The 'moveSpeed' is initially set to 0 in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " +
				"Set the 'moveSpeed' greater than 0 so the GameObject can move in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
				break;
			// The Four Transform directions are null.
			case 36:
				Debug.Log ("The 'up', 'down', 'left' and 'right' are equal to 'null' in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " +
				"Assign a minimum of 1 side so the GameObject can be moved in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
				break;


			// The initial player GameObject to spawn is null.
			case 40:
				Debug.Log ("The 'Player' is equal to 'null' in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " +
				"Assign your player prefab for the 'Player' GameObject in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
				break;
			// The 'Scene Spawn Location' is initially null.
			case 41:
				Debug.Log ("The 'Scene Spawn Location' is equal to 'null' in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " +
				"Assign Transform locations that you want your player to spawn for the start of the game/scene and/or scene change in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
				break;
			// The 'canMove' is set to 0.
			case 42:
				Debug.Log ("The 'canMove' is initially set to 0 in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " +
				"Assign 'canMoves' value greater than 0 so the GameObject can move, if this was intended ignore this message in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
				break;
			// The 'alterSpeed' is set to 0.
			case 43:
				Debug.Log ("The 'alterSpeed' is initially set to 0 in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " +
				"Assign 'alterSpeed' value greater than 0 so the GameObject can move, if this was intended ignore this message in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
				break;
			// The 'terrainAnimation' is initially null.
			case 44:
				Debug.Log ("The 'terrainAnimation' is equal to 'null' in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " +
				"Set the 'terrainAnimation' to the selected GameObject that you want to display for showing visuals in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
				break;
			// The 'Sound Clip' is null.
			case 46:
				Debug.Log ("Your 'Sound Clip' is equal to 'null' in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " +
				"Set Sound Clip for this variable in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
				break;
			// The 'Sound Clip' is null.
			case 47:
				Debug.Log ("The 'Distance' is equal to 0 which may give unwanted sound results in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " +
				"Set a 'Distance' value greater than 0 in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
				break;


			// The 'joltTags' is initially null and empty.
			case 50:
				Debug.Log ("The 'joltTags' is equal to 'null' in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " +
				"Put the tags that you want associated for 'joltTags' so the script will work in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
				break;
	//		// The 'joltAmount' is equal to 0.
	//		case 51:
	//			Debug.Log("The 'joltAmount' is equal to 0 in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " + 
	//			          "Set the 'joltAmount' greater than 0 so the colliding GameObject will be jolted away in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
	//			break;
			// The time the player is invulnerable after being damaged is set to 0.
			case 52:
				Debug.Log ("The 'Invulnerability Time' is equal to 0 in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " +
				"Set the 'Invulnerability Time' greater than 0 so the GameObject will be immune to any colliding jolts in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
				break;

			// The user wants to see the collider in the scene view but forgot to set a collider.
			case 70:
				Debug.Log ("The 'Range Collider' is 'null' in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " +
				"Set the collider you wish to represent in your scene view to 'Range Collider' in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
				break;
			
			// The user did not set the dialogue box.
			case 71:
				Debug.Log ("The 'Dialogue Box' is 'null' in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " +
				"Set a gameobject that you want to be your dialogue display for 'Dialogue Box' in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
				break;

			// The user did not set any dialogue.
			case 72:
				Debug.Log ("The 'Dialogue' is currently empty in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " +
				"Set an array of strings that you want your Dialogue to display in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
				break;

			// The user did not set the Dialogue Image.
			case 73:
				Debug.Log ("The 'Dialogue Image' is 'null' in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " +
				"Set the Image component that is on your GameObject in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
				break;

			// The user did not set the Dialogue Text.
			case 74:
				Debug.Log ("The 'Dialogue Text' is 'null' in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " +
				"Set the Text component that is found as a child of your Image GameObject in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
				break;

			// The user did not set an Icon.
			case 75:
				Debug.Log ("The 'Icon' is 'null' in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " +
				"Set a GameObject that you wish to be your icon in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
				break;

			// The user did not set any locations in Point To Point.
			case 76:
				Debug.Log ("The 'Locations' array is equal to 0 in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " +
				"Set locations for the GameObject to move towards in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
				break;

	//		// The user user didn't set up the Action Key dialogue properly by not making 
	//		// this script be on a child gameobject of the main NPC gameobject.
	//		case 77:
	//			Debug.Log ("The GameObject you have your Action Key Dialogue script on is not a child of the main NPC GameObject in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " +
	//			"Create a child GameObject for your NPC GameObject and assign this script and the desired Collider2D for that GameObject in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
	//			break;
	//
	//		// The user user didn't set up the Area Key dialogue properly by not making this script be on a child gameobject of the main NPC gameobject.
	//		case 78:
	//			Debug.Log ("The GameObject you have your Area Key Dialogue script on is not a child of the main NPC GameObject in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " +
	//			"Create a child GameObject for your NPC GameObject and assign this script and the desired Collider2D for that GameObject in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
	//			break;

			// The user user didn't place the Dialogue Component on the Dialogue Box GameObject that is in the Area_Dialogue script.
			case 79:
				Debug.Log ("The Dialogue script is not attached to the Dialogue Box GameObject in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " +
					"Attach the Dialogue Script to your Dialogue Box GameObject that is in your Area_Dialogue script in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
				break;

			// There is a button to bring up the options menu but the Options Canvas is not assigned.
			case 100:
				Debug.Log ("You have a button to bring up your options menu but no Options Canvas is assigned in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " +
				"Assign a Canvas so your options menu panel can be displayed in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
				break;

			// There is a button to bring up the options menu but the Options Panel Menu is not assigned.
			case 101:
				Debug.Log ("You have a button to bring up your options menu but no Options Panel Menu is assigned in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " +
				"Assign a Panel to represent your options menu in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
				break;

			default:
				Debug.Log ("Error in Debug Check for number: " + choice + "in " + script, _gameObject);
				break;
			}
		}
	}
}