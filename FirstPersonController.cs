using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float speed = 5.0f;
    public float sensitivity = 2.0f;
    public Camera playerCamera;

    private float pitch = 0.0f;
    private float yaw = 0.0f;

    void Start()
    {
        // Lock the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Handle player rotation
        yaw += sensitivity * Input.GetAxis("Mouse X");
        pitch -= sensitivity * Input.GetAxis("Mouse Y");

        // Clamp pitch value to range [-90, 90] to prevent flipping
        pitch = Mathf.Clamp(pitch, -90, 90);

        // Apply rotation to the player
        transform.eulerAngles = new Vector3(0, yaw, 0);
        playerCamera.transform.localEulerAngles = new Vector3(pitch, 0, 0);

        // Handle player movement
        float moveForwardBackward = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float moveLeftRight = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        Vector3 movement = transform.forward * moveForwardBackward + transform.right * moveLeftRight;
        transform.position += movement;
    }
}