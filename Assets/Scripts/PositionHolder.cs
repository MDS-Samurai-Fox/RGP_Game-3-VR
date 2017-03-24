using DG.Tweening;
using UnityEngine;

public class PositionHolder : MonoBehaviour {

    public CanvasGroup modelCanvasGroup, objectCanvasGroup;
    public Vector3 originalPosition, middlePosition, tweenPosition;
    public Ease easeType = Ease.OutSine;
    public float tweenDuration = 1f;

    private Vector3[] originalPath, tweenPath;
    
    private float fadeDuration = 0.3f;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start() {

        transform.DOMove(originalPosition, 0);
        objectCanvasGroup.alpha = 0;
        objectCanvasGroup.blocksRaycasts = false;

        originalPath = new Vector3[] {
            middlePosition,
            originalPosition
        };

        tweenPath = new Vector3[] {
            middlePosition,
            tweenPosition
        };

    }

    public void ChangePositions() {

        // Change to the tween position
        if (transform.position == originalPosition) {

            transform.DOPath(tweenPath, tweenDuration, PathType.CatmullRom, PathMode.Full3D, 5).SetEase(easeType);
            FadeIn();

        }
        // Change to the original position
        else {

			transform.DOPath(originalPath, tweenDuration, PathType.CatmullRom, PathMode.Full3D, 5).SetEase(easeType);
            FadeOut();

        }

    }
    
    void FadeIn() {
        objectCanvasGroup.DOFade(1, fadeDuration * 2).SetDelay(tweenDuration - fadeDuration * 2);
        objectCanvasGroup.blocksRaycasts = true;
        modelCanvasGroup.DOFade(0, fadeDuration);
        modelCanvasGroup.blocksRaycasts = false;
    }
    
    void FadeOut() {
        modelCanvasGroup.DOFade(1, fadeDuration * 2).SetDelay(tweenDuration - fadeDuration * 2);
        modelCanvasGroup.blocksRaycasts = true;
        objectCanvasGroup.DOFade(0, fadeDuration);
        objectCanvasGroup.blocksRaycasts = false;
    }

}