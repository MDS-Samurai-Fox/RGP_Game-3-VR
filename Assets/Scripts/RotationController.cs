using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class RotationController : MonoBehaviour {

    [HideInInspector] public GameManager gameManager;

    // Rotation
    [HideInInspector] public Vector3 originalRotation;
    private Vector3 eulerAngles;
    private int endAngle = 0;

    public Ease rotationEaseType = Ease.OutBounce;
    public float rotationDuration = 1;
    private bool canRotate = true;

    // ------------------------------------------------------------------------

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        gameManager = FindObjectOfType<GameManager>();
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start() {
        originalRotation = transform.eulerAngles;
    }

    /// <summary>
    /// Rotates the object 90 degrees horizontally 
    /// </summary>
    /// <param name="shouldRotateRight"> Whether or not the object should rotate to the right </param>
    public void RotateTo(bool shouldRotateRight) {

        if (!canRotate) {
            return;
        } else {
            canRotate = false;
        }

        eulerAngles = transform.transform.eulerAngles;

        if (shouldRotateRight) {
            gameManager.PlaySound(gameManager.arrow_right);
            eulerAngles.y -= 90;
        } else {
            gameManager.PlaySound(gameManager.arrow_left);
            eulerAngles.y += 90;
        }

        endAngle = Mathf.RoundToInt(eulerAngles.y);

        transform.DORotate(eulerAngles, rotationDuration).SetEase(rotationEaseType).OnComplete(ResetRotationState);

    }

    public void RotateTo(Vector3 _eulerAngles) {

        transform.DORotate(_eulerAngles, 1f);

    }

    public void ResetRotationState() {
        canRotate = true;
    }

    public void ResetRotation() {
        transform.DORotate(originalRotation, 0);
    }

    /// <summary>
    /// Shows the respective canvas according to the rotation of the moa (Makes everything look cleaner)
    /// </summary>
    public void ShowCurrentCanvasSide(bool _shouldDelay) {
        
        print("Showing canvas at angle: " + endAngle + " | Delay: " + _shouldDelay);
        
        // Find the moa canvas
        Transform moaCanvas = GameObject.Find("Moa").transform.FindChild("Moa Canvas");

        CanvasGroup front = moaCanvas.FindChild("Side - Front").GetComponent<CanvasGroup>();
        CanvasGroup left = moaCanvas.FindChild("Side - Left").GetComponent<CanvasGroup>();
        CanvasGroup right = moaCanvas.FindChild("Side - Right").GetComponent<CanvasGroup>();
        CanvasGroup back = moaCanvas.FindChild("Side - Back").GetComponent<CanvasGroup>();

        // Fade all the canvases out
        front.DOFade(0, 0.15f); front.blocksRaycasts = false;
        left.DOFade(0, 0.15f); left.blocksRaycasts = false;
        right.DOFade(0, 0.15f); right.blocksRaycasts = false;
        back.DOFade(0, 0.15f); back.blocksRaycasts = false;
        
        // Depending on the angle, fade the current side in
        // Front
        if (endAngle == 0 || endAngle == 360) {
            front.DOFade(1, 0.15f);
            if (_shouldDelay) {
                // front.DOFade(1, 0.25f).SetDelay(0.1f);
            } else {
            }
            front.blocksRaycasts = true;
        }
        // Right
        else if (endAngle == 90) {
            right.DOFade(1, 0.5f).SetDelay(rotationDuration);
            right.blocksRaycasts = true;
            
        }
        // Left
        else if (endAngle == -90) {
            left.DOFade(1, 0.5f).SetDelay(rotationDuration);
            left.blocksRaycasts = true;
        }
        // Back
        else if (endAngle == 180) {
            back.DOFade(1, 0.5f).SetDelay(rotationDuration);
            back.blocksRaycasts = true;
        }

    }

}