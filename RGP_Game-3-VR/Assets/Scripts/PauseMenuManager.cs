using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenuManager : MonoBehaviour {

	Canvas c;
	CanvasGroup cv;

	bool isPaused = false;

	void Awake() {

		c = GetComponentInChildren<Canvas>();
		cv = c.GetComponentInChildren<CanvasGroup>();

	}

	void Update() {

		if (Input.GetButtonDown("Cancel")) {

			TogglePause();

		}

	}

	public void TogglePause() {

		isPaused = !isPaused;

		if (!isPaused) {
			StartCoroutine(UIAnimation.FadeOut(cv, 0.5f));
		}
		else {
			StartCoroutine(UIAnimation.FadeIn(cv, 0.5f));
		}

		EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(null);

	}

	public void QuitToMenu() {

		iTween.MoveBy(c.gameObject, iTween.Hash("y", 1, "time", 1, "easeType", "easeInCubic", "delay", 0.1f));
		Invoke("LoadScene", 1.5f);

	}

	private void LoadScene() {

		print("Quitting to menu");
		SceneManager.LoadScene(2);

	}

}