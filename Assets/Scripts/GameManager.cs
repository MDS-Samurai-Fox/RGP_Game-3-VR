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

    // ------------------------------------------------------------------------

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start() {

        cgList.Add(intro);
        cgList.Add(moa);
        cgList.Add(screenViewCanvas);
        cgList.Add(modelViewCanvas);

        OnlyShowTutorialCanvas();

    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update() {

        if (Input.GetKeyDown(KeyCode.Escape)) {
            Reset();
        }

    }

    /// <summary>
    /// Fades the tutorial canvas in, gets rid of the others
    /// </summary>
    public void OnlyShowTutorialCanvas() {

        for (int i = 0; i < cgList.Count; i++) {
            cgList[i].alpha = 0;
            cgList[i].blocksRaycasts = false;
        }

        cgList[0].DOFade(1, 1);
        cgList[0].blocksRaycasts = true;

    }
    
    /// <summary>
    /// Start the game again
    /// </summary>
    public void BeginPlaying() {

        PlaySound(button);
        StartCoroutine(EnableGameplay());

    }
    
    /// <summary>
    /// Fades canvases out and etc.
    /// </summary>
    /// <returns></returns>
    IEnumerator EnableGameplay() {

        cgList[0].DOFade(0, 1);
        cgList[0].blocksRaycasts = false;

        yield return new WaitForSeconds(0.8f);
        cgList[1].DOFade(1, 1);
        cgList[1].blocksRaycasts = true;
        cgList[2].DOFade(1, 1);
        cgList[2].blocksRaycasts = true;

    }
    
    /// <summary>
    /// Resets the game
    /// </summary>
    public void Reset() {
        
        // Find all the existing zoom managers
        ZoomManager[] phList = FindObjectsOfType<ZoomManager>();
        
        // Reset the moa
        print(moa.transform.parent);
        moa.transform.parent.GetComponent<ModelController>().ResetRotation();
        
        // Reset the body parts
        foreach(ZoomManager ph in phList) {
            ph.mc.ResetRotation();
            ph.ResetPosition();
        }
        
        OnlyShowTutorialCanvas();
    
    }

    public void PlaySound(AudioClip _clip) {
        sfxSource.PlayOneShot(_clip);
        EventSystem.current.SetSelectedGameObject(null);
    }

}