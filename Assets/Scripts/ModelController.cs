using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class ModelController : MonoBehaviour {

    private GameManager gm;

    public Ease easeType = Ease.OutBounce;
    public float rotationDuration = 1;
    private bool canRotate = true;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        gm = FindObjectOfType<GameManager>();
    }

    // Called from a button
    public void RotateTo(bool shouldRotateRight) {

        if (!canRotate) {
            return;
        } else {
            canRotate = false;
        }

        Vector3 eulerAngles = transform.transform.eulerAngles;

        if (shouldRotateRight) {
			gm.PlaySound(gm.arrow_right);
            eulerAngles.y -= 90;
        } else {
			gm.PlaySound(gm.arrow_left);
            eulerAngles.y += 90;
        }

        transform.DORotate(eulerAngles, rotationDuration).SetEase(easeType).OnComplete(ResetRotationState);

    }

    public void RotateTo(Vector3 _eulerAngles) {

        transform.DORotate(_eulerAngles, 1f);

    }

    void ResetRotationState() {
        canRotate = true;
    }

}