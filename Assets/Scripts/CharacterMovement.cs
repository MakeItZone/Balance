using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
	[Tooltip("Player movement speed")]
	public float moveSpeed = 32f;

	[Tooltip("Airborne player movement speed")]
	public float airMoveSpeed = 0.25f;

	[Tooltip("Input axis threshold to insta-stop player")]
	public float moveThreshold = 0.1f;

	[Tooltip("Player jump force")]
	public float jumpPower = 4f;

	[Tooltip("multiplier used to calculate boost applied to jump when moving")]
	public float jumpMultiplier = 0.5f;

	[Tooltip("jump multiplier limit")]
	public float jumpMultLimit = 1f;

	[Tooltip("Number to multiply movement speed by when sprinting")]
	public float SprintMultiplier = 1.5f;

	private Rigidbody _Rigidbody;

	private CapsuleCollider _col;

	// Vector3 to store player inputs.
	private Vector3 moveInput;

	// Awake() vs Start() - see https://gamedevbeginner.com/start-vs-awake-in-unity/
	// Use Awake() this for initialization of reference to components in this GameObject
	void Awake()
	{
		// see https://docs.unity3d.com/ScriptReference/Component.TryGetComponent.html
		TryGetComponent(out _Rigidbody);
		TryGetComponent(out _col);
	}

	void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
	}

	// Update is called once per frame draw
	void Update()
	{
		// Try to always get your player inputs in the Update method.
		moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

		if (IsGrounded() && Input.GetButtonDown("Jump"))
		{
			//Add upward force to the rigid body when we press jump.
			_Rigidbody.AddForce(Vector3.up * (jumpPower * (1 + Mathf.Clamp(Mathf.Abs(_Rigidbody.velocity.x) + Mathf.Abs(_Rigidbody.velocity.z), 0, jumpMultLimit) * jumpMultiplier)), ForceMode.Impulse);
		}

		if (Input.GetKeyDown(KeyCode.Escape))
			Cursor.lockState = CursorLockMode.None;
	}

	private void FixedUpdate()
	{
		// DON'T FORGET:
		// on players rigidbody, set: interpolation to interpolate, and collision detection to continuous

		// Rigidbody actions are handled by Unity's physics engine, so you should always mess with
		// rigidbody stuff inside FixedUpdate, this will guarantee consistent physics behaviour.

		// TODO: maybe adding jump force should really be moved here too?

		// No movement input? Instant stop player movement.
		// The IsGrounded() check let's you keep sliding while in the air.
		// remove/comment out to "slide" like a hovercraft/plane
		/*if ((moveInput.x <= moveThreshold) && (moveInput.x >= -moveThreshold) && (moveInput.z <= moveThreshold) && (moveInput.z >= -moveThreshold) && IsGrounded())
		{
			_Rigidbody.velocity = new Vector3(0, _Rigidbody.velocity.y, 0);
		}*/

		// Rotate movement inputs from world space -> player space

		if(Input.GetKey(KeyCode.LeftShift)) {
			moveInput = new Vector3(moveInput.x * SprintMultiplier, moveInput.y, moveInput.z * SprintMultiplier);
		}

		moveInput = transform.TransformDirection(moveInput);
		if(IsGrounded()) {
			_Rigidbody.velocity = new Vector3(moveInput.x * moveSpeed, _Rigidbody.velocity.y, moveInput.z * moveSpeed);
		}
		else {
			_Rigidbody.AddForce(moveInput * airMoveSpeed, ForceMode.VelocityChange);
		}
	}

	private bool IsGrounded()
	{
		//Test that we are grounded by drawing an invisible line (raycast)
		//If this hits a solid object e.g. floor then we are grounded.
		return Physics.Raycast(transform.position, Vector3.down, _col.bounds.extents.y + 0.1f);
	}
}
