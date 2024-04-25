using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -400f; //Control the gravity velocity here

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public float jumpHeight = 6f;
    public float jumpSmoothing = 0.5f;

    public Vector3 velocity;
    public float sprintModifier = 6f;
    bool isGrounded;

    void Start()
    {
        
    }

    // Update is called once per frame
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
        move *= speed * Time.deltaTime; // Scale movement by speed and time

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        else if (Input.GetButtonUp("Jump") && !isGrounded)
        {
            velocity.y *= jumpSmoothing;
        }


        // Apply gravity
        velocity.y += gravity * Time.deltaTime;

        // Add gravity to the movement vector
        move += velocity * Time.deltaTime;

        // Apply the combined movement to the controller
        controller.Move(move);
    }
}
