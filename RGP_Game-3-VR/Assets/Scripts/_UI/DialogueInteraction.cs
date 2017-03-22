using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// Shows the dialogue boxes
/// </summary>
public class DialogueInteraction : MonoBehaviour {

	/// <summary>
	/// The tag to check trigger collision with
	/// </summary>
	public string triggerTag;

	/// <summary>
	/// The key that the player will interact with
	/// </summary>
	public KeyCode interactionKey = KeyCode.Space;

	/// <summary>
	/// The time in which the interaction prompt will fade in/out
	/// </summary>
	public float dialoguePromptFadeTime = 1f;

	// Animation values
	[Range(0.1f, 1f)]
	public float dialogueTranslateValue = 0.2f;

	// others
	private bool canInteractWithDialogue = false;
	private bool hasInteractedWithDialogue = false;
	private CanvasGroup promptCanvasGroup = null;
	private List<CanvasGroup> containers;

	void Start() {

		containers = new List<CanvasGroup>();

	}

	void Update() {

		if (canInteractWithDialogue) {

			// If the user presses E
			if (Input.GetKeyDown(interactionKey)) {

				// Make the dialogue box visible	
				if (!hasInteractedWithDialogue) {

					iTween.MoveBy(promptCanvasGroup.gameObject, iTween.Hash("y", -0.4f, "easeType", "easeInOutExpo", "loopType", "none", "delay", 0.1f));

					foreach (CanvasGroup child in containers) {

						StartCoroutine(UIAnimation.FadeIn(child, 0.5f));
						iTween.MoveBy(child.gameObject, iTween.Hash("y", dialogueTranslateValue, "easeType", "easeInOutExpo", "loopType", "none", "delay", 0.1f));

					}

					hasInteractedWithDialogue = true;

				}
				// Make the dialogue box invisible
				else {

					iTween.MoveBy(promptCanvasGroup.gameObject, iTween.Hash("y", 0.4f, "easeType", "easeInOutExpo", "loopType", "none", "delay", 0.1f));

					foreach (CanvasGroup child in containers) {
						
						Invoke("FadeOutWithTime", 0.5f);
						iTween.MoveBy(child.gameObject, iTween.Hash("y", -dialogueTranslateValue, "easeType", "easeInOutExpo", "loopType", "none", "delay", 0));

					}

					hasInteractedWithDialogue = false;

				}

			} // !Input.GetKeyDown(KeyCode.E)

		} // !canInteractWithDialogue

	}
		
	/// <summary>
	/// This method is called everytime the entity enters a collider that is set to work as a trigger
	/// </summary>
	/// <param name="other"> the trigger that caused the collision</param>
	void OnTriggerEnter(Collider other) {

		if (other.tag == triggerTag) {

			// Access the canvas group object of the current object
			CanvasGroup[] cvGroups = other.GetComponentsInChildren<CanvasGroup>();

			// The first canvas group will be the prompt text
			promptCanvasGroup = cvGroups[0];

			// The remaining objects are the containers
			for (int i = 1; i < cvGroups.Length; i++) {
				containers.Add(cvGroups[i]);
			}

			StartCoroutine(UIAnimation.FadeIn(promptCanvasGroup, dialoguePromptFadeTime * 3));

			foreach (CanvasGroup child in containers) {

				if (child.alpha == 1)
					hasInteractedWithDialogue = true;
				else
					hasInteractedWithDialogue = false;

			}

			canInteractWithDialogue = true;

		}

	}


	/// <summary>
	/// // This method is called everytime the entity exits a collider that is set to work as a trigger
	/// </summary>
	/// <param name="other"> the trigger that caused the collision</param>
	void OnTriggerExit(Collider other) {

		if (other.tag == triggerTag) {

			// Reset the variable states
			StartCoroutine(UIAnimation.FadeOut(promptCanvasGroup, dialoguePromptFadeTime));
			Invoke("ExitTrigger", 1f);

		}

	}

	/// <summary>
	/// To be called using the Invoke function
	/// </summary>
	private void FadeOutWithTime() {

		foreach (CanvasGroup child in containers) {

			StartCoroutine(UIAnimation.FadeOut(child, 0.5f));

		}

	}

	/// <summary>
	/// Clear the list of containers and disable interaction upon calling this method
	/// </summary>
	private void ExitTrigger() {

		containers.Clear();
		hasInteractedWithDialogue = false;
		canInteractWithDialogue = false;

	}

}