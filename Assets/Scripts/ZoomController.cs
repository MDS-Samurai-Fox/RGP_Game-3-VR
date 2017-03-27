using DG.Tweening;
using TMPro;
using UnityEngine;

[RequireComponent(typeof (RotationController))]
public class ZoomController : MonoBehaviour {

    [HideInInspector] public RotationController rotationController;
    private GameManager gameManager;

    // Text Fills
    private TextMeshProUGUI[] texts;

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

        rotationController = GetComponent<RotationController> ();
        texts = GetComponentsInChildren<TextMeshProUGUI> ();

    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start() {

        gameManager = rotationController.gameManager;

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

        gameManager.PlaySound(gameManager.zoom_in);

        // Change the size of the model view canvas to fit the scale of the object
        modelViewRT = gameManager.modelViewCanvas.GetComponent<RectTransform> ();
        modelViewRT.sizeDelta = new Vector2(transform.localScale.x, transform.localScale.y) * 1000;
        modelViewRT.DOAnchorPosY(finalPosition.y, fadeDuration - 0.2f);

        // Change the text
        gameManager.textLeft.text = texts[0].text;
        gameManager.textRight.text = texts[1].text;

        gameManager.modelViewCanvas.DOFade(1, fadeDuration * 2).SetDelay(tweenDuration - fadeDuration);
        gameManager.modelViewCanvas.blocksRaycasts = true;

        gameManager.moaCanvas.DOFade(0, fadeDuration);
        gameManager.moaCanvas.blocksRaycasts = false;

        gameManager.screenViewCanvas.DOFade(0, fadeDuration);
        gameManager.screenViewCanvas.blocksRaycasts = false;

        transform.DOPath(tweenPath, tweenDuration, PathType.CatmullRom, PathMode.Full3D, 5).SetEase(ZoomEaseType);

    }

    /// <summary>
    /// Bring this transform to its original position and hide the edit canvas of it
    /// </summary>
    void ZoomOut() {

        gameManager.PlaySound(gameManager.zoom_out);

        // Reset the rotation back to normal
        transform.GetComponent<RotationController> ().RotateTo(rotationController.originalRotation);

        gameManager.moaCanvas.DOFade(1, fadeDuration * 2).SetDelay(tweenDuration - fadeDuration * 2);
        gameManager.moaCanvas.blocksRaycasts = true;

        gameManager.screenViewCanvas.DOFade(1, fadeDuration * 2).SetDelay(tweenDuration - fadeDuration * 2);
        gameManager.screenViewCanvas.blocksRaycasts = true;

        gameManager.modelViewCanvas.DOFade(0, fadeDuration);
        gameManager.modelViewCanvas.blocksRaycasts = false;

        transform.DOPath(originalPath, tweenDuration, PathType.CatmullRom, PathMode.Full3D, 5).SetEase(ZoomEaseType);

    }

}