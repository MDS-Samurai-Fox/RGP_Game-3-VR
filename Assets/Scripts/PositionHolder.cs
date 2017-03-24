using DG.Tweening;
using UnityEngine;

public class PositionHolder : MonoBehaviour {
    
    // The main canvases that provide interaction with the game
    private CanvasGroup moaInteractionCG, modelViewCG, screenViewCG;
    
    // The positions of the element to store at
    public Vector3 originalPosition, middlePosition, tweenPosition;
    
    // The original rotation of the element, stored via script
    private Vector3 originalRotation;

    // Animation variables
    public Ease easeType = Ease.OutSine;
    public float tweenDuration = 1f;
    private Vector3[] originalPath, tweenPath;
    private float fadeDuration = 0.3f;
    private RectTransform objectRT, modelViewRT;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        moaInteractionCG = GameObject.Find("Moa Interaction Canvas").GetComponent<CanvasGroup>();
        modelViewCG = GameObject.Find("Model View Canvas").GetComponent<CanvasGroup>();
        screenViewCG = GameObject.Find("Screen View Canvas").GetComponent<CanvasGroup>();
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start() {

        originalRotation = transform.eulerAngles;

        // transform.DOMove(originalPosition, 0);
        // modelViewCG.alpha = 0;
        // modelViewCG.blocksRaycasts = false;

        originalPath = new Vector3[] {
            middlePosition,
            originalPosition
        };

        tweenPath = new Vector3[] {
            middlePosition,
            tweenPosition
        };

    }
    
    /// <summary>
    /// Toggles the change between positions, called from whithin the buttons
    /// </summary>
    public void ChangePositions() {

        // Change to the tween position
        if (transform.position == originalPosition) {

            ZoomIn();

        }
        // Change to the original position
        else {

            ZoomOut();

        }

    }

    /// <summary>
    /// Bring this transform in front of the player and hide the other canvases
    /// </summary>
    void ZoomIn() {

        // Change the size of the model view canvas to fit the scale of the object
        modelViewRT = modelViewCG.GetComponent<RectTransform>();
        modelViewRT.sizeDelta = new Vector2(transform.localScale.x, transform.localScale.y) * 1000;
        modelViewRT.DOAnchorPosY(tweenPosition.y, fadeDuration - 0.2f);

        modelViewCG.DOFade(1, fadeDuration * 2).SetDelay(tweenDuration - fadeDuration * 2);
        modelViewCG.blocksRaycasts = true;
        
        moaInteractionCG.DOFade(0, fadeDuration);
        moaInteractionCG.blocksRaycasts = false;
        
        screenViewCG.DOFade(0, fadeDuration);
        screenViewCG.blocksRaycasts = false;

        transform.DOPath(tweenPath, tweenDuration, PathType.CatmullRom, PathMode.Full3D, 5).SetEase(easeType);

    }

    /// <summary>
    /// Bring this transform to its original position and hide the edit canvas of it
    /// </summary>
    void ZoomOut() {

        // Reset the rotation back to normal
        transform.GetComponent<ModelController>().RotateTo(originalRotation);

        moaInteractionCG.DOFade(1, fadeDuration * 2).SetDelay(tweenDuration - fadeDuration * 2);
        moaInteractionCG.blocksRaycasts = true;
        
        screenViewCG.DOFade(1, fadeDuration * 2).SetDelay(tweenDuration - fadeDuration * 2);
        screenViewCG.blocksRaycasts = true;
        
        modelViewCG.DOFade(0, fadeDuration);
        modelViewCG.blocksRaycasts = false;

        transform.DOPath(originalPath, tweenDuration, PathType.CatmullRom, PathMode.Full3D, 5).SetEase(easeType);

    }

}