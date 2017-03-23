using System.Collections;
using System;
using DG.Tweening;
using UnityEngine;

public class ModelController : MonoBehaviour {

    public Transform pivot;
	public float rotationDuration = 1;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

        

    }

    public void RotateTo(bool shouldRotateRight) {
		
		StartCoroutine(PerformRotation(shouldRotateRight));

    }
	
	private IEnumerator PerformRotation(bool shouldRotateRight) {
		
		Vector3 eulerAngles = this.transform.transform.eulerAngles;
		
		if (shouldRotateRight) {
			eulerAngles.y -= 90;
		} else {
			eulerAngles.y += 90;
		}
		
		this.transform.DORotate(eulerAngles, 0.4f);
		
		yield return null;
		// for (float t = 0; t <= 1; t += (Time.deltaTime / rotationDuration)) {
			
		// 	this.transform.RotateAround(pivot.position, Vector3.up, eulerAngles.y * t);
		// 	yield return null;
			
		// }
		
		// this.transform.RotateAround(pivot.position, Vector3.up, eulerAngles.y);
		
		// print("-- Finished Rotation");
		
	}

}