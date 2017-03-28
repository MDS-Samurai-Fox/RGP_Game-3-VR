using System.Collections;
using DG.Tweening;
using UnityEngine;
using TMPro;
// using UnityEngine.UI;

public class CameraZoomController : MonoBehaviour {
	
	public GameManager gameManager;
    
    [HeaderAttribute("Fade")]
    public FadeController fadeImage;

    [HeaderAttribute("Animation")]
    public Ease zoomEaseType = Ease.OutSine;
    public float zoomDuration = 1f;
	public float fadeDuration = 0.3f;

    
    private Vector3 originalPosition;
    private bool isTweening = false;
	
	

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    /// <summary>
    /// Moves the camera towards the chosen body part
    /// </summary>
    /// <param name="_bodypart"> The transform to move towards </param>
    public void ZoomTowards(Transform _bodypart) {

        if (isTweening)
            return;
			
        StartCoroutine(ZoomIn(_bodypart));

    }
	
	IEnumerator ZoomIn(Transform _bodypart) {
		
		isTweening = true;
        
        fadeImage.FadeIn(0.5f);
        
        yield return new WaitForSeconds(0.5f);
        
        zoomDuration = 0;
        
		// Move the camera towards the body part
        transform.DOMove(_bodypart.position + _bodypart.GetComponent<TweenOffset>().zoomPosition, zoomDuration).SetEase(zoomEaseType);
		
		//-- Code from the zoom controller
		gameManager.PlaySound(gameManager.zoom_in);
		 
		// Change the text
        gameManager.textLeft.text = _bodypart.FindChild("Text L").GetComponent<TextMeshProUGUI>().text;
        gameManager.textRight.text = _bodypart.FindChild("Text R").GetComponent<TextMeshProUGUI>().text;
        
        // Show the model view canvas
        gameManager.modelViewCanvas.DOFade(1, fadeDuration * 2).SetDelay(zoomDuration - fadeDuration);
        gameManager.modelViewCanvas.blocksRaycasts = true;
		
		gameManager.modelViewCanvas.GetComponent<RectTransform>().DOMove(_bodypart.position + _bodypart.GetComponent<TweenOffset>().canvasPosition, 0);
        
        // Hide the moa and screen view canvas
        gameManager.moaCanvas.DOFade(0, fadeDuration);
        gameManager.moaCanvas.blocksRaycasts = false;

        gameManager.screenViewCanvas.DOFade(0, fadeDuration);
        gameManager.screenViewCanvas.blocksRaycasts = false;
		
		// --
		
		yield return new WaitForSeconds(zoomDuration);
        
        fadeImage.FadeOut(0.5f);
		
		isTweening = false;
		
	}
	
	/// <summary>
	/// Resets the camera to be at the original position
	/// </summary>
	public void ResetZoom() {
		
		if (isTweening)
            return;
			
        StartCoroutine(ZoomOut());
		
	}
	
	IEnumerator ZoomOut() {
		
		isTweening = true;
		
		transform.DOMove(originalPosition, zoomDuration).SetEase(zoomEaseType);
		
		gameManager.moaCanvas.DOFade(1, zoomDuration * 0.5f).SetDelay(zoomDuration * 0.5f);
		
		//-- Zoom Controller
		 gameManager.PlaySound(gameManager.zoom_out);

        // Reset the rotation back to normal
        // transform.GetComponent<RotationController> ().RotateTo(rotationController.originalRotation);

        gameManager.moaCanvas.DOFade(1, fadeDuration * 2).SetDelay(fadeDuration - fadeDuration * 2);
        gameManager.moaCanvas.blocksRaycasts = true;

        gameManager.screenViewCanvas.DOFade(1, fadeDuration * 2).SetDelay(fadeDuration - fadeDuration * 2);
        gameManager.screenViewCanvas.blocksRaycasts = true;

        gameManager.modelViewCanvas.DOFade(0, fadeDuration);
        gameManager.modelViewCanvas.blocksRaycasts = false;
		
		yield return new WaitForSeconds(zoomDuration);
		
		gameManager.moaCanvas.blocksRaycasts = true;
		isTweening = false;
		
	}
	
	public void ResetPosition() {
		
		transform.DOMove(originalPosition, zoomDuration).SetEase(zoomEaseType);
		
	}

}