using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CanvasGroup))]
public class AppearAfterSeconds : MonoBehaviour {

	/// <summary>
	/// The seconds after which the object will appear
	/// </summary>
	public float SecondsToWait = 1f;

	/// <summary>
	/// Whether or not the object should fade upwards
	/// </summary>
	public bool shouldFadeUpwards = true;

	/// <summary>
	/// The amount to move the object by
	/// </summary>
	public float moveAmount = 0.2f;

	private CanvasGroup cv;

	void Awake() {

		cv = GetComponent<CanvasGroup>();

	}

	// Use this for initialization
	void Start () {

		cv.alpha = 0;

		Invoke("FadeIn", SecondsToWait);
	
	}

	void FadeIn() {

		if (!shouldFadeUpwards)
			moveAmount *= -1;

		StartCoroutine(UIAnimation.FadeIn(cv, 1));
		iTween.MoveBy(cv.gameObject, iTween.Hash("y", moveAmount, "easeType", "easeInOutCubic"));

	}

}