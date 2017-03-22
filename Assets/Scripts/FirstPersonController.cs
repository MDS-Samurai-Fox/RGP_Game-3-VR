using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class FirstPersonController : MonoBehaviour {
	
	[Range(30f, 70f)]
	public float fieldOfView = 45f;

	// Movement //
	[Range(4.0f, 8.0f)]
	public float walkSpeed = 6.0f;

	Vector3
	moveAmount,
	smoothMoveVelocity;

	// Camera //
	[Range(3.0f, 6.0f)]
	public float mouseSensitivityX;

	[Range(3.0f, 6.0f)]
	public float mouseSensitivityY;

	// the amount of rotation that should be added to the camera
	private float verticalLookRotation;

	// Object members //
	Rigidbody body;

	// Methods //

	void Awake() {

		body = GetComponent<Rigidbody>();

	}
	
	// Update is called once per frame
	void Update() {
		
		float inputX = Input.GetAxis("Horizontal");
		float inputY = Input.GetAxis("Vertical");

		// First person rotations //
		// This rotates the player by the up vector (y)
		transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseSensitivityX);
		
		// Calculate how much should the camera rotate vertically
		verticalLookRotation += Input.GetAxis("Mouse Y") * mouseSensitivityY;
		verticalLookRotation = Mathf.Clamp(verticalLookRotation, -fieldOfView, fieldOfView);

		Camera.main.transform.localEulerAngles = Vector3.left * verticalLookRotation;

		Vector3 moveDirection = new Vector3(inputX, 0, inputY).normalized;
		Vector3 targetMoveAmount = moveDirection * walkSpeed;

		// Smooth the movement
		moveAmount = targetMoveAmount;
		//moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVelocity, 0.1f);

	}

	void FixedUpdate() {


		// The new move position will be the current one plus the move amount (converted from world space to local space)
		Vector3 localMove = transform.TransformDirection(moveAmount) * Time.deltaTime;
		body.MovePosition(body.position + localMove);

	}

}