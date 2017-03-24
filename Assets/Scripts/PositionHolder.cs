using DG.Tweening;
using UnityEngine;

public class PositionHolder : MonoBehaviour {

    private CanvasGroup moaInteractionCG, modelViewCG;

    public Vector3 originalPosition, middlePosition, tweenPosition;

    // Animation
    public Ease easeType = Ease.OutSine;
    public float tweenDuration = 1f;
    private Vector3[] originalPath, tweenPath;
    private float fadeDuration = 0.3f;
    private RectTransform objectRT, modelViewRT;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        modelViewCG = GameObject.Find("Model View Canvas").GetComponent<CanvasGroup>();
        moaInteractionCG = GameObject.Find("Moa Interaction Canvas").GetComponent<CanvasGroup>();
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start() {

        transform.DOMove(originalPosition, 0);
        modelViewCG.alpha = 0;
        modelViewCG.blocksRaycasts = false;

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

            FadeIn();
            transform.DOPath(tweenPath, tweenDuration, PathType.CatmullRom, PathMode.Full3D, 5).SetEase(easeType);

        }
        // Change to the original position
        else {

            transform.DOPath(originalPath, tweenDuration, PathType.CatmullRom, PathMode.Full3D, 5).SetEase(easeType);
            FadeOut();

        }

    }

    void FadeIn() {
        
        // Change the size of the model view canvas to fit the scale of the object
        modelViewRT = modelViewCG.GetComponent<RectTransform>();
        modelViewRT.sizeDelta = new Vector2(transform.localScale.x, transform.localScale.y) * 1000;
        modelViewRT.DOAnchorPosY(tweenPosition.y, fadeDuration - 0.2f);
        
        modelViewCG.DOFade(1, fadeDuration * 2).SetDelay(tweenDuration - fadeDuration * 2);
        modelViewCG.blocksRaycasts = true;
        moaInteractionCG.DOFade(0, fadeDuration);
        moaInteractionCG.blocksRaycasts = false;
        
    }

    void FadeOut() {
        
        moaInteractionCG.DOFade(1, fadeDuration * 2).SetDelay(tweenDuration - fadeDuration * 2);
        moaInteractionCG.blocksRaycasts = true;
        modelViewCG.DOFade(0, fadeDuration);
        modelViewCG.blocksRaycasts = false;
        
    }

}