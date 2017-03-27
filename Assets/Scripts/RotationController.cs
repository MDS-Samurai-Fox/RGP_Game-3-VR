using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class RotationController : MonoBehaviour {

    [HideInInspector] public GameManager gameManager;

    // Rotation
    [HideInInspector] public Vector3 originalRotation;
    public Ease rotationEaseType = Ease.OutBounce;
    public float rotationDuration = 1;
    private bool canRotate = true;

    // ------------------------------------------------------------------------

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        gameManager = FindObjectOfType<GameManager> ();
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

        Vector3 eulerAngles = transform.transform.eulerAngles;

        if (shouldRotateRight) {
            gameManager.PlaySound(gameManager.arrow_right);
            eulerAngles.y -= 90;
        } else {
            gameManager.PlaySound(gameManager.arrow_left);
            eulerAngles.y += 90;
        }

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

}