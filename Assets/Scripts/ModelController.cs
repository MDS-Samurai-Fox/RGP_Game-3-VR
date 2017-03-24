using System.Collections;
using System;
using DG.Tweening;
using UnityEngine;

public class ModelController : MonoBehaviour {
	
	public Ease easeType = Ease.OutBounce;
	public float rotationDuration = 1;
	private bool canRotate = true;

    public void RotateTo(bool shouldRotateRight) {
		
		if (!canRotate) {
			return;
		} else {
			canRotate = false;
		}
		
		Vector3 eulerAngles = transform.transform.eulerAngles;
		
		if (shouldRotateRight) {
			eulerAngles.y -= 90;
		} else {
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