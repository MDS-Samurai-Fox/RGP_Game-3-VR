using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIAnimation : MonoBehaviour {

	/// <summary>
	/// Fades the canvas group in and make it able to be raycasted
	/// </summary>
	public static IEnumerator FadeIn(CanvasGroup canvas, float fadeTime) {

		canvas.alpha = 0;

		for (float t = 0.0f; t < 1.0f; t += (Time.deltaTime / fadeTime)) {

			canvas.alpha = t;
			yield return null;

		}

		canvas.alpha = 1;
		canvas.blocksRaycasts = true;

	}

	/// <summary>
	/// Fades the canvas group out and make it unable to be raycasted
	/// </summary>
	public static IEnumerator FadeOut(CanvasGroup canvas, float fadeTime) {

		//yield return new WaitForSeconds(fadeTime * 2);
		canvas.blocksRaycasts = false;

		for (float t = 1.0f; t > 0.0f; t -= (Time.deltaTime / fadeTime)) {

			canvas.alpha = t;
			yield return null;

		}

		canvas.alpha = 0;

	}

	/// <summary>
	/// Shows the text in a typewriter style
	/// </summary>
	public static IEnumerator TextIn(Text dialogueBox, string strText, float textDelay) {

		for (int i = 0; i <= strText.Length; i++) {

			dialogueBox.text = strText.Substring(0, i);
			yield return new WaitForSeconds(textDelay);

		}

	}

	/// <summary>
	/// Removes the text in a typewriter style
	/// </summary>
	public static IEnumerator TextOut(Text dialogueBox, string strText, float textDelay) {

		for (int i = strText.Length; i >= 0; i--) {

			dialogueBox.text = strText.Substring(0, i);
			yield return new WaitForSeconds(textDelay);

		}

	}

}