using System.Collections;
using System;
using DG.Tweening;
using UnityEngine;

public class ModelController : MonoBehaviour {

    public Transform pivot;
	public float rotationDuration = 1;

    public void RotateTo(bool shouldRotateRight) {
		
		Vector3 eulerAngles = this.transform.transform.eulerAngles;
		
		if (shouldRotateRight) {
			eulerAngles.y -= 90;
		} else {
			eulerAngles.y += 90;
		}
		
		this.transform.DORotate(eulerAngles, 0.4f);

    }

}