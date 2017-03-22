using UnityEngine;

[RequireComponent (typeof(CanvasGroup))]
public class FadeOutAfterStart : MonoBehaviour {

	/// <summary>
	/// The time to fade out
	/// </summary>
	public float fadeOutTime = 1f;

	void Start() {

		GetComponent<CanvasGroup>().alpha = 1;
		Invoke("FadeOut", fadeOutTime);

	}

	void FadeOut() {

		StartCoroutine(UIAnimation.FadeOut(this.GetComponent<CanvasGroup>(), 1));

	}

}