using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour {

    [HeaderAttribute("Canvases")]
    public CanvasGroup intro;
    public CanvasGroup moa;
    public CanvasGroup modelViewCanvas;
    public CanvasGroup screenViewCanvas;

    [HeaderAttribute("Sound Effects")]
    public AudioClip button;
    public AudioClip arrow_left;
    public AudioClip arrow_right;
	public AudioClip zoom_in;
	public AudioClip zoom_out;
    public AudioSource musicSource;
	public AudioSource sfxSource;

    private List<CanvasGroup> cgList = new List<CanvasGroup>();

    // Use this for initialization
    void Start() {

        cgList.Add(intro);
        cgList.Add(moa);
        cgList.Add(screenViewCanvas);
        cgList.Add(modelViewCanvas);

        OnlyShowTutorialCanvas();

    }

    // Update is called once per frame
    void Update() {

        if (Input.GetKeyDown(KeyCode.R)) {
            Reset();
        }

    }

    public void OnlyShowTutorialCanvas() {

        for (int i = 1; i < cgList.Count; i++) {
            cgList[i].alpha = 0;
            cgList[i].blocksRaycasts = false;
        }

    }

    public void BeginPlaying() {
		
		PlaySound(button);
        StartCoroutine(EnableGameplay());

    }

    IEnumerator EnableGameplay() {

        cgList[0].DOFade(0, 1);
        cgList[0].blocksRaycasts = false;

        yield return new WaitForSeconds(0.8f);
        cgList[1].DOFade(1, 1);
        cgList[1].blocksRaycasts = true;
        cgList[2].DOFade(1, 1);
        cgList[2].blocksRaycasts = true;

    }

    public void Reset() {
        OnlyShowTutorialCanvas();
    }
	
	public void PlaySound(AudioClip _clip) {
		sfxSource.PlayOneShot(_clip);
		EventSystem.current.SetSelectedGameObject(null);
	}

}