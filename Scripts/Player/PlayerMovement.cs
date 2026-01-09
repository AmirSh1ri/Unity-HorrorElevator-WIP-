using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player Movement Logic (Keyboard/Controllers)
[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 4f;
    public float runMultiplier = 2f;
    public float gravity = -9.81f;

    [Header("Head Bob Settings")]
    public Transform playerCamera;
    public float bobFrequency = 6f;
    public float bobAmplitude = 0.05f;
    public float runBobMultiplier = 1.5f;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isRunning;
    private float bobTimer;
    private Vector3 camInitialPos;
    private Vector3 moveInput;
    private Vector3 lastPosition;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        camInitialPos = playerCamera.localPosition;
        lastPosition = transform.position;
    }

    void Update()
    {
        HandleMovement();
        HandleHeadBob();
    }

    void HandleMovement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        isRunning = Input.GetButton("Fire3");

        Transform cam = Camera.main.transform;
        Vector3 forward = cam.forward;
        Vector3 right = cam.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        moveInput = (forward * v + right * h).normalized;

        float speed = walkSpeed * (isRunning ? runMultiplier : 1f);
        Vector3 horizontalMove = moveInput * speed;

        controller.Move(horizontalMove * Time.deltaTime);

        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    // HeadBob Based On speed
    void HandleHeadBob()
    {
        float movedDistance = Vector3.Distance(transform.position, lastPosition);
        lastPosition = transform.position;

        if (movedDistance > 0.01f && controller.isGrounded)
        {
            bobTimer += Time.deltaTime * bobFrequency * (isRunning ? runBobMultiplier : 1f);
            float bobOffsetY = Mathf.Sin(bobTimer) * bobAmplitude;
            float bobOffsetX = Mathf.Cos(bobTimer * 0.5f) * bobAmplitude * 0.5f;
            playerCamera.localPosition = camInitialPos + new Vector3(bobOffsetX, bobOffsetY, 0f);
        }
        else
        {
            bobTimer = 0f;
            playerCamera.localPosition = Vector3.Lerp(playerCamera.localPosition, camInitialPos, Time.deltaTime * 5f);
        }
    }
}
