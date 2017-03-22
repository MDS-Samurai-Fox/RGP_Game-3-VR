using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogueText : MonoBehaviour {

	/// <summary>
	/// 
	/// </summary>
	public string[] text;

	/// <summary>
	/// 
	/// </summary>
	public Text[] texts;

	private Text textField;
	private int textIndex = 0;

	// Use this for initialization
	void Start() {

		// Initialize a list of texts
		textField = this.GetComponentInChildren<Text>();
		textField.text = texts[0].text;

	}

	public void LoadNextText() {

		if (textIndex + 1 >= text.Length)
			return;

		textIndex++;

		AnimateText();

	}

	public void LoadPreviousText() {

		if (textIndex - 1 < 0)
			return;

		textIndex--;

		AnimateText();

	}

	private void AnimateText() {

		//textField.GetComponent<CanvasGroup>().alpha = 0;
		textField.text = texts[textIndex].text;
		//StartCoroutine(UIAnimation.FadeIn(textField.GetComponentInChildren<CanvasGroup>(), 0.2f));

	}

}