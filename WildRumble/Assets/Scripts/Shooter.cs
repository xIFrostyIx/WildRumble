using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public Camera PlayerCamera; // Reference to the player's camera
    public GameObject HitPoint; // Hit effect prefab
    public GameObject MuzzleFlash; // Muzzle flash prefab
    public Transform MuzzleFlashPoint; // Position for muzzle flash
    public int DamageAmount = 0;
    public AudioSource ShootingAudio; // Audio source for gunfire sound

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shooting();
        }
    }

    public void Shooting()
    {
        // Play the shooting sound
        if (ShootingAudio != null)
        {
            ShootingAudio.Play();
        }

        // Create muzzle flash
        if (MuzzleFlash != null && MuzzleFlashPoint != null)
        {
            GameObject flash = Instantiate(MuzzleFlash, MuzzleFlashPoint.position, MuzzleFlashPoint.rotation);
            Destroy(flash, 0.1f); // Destroy the muzzle flash shortly after creation
        }

        RaycastHit hit;

        // Raycast from the center of the screen using the camera's forward direction
        Vector3 rayOrigin = PlayerCamera.transform.position;
        Vector3 rayDirection = PlayerCamera.transform.forward;

        if (Physics.Raycast(rayOrigin, rayDirection, out hit, 100))
        {
            Debug.DrawRay(rayOrigin, rayDirection * hit.distance, Color.red);

            // Instantiate hit effect at the hit point
            GameObject b = Instantiate(HitPoint, hit.point, Quaternion.identity);
            Destroy(b, 1);

            // Deal damage if the hit object has an Enemy component
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.Damage(DamageAmount);
            }
        }
    }
}
