using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject projectilePrefab; // Assign the projectile prefab in the inspector
    public Transform firePoint; // Assign the position where the projectile will be instantiated
    public float projectileSpeed = 20f;
    public float destroyTime = 3f; // Time after which the projectile will be destroyed

    public int maxAmmo = 10; // Maximum number of shots before reloading
    public float reloadTime = 2f; // Time it takes to reload
    private int currentAmmo; // Current ammo left in the weapon
    private bool isReloading = false; // Flag to indicate if the weapon is reloading

    // Audio components for the gunshot sound
    public AudioClip gunShotSound; // The sound clip for the gunshot
    private AudioSource audioSource; // AudioSource to play the sound
    public float gunShotVolume = 1f; // Volume control for the gunshot sound, default to full volume

    void Start()
    {
        currentAmmo = maxAmmo; // Initialize the weapon with full ammo

        // Initialize the AudioSource component
        audioSource = GetComponent<AudioSource>();

        // If there's no AudioSource on the weapon object, add one
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        // If the weapon is reloading, do not allow shooting
        if (isReloading)
            return;

        // Check if the player wants to reload manually
        if (Input.GetKeyDown(KeyCode.R)) // "R" key for manual reload
        {
            StartCoroutine(Reload());
            return;
        }

        // Check for player input to fire
        if (Input.GetButtonDown("Fire1")) // "Fire1" is typically mapped to the mouse left-click or a controller button
        {
            if (currentAmmo > 0)
            {
                Shoot();
            }
            else
            {
                // Start reloading if there's no ammo left
                StartCoroutine(Reload());
            }
        }
    }

    void Shoot()
    {
        // Instantiate the projectile at the firePoint's position and rotation
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // Add velocity to the projectile
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = firePoint.forward * projectileSpeed;
        }

        // Destroy the projectile after 'destroyTime' seconds
        Destroy(projectile, destroyTime);

        // Reduce the ammo count
        currentAmmo--;

        // Play the gunshot sound
        PlayGunShotSound();
    }

    void PlayGunShotSound()
    {
        if (gunShotSound != null)
        {
            // Play the gunshot sound with the specified volume
            audioSource.PlayOneShot(gunShotSound, gunShotVolume);
        }
    }

    IEnumerator Reload()
    {
        // Set reloading flag to true and wait for reload time
        isReloading = true;
        Debug.Log("Reloading...");
        yield return new WaitForSeconds(reloadTime);

        // Reset ammo and reloading flag
        currentAmmo = maxAmmo;
        isReloading = false;
        Debug.Log("Reload Complete");
    }
}
