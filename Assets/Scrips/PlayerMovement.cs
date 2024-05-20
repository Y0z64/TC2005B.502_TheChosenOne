using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance { get; private set; } // Singleton instance

    public CharacterController controller;
    public float gravity = -29.43f;

    public float speed = 12f;
    public float sprintSpeed = 25f;
    private float originalSpeed;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public float jumpHeight = 6f;
    public float jumpSmoothing = 0.5f;

    public Vector3 velocity;
    bool isGrounded;

    public float teleportDistance = 5f; // Distance to teleport forward
    private int teleportCount = 3; // Counter for teleport uses
    private float teleportResetTime = 10f; // Time in seconds to reset the counter

    public UIControl uiControl; // Reference to the UIControl script

    public float speedBoostDuration = 5f; // Duration of the speed boost
    public float speedBoostMultiplier = 2f; // Speed multiplier for the boost

    void Awake()
    {
        // Ensure only one instance exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: Keeps the instance persistent across scenes
        }
    }

    void Start()
    {
        originalSpeed = speed;
        UpdateTeleportCountUI(); // Initial update of the UI text
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            move *= sprintSpeed * Time.deltaTime;
        }
        else
        {
            move *= speed * Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        else if (Input.GetButtonUp("Jump") && !isGrounded)
        {
            velocity.y *= jumpSmoothing;
        }

        velocity.y += gravity * Time.deltaTime;
        move += velocity * Time.deltaTime;
        controller.Move(move);

        // Teleport forward when the 'J' key is pressed
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (teleportCount > 0)
            {
                TeleportForward();
                teleportCount--;
                UpdateTeleportCountUI(); // Update the UI text
                if (teleportCount == 2) // Start the timer when the first teleport is used
                {
                    StartCoroutine(ResetTeleportCounter());
                }
            }
            else
            {
                Debug.Log("Teleport limit reached. Please wait before teleporting again.");
            }
        }

        // Just for testing, press 'P' to simulate picking up a speed boost
        if (Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(SpeedBoost());
        }
    }

    void TeleportForward()
    {
        // Calculate the new position by moving forward by teleportDistance
        Vector3 newPosition = transform.position + transform.forward * teleportDistance;

        // Move the character controller to the new position
        controller.enabled = false; // Disable the controller to set the position directly
        transform.position = newPosition;
        controller.enabled = true; // Re-enable the controller
    }

    IEnumerator ResetTeleportCounter()
    {
        yield return new WaitForSeconds(teleportResetTime);
        teleportCount = 3; // Reset teleport count
        UpdateTeleportCountUI(); // Update the UI text
        Debug.Log("Teleport counter reset.");
    }

    void UpdateTeleportCountUI()
    {
        if (uiControl != null)
        {
            uiControl.UpdateTeleportCountDisplay(teleportCount);
        }
    }

    IEnumerator SpeedBoost()
    {
        speed *= speedBoostMultiplier; // Double the player's speed
        yield return new WaitForSeconds(speedBoostDuration); // Wait for the boost duration
        speed = originalSpeed; // Reset to original speed
    }
}
