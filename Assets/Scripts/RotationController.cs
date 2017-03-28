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
        float fadeTime = 0.15f;
        if (!_shouldDelay) 
            fadeTime = 0;
        
        front.DOFade(0, fadeTime); front.blocksRaycasts = false;
        left.DOFade(0, fadeTime); left.blocksRaycasts = false;
        right.DOFade(0, fadeTime); right.blocksRaycasts = false;
        back.DOFade(0, fadeTime); back.blocksRaycasts = false;
        
        StartCoroutine(FadeCanvases(rotationDuration - 1, front, left, right, back));

    }
    
    IEnumerator FadeCanvases(float _waitTime, CanvasGroup front, CanvasGroup left, CanvasGroup right, CanvasGroup back) {
        
        print("-- Fading the canvases in " + _waitTime);
        
        yield return new WaitForSeconds(_waitTime);
        
        float fadeTime = 0.5f;
        
        // Depending on the angle, fade the current side in
        // Front
        if (endAngle == 0 || endAngle == 360) {
            print("-- Front");
            front.DOFade(1, fadeTime);
            yield return new WaitForSeconds(fadeTime);
            front.blocksRaycasts = true;
        }
        // Right
        else if (endAngle == 90) {
            print("-- Right");
            right.DOFade(1, fadeTime);
            yield return new WaitForSeconds(fadeTime);
            right.blocksRaycasts = true;
            
        }
        // Left
        else if (endAngle == -90) {
            print("-- Left");
            left.DOFade(1, fadeTime);
            yield return new WaitForSeconds(fadeTime);
            left.blocksRaycasts = true;
        }
        // Back
        else if (endAngle == 180) {
            print("-- Back");
            back.DOFade(1, fadeTime);
            yield return new WaitForSeconds(fadeTime);
            back.blocksRaycasts = true;
        }
        
    }

}