using System;
using UnityEngine;

public class PlayerAgent : MonoBehaviour
{
    // TODO: Implement aim and rotation [Done]
    // TODO: Implement Rigidbody movement [Done]
    // TODO: Send Speed value to the UI [Done]
    // TODO: Implement Jump (and probably a multi-jump)

    [Header("Core Attributes")]

    [Tooltip("Place the input reader scriptable object here.")]
    [SerializeField] InputReader inputReader;

    [Tooltip("The gravity force applied to the player.")]
    [SerializeField] float gravity = -9.81f;

    [Tooltip("The layer the player considers ground.")]
    [SerializeField] LayerMask groundLayer;

    [Tooltip("How far from the player's center before it can detect ground.")]
    [SerializeField] float distanceToCheckGround = 1.1f;

    //----------

    [Header("Movement Attributes")]

    [Tooltip("The maximum speed the player can have with base movement.")]
    [SerializeField] float maxSpeed = 10f;

    [Tooltip("The maximum amount of acceleration applied to the player to reach max speed.")]
    [SerializeField] float maxAcceleration = 50f;

    [Tooltip("The amount of drag applied to the player when over max speed.")]
    [SerializeField] float dragCoefficient = 1f;

    //----------

    [Header("Rotation Attributes")]

    [Tooltip("How fast the player's camera rotates.")]
    [SerializeField] float mouseSensitivity = 2f;

    [Tooltip("The maximum vertical angle that the camera can rotate to.")]
    [SerializeField] float verticalClamp = 45f;

    [Tooltip("The speed at which the player rotates.")]
    [SerializeField] float rotationSpeed = 5f;

    //----------

    [Header("Jump Attributes")]

    [Tooltip("The amount of force added to the player's jump")]
    [SerializeField] float jumpForce = 10f;

    [Tooltip("Amount of times a player can jump before the player must ground itself.")]
    [SerializeField] int maxJumpAmount = 2;

    //----------

    // References
    private Camera playerCamera;
    private Rigidbody playerRigidbody;

    // Variables
    private Vector2 cameraRotation = Vector2.zero;
    private Vector2 movementInput = Vector2.zero;
    private Vector3 previousDirection = Vector3.zero;
    private int jumpCount = 0;

    // Event Actions
    public static event Action<float> PlayerSpeedEvent;

    //----------

    private void Awake()
    {
        // Subscribe methods to inputReader events
        inputReader.AimEvent += ProcessAim;
        inputReader.MovementEvent += ProcessMovement;
        inputReader.JumpStartedEvent += ProcessJump;

        // Set the references
        playerCamera = GetComponentInChildren<Camera>();
        playerRigidbody = GetComponent<Rigidbody>();

        // Set the cursor properties
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        // Reset the jump counter
        if (IsGrounded() && jumpCount != 0)
        {
            jumpCount = 0;
        }

        // Calculate the direction the player needs to go based on input and rotation
        Vector3 moveDirection = transform.right * movementInput.x + transform.forward * movementInput.y;
        moveDirection.Normalize();

        Debug.DrawRay(transform.position, moveDirection, Color.green);
        Debug.DrawRay(transform.position, previousDirection, Color.magenta);

        // Calculate the dot product between the current and prevoius velocity
        float directionChange = Vector3.Dot(moveDirection, previousDirection);

        Debug.Log(directionChange < 0.992f && moveDirection.magnitude > 0.1f);

        // Apply additional force for a snappier turn
        if (directionChange < 0.992f && moveDirection.magnitude > 0.1f)
        {
            playerRigidbody.AddForce(-previousDirection * maxAcceleration * 0.3f, ForceMode.VelocityChange);
            playerRigidbody.AddForce(moveDirection * maxAcceleration * 0.3f, ForceMode.VelocityChange);
        }
        else
        {
            // Apply acceleration, clamping the magnitude of the acceleration force
            float desiredAcceleration = Mathf.Clamp(moveDirection.magnitude * maxAcceleration, 0, maxAcceleration);
            playerRigidbody.AddForce(moveDirection * desiredAcceleration, ForceMode.Acceleration);

            // Apply drag to gradually reduce the player velocity to maxSpeed
            Vector3 velocityWithoutY = playerRigidbody.velocity;
            velocityWithoutY.y = 0f;
            Vector3 dragForce = -velocityWithoutY.normalized * Mathf.Min(velocityWithoutY.magnitude, maxSpeed) * dragCoefficient;
            playerRigidbody.AddForce(dragForce, ForceMode.Acceleration);
        }

        // Invoke the event
        PlayerSpeedEvent?.Invoke(playerRigidbody.velocity.magnitude);

        // Apply gravity to the player
        if (!IsGrounded())
        {
            playerRigidbody.velocity += new Vector3(0f, gravity * Time.fixedDeltaTime, 0f);
        }

        // Apply the current Move Direction to the previous move direction
        previousDirection = moveDirection;
    }

    private void LateUpdate()
    {
        // Set the target rotations to quaternions
        Quaternion targetPlayerRotation = Quaternion.Euler(0, cameraRotation.x, 0);
        Quaternion targetCameraRotation = Quaternion.Euler(cameraRotation.y, 0, 0);

        // Apply and interpolate the new rotations to the player and camera
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetPlayerRotation, rotationSpeed * Time.deltaTime);
        playerCamera.transform.localRotation = Quaternion.Slerp(playerCamera.transform.localRotation, targetCameraRotation, rotationSpeed * Time.deltaTime);
    }

    private void ProcessAim(Vector2 input)
    {
        // Calculate the new camera rotation
        cameraRotation.x += input.x * mouseSensitivity * 10 * Time.deltaTime;
        cameraRotation.y -= input.y * mouseSensitivity * 10 * Time.deltaTime;

        // Clamp the camera's Y rotation
        cameraRotation.y = Mathf.Clamp(cameraRotation.y, -verticalClamp, verticalClamp);
    }

    private void ProcessMovement(Vector2 input)
    {
        // Update the input
        movementInput = input;
    }

    private void ProcessJump()
    {
        if (jumpCount <= maxJumpAmount)
        {
            playerRigidbody.AddForce(0, jumpForce, 0, ForceMode.VelocityChange);
        }
    }

    private bool IsGrounded()
    {
        // Uses a raycast to check for grounded
        return Physics.Raycast(transform.position, Vector3.down, distanceToCheckGround, groundLayer);
    }
}
