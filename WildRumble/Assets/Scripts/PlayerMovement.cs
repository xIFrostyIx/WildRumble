using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float groundDrag;
    public float sprintSpeed;
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump = true;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask Ground;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;

    Rigidbody rb;

    public AudioClip footstepSound; // Footstep sound
    private AudioSource audioSource;
    public float footstepVolume = 1f; // Volume controlled by ambience slider
    public float footstepInterval = 0.5f; // Interval between footstep sounds
    private float footstepTimer;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = footstepSound; // Assign footstep sound to audio source
        audioSource.loop = false; // Make sure it doesn't loop
    }
    //Edited by Darcy
    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + .2f, Ground);
        MyInput();
        SpeedControl();

        rb.drag = grounded ? groundDrag : 0;

        
        if (grounded && (horizontalInput != 0 || verticalInput != 0) && footstepTimer <= 0f)
        {
            PlayFootstepSound();
            footstepTimer = footstepInterval; 
        }

        
        if (footstepTimer > 0f)
        {
            footstepTimer -= Time.deltaTime;
        }
    }
    //
    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : moveSpeed;

        if (grounded)
            rb.AddForce(moveDirection.normalized * currentSpeed * 10f, ForceMode.Force);
        else
            rb.AddForce(moveDirection.normalized * currentSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }
    //edited by Darcy
    private void PlayFootstepSound()
    {
        if (audioSource != null && footstepSound != null && !audioSource.isPlaying)
        {
            audioSource.volume = footstepVolume;
            audioSource.PlayOneShot(footstepSound);
        }
    }

    public void SetFootstepVolume(float volume)
    {
        footstepVolume = volume; 
    }
    //
}
