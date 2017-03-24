using DG.Tweening;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	
	public CanvasGroup intro, moa, modelViewCanvas, screenViewCanvas;
	
	private List<CanvasGroup> cgList = new List<CanvasGroup>();

	// Use this for initialization
	void Start () {

		cgList.Add(intro);
		cgList.Add(moa);
		cgList.Add(modelViewCanvas);
		cgList.Add(screenViewCanvas);
		
		HideUI();
		StartCoroutine(PlayIntroAnimation());
	
	}
	
	// Update is called once per frame
	void Update () {
	
		
	
	}
	
	public void HideUI() {
		
		foreach (CanvasGroup cg in cgList) {
			cg.alpha = 0;
			cg.blocksRaycasts = false;
		}
		
	}
	
	IEnumerator PlayIntroAnimation() {
		
		intro.DOFade(1, 1);
		
		yield return null;
		
	}

}