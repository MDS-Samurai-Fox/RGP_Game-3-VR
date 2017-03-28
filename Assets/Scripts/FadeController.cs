using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof (CanvasGroup))]
public class FadeController : MonoBehaviour {

    public bool fadeIn = true;
    public float delay = 1f;

    private CanvasGroup cg;

    // ------------------------------------------------------------------------

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {

        cg = GetComponent<CanvasGroup> ();

    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start() {

        Fade();

    }

    public void Fade() {

        StartCoroutine(PlayFade());

    }

    IEnumerator PlayFade() {

        if (fadeIn) {
            cg.alpha = 0;
            cg.blocksRaycasts = false;
        } else {
            cg.alpha = 0;
            cg.blocksRaycasts = false;
        }

        yield return new WaitForSeconds(delay);

        cg.DOFade(Convert.ToInt16(fadeIn), 1);

        cg.blocksRaycasts = fadeIn;

    }
    
    /// <summary>
    /// 
    /// </summary>
    public void FadeIn(float _fadeDuration) {
        
        cg.DOFade(1, _fadeDuration);
        StartCoroutine(RaycastBlockAssign(_fadeDuration, true));
        
    }
    
    /// <summary>
    /// 
    /// </summary>
    public void FadeOut(float _fadeDuration) {
        
        cg.DOFade(0, _fadeDuration);
        StartCoroutine(RaycastBlockAssign(0, false));
        
    }
    
    IEnumerator RaycastBlockAssign(float _delay, bool _shouldBlockRaycast) {
        
        yield return new WaitForSeconds(_delay);
        
        cg.blocksRaycasts = _shouldBlockRaycast;
        
    }

}