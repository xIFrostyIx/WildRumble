using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Created By; Joshua Guerrero
 * This script sets up the X and Y movement 
 * of the first person camera
 * using the mouse
 */
public class CameraMovement : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;

    float xRotation;
    float yRotation;

    // Added by Darcy from 21 to 25
    private Vector3 originalPosition;
    public float shakeDuration = 0.05f; 
    public float shakeMagnitude = 0.02f; 
    private bool isShaking = false;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        originalPosition = transform.localPosition; //ADded by Darcy
    }

    private void Update()
    {
        if (!isShaking)
        {
            // Mouse Input and sensitivity
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

            yRotation += mouseX;

            xRotation -= mouseY;
            // Locks the angle of looking up and down 
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        }
    }
    // Added by Darcy
    public IEnumerator CameraShake()
    {
        isShaking = true;
        float elapsed = 0.0f;

        while (elapsed < shakeDuration)
        {
            float xOffset = Random.Range(-1f, 1f) * shakeMagnitude;
            float yOffset = Random.Range(-1f, 1f) * shakeMagnitude;

            
            transform.localPosition = originalPosition + new Vector3(xOffset, yOffset, 0);

            elapsed += Time.deltaTime;

            yield return null; 
        }

        
        transform.localPosition = originalPosition;
        isShaking = false;
    }
}
