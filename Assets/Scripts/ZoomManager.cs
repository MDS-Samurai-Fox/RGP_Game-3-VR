using DG.Tweening;
using UnityEngine;
using TMPro;

public class ZoomManager : MonoBehaviour {

    private GameManager gm;
    [HideInInspector] public RotationController mc;
    
    // Text Fills
    private TextMeshProUGUI[] texts;

    // The main canvases that provide interaction with the game
    // private CanvasGroup moaInteractionCG, modelViewCG, screenViewCG;

    // The positions of the element to store at
    public Vector3 originalPosition, middlePosition, finalPosition;

    // Animation variables
    public Ease ZoomEaseType = Ease.OutSine;
    public float tweenDuration = 1f;
    private Vector3[] originalPath, tweenPath;
    private float fadeDuration = 0.3f;
    private RectTransform objectRT, modelViewRT;

    // ------------------------------------------------------------------------

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        
        gm = FindObjectOfType<GameManager>();
        
        // moaInteractionCG = GameObject.Find("Moa Interaction Canvas").GetComponent<CanvasGroup>();
        // modelViewCG = GameObject.Find("Model View Canvas").GetComponent<CanvasGroup>();
        // screenViewCG = GameObject.Find("Screen View Canvas").GetComponent<CanvasGroup>();
        
        mc = GetComponent<RotationController>();
        texts = GetComponentsInChildren<TextMeshProUGUI>();
    
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start() {

        transform.DOMove(originalPosition, 0);

        originalPath = new Vector3[] {
            middlePosition,
            originalPosition
        };

        tweenPath = new Vector3[] {
            middlePosition,
            finalPosition
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
    /// Resets the position of the object, useful 
    /// </summary>
    public void ResetPosition() {
        if (transform.position != originalPosition) {
            transform.DOMove(originalPosition, 0);
        }
    }

    /// <summary>
    /// Bring this transform in front of the player and hide the other canvases
    /// </summary>
    void ZoomIn() {

        gm.PlaySound(gm.zoom_in);

        // Change the size of the model view canvas to fit the scale of the object
        modelViewRT = gm.modelViewCanvas.GetComponent<RectTransform>();
        modelViewRT.sizeDelta = new Vector2(transform.localScale.x, transform.localScale.y) * 1000;
        modelViewRT.DOAnchorPosY(finalPosition.y, fadeDuration - 0.2f);
        
        // Change the text
        gm.textLeft.text = texts[0].text;
        gm.textRight.text = texts[1].text;

        gm.modelViewCanvas.DOFade(1, fadeDuration * 2).SetDelay(tweenDuration - fadeDuration);
        gm.modelViewCanvas.blocksRaycasts = true;

        gm.moaCanvas.DOFade(0, fadeDuration);
        gm.moaCanvas.blocksRaycasts = false;

        gm.screenViewCanvas.DOFade(0, fadeDuration);
        gm.screenViewCanvas.blocksRaycasts = false;

        transform.DOPath(tweenPath, tweenDuration, PathType.CatmullRom, PathMode.Full3D, 5).SetEase(ZoomEaseType);

    }

    /// <summary>
    /// Bring this transform to its original position and hide the edit canvas of it
    /// </summary>
    void ZoomOut() {

        gm.PlaySound(gm.zoom_out);

        // Reset the rotation back to normal
        transform.GetComponent<RotationController>().RotateTo(mc.originalRotation);

        gm.moaCanvas.DOFade(1, fadeDuration * 2).SetDelay(tweenDuration - fadeDuration * 2);
        gm.moaCanvas.blocksRaycasts = true;

        gm.screenViewCanvas.DOFade(1, fadeDuration * 2).SetDelay(tweenDuration - fadeDuration * 2);
        gm.screenViewCanvas.blocksRaycasts = true;

        gm.modelViewCanvas.DOFade(0, fadeDuration);
        gm.modelViewCanvas.blocksRaycasts = false;

        transform.DOPath(originalPath, tweenDuration, PathType.CatmullRom, PathMode.Full3D, 5).SetEase(ZoomEaseType);

    }

}