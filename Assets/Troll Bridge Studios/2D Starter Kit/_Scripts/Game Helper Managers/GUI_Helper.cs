using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace TrollBridge {

	public static class GUI_Helper {
		
		/// <summary>
		/// Rotates the sprite based on the time rotateTime on the z-axis.
		/// </summary>
		/// <returns>The sprite.</returns>
		/// <param name="_transform">Transform.</param>
		/// <param name="rotateTime">Rotate time.</param>
		/// <param name="rotateFrom">Rotate from.</param>
		/// <param name="rotateTo">Rotate to.</param>
		public static IEnumerator RotateSprite(Transform _transform, float rotateTime, float rotateFrom, float rotateTo){
			// IF we have a null Transform.
			if(_transform == null){
				yield break;;
			}
			// Loop in a lerp manner to get a smooth rotation.
			for(float x = 0.0f; x < 1.0f; x += Time.deltaTime / rotateTime){
				// IF the transform is destroyed before finishing rotation.
				if(_transform == null){
					yield break;
				}
				_transform.eulerAngles = new Vector3 (_transform.eulerAngles.x, _transform.eulerAngles.y, Mathf.SmoothStep(rotateFrom, rotateTo, x));
				yield return null;
			}
		}

		// Fading for images.
		public static IEnumerator FadeImage(Image image, float fadeTime, float from, float to){
			// IF we have a null image.
			if(image == null){
				yield break;
			}
			// Get the images color.
			Color col = image.color;
			// Loop in a SmoothStep manner to get a smooth fade.
			for(float x = 0.0f; x < 1.0f; x += Time.deltaTime / fadeTime){
				// IF the Image is destroyed before it is finished fading.
				if(image == null){
					yield break;
				}
				// Smooth the alpha.
				col.a = Mathf.SmoothStep(from, to, x);
				// Set the color.
				image.color = col;
				yield return null;
			}
			Color endColor = image.color;
			endColor.a = Mathf.SmoothStep(from, to, 1f);
			image.color = endColor;
		}

		// Fading for text.
		public static IEnumerator FadeText(Text txt, float fadeTime, float from, float to){
			// IF we have a null text.
			if(txt == null){
				yield break;
			}
			// Get the text color.
			Color col = txt.color;
			// Loop through a fadeTime interval for fading text.
			for(float x = 0.0f; x < 1.0f; x += Time.deltaTime / fadeTime){
				// IF the Image is destroyed before it is finished fading.
				if(txt == null){
					yield break;
				}
				// Smooth the alpha
				col.a = Mathf.SmoothStep(from, to, x);
				// Set the color.
				txt.color = col;
				yield return null;
			}
			Color endColor = txt.color;
			endColor.a = Mathf.SmoothStep(from, to, 1f);
			txt.color = endColor;
		}

		// Grow/Shrink for images.
		public static IEnumerator GrowShrinkImage(Image image, float resizeTime, float fromX, float fromY, float toX, float toY){
			// IF we have a null image.
			if(image == null){
				yield break;
			}
			// Loop through a resizeTime interval for growing and shrinking images.
			for(float x = 0.0f; x < 1.0f; x += Time.deltaTime / resizeTime){
				// IF the image is destroyed before it is finished resizing.
				if(image == null){
					yield break;
				}
				// Smooth the sizing.
				image.rectTransform.localScale = new Vector2(Mathf.SmoothStep(fromX, toX, x), Mathf.SmoothStep(fromY, toY, x));
				yield return null;
			}
			// Make sure we reach the our destination by manually setting the end point incase the loop didnt finish on x = 1f.
			image.rectTransform.localScale = new Vector3(Mathf.SmoothStep(fromX, toX, 1f), Mathf.SmoothStep(fromY, toY, 1f), 1f);
		}

		// Make the text type out based on the text speed.
		public static IEnumerator TypeText(Text txt, float pauseTime, string dialogue, AudioClip typeSound){
			// IF we have a null text.
			if(txt == null){
				yield break;
			}

			txt.text = "";
			for(int i = 0; i <= dialogue.Length; i++){
				txt.text = dialogue.Substring(0, i);
				Grid.soundManager.PlaySound(typeSound, Character_Manager.GetPlayer().transform.position, 1f, 1f);
				yield return new WaitForSeconds(pauseTime);
				yield return null;
			}
		}
	}
}
