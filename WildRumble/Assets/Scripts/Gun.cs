using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject projectilePrefab;  // The projectile to shoot
    public Transform shootPoint;         // Point where the projectile is spawned

    public int maxAmmo = 10;             // Maximum ammo per clip
    public int currentAmmo;              // Current ammo in the clip
    public int totalAmmo = 30;           // Total ammo available for reloading
    public float reloadTime = 2f;        // Time it takes to reload
    private bool isReloading = false;

    public AudioSource audioSource;      // Audio source to play sounds
    public AudioClip shootSound;         // Shooting sound effect

    void Start()
    {
        // Initialize ammo
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        if (isReloading)
            return;

        if (Input.GetButtonDown("Fire1") && currentAmmo > 0)
        {
            Shoot();
        }
        else if (Input.GetButtonDown("Fire1") && currentAmmo <= 0)
        {
            Debug.Log("Out of ammo! Reload!");
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    void Shoot()
    {
        // Play shooting sound
        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }

        // Instantiate the projectile
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);

        // Apply forward force to the projectile if it has a Rigidbody
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = shootPoint.forward * 20f; // Adjust speed as needed
        }

        // Decrease ammo count
        currentAmmo--;
    }

    void Reload()
    {
        if (isReloading || totalAmmo <= 0)
            return;

        Debug.Log("Reloading...");
        isReloading = true;

        // Delay reload to simulate time taken
        Invoke(nameof(FinishReloading), reloadTime);
    }

    void FinishReloading()
    {
        int ammoNeeded = maxAmmo - currentAmmo;

        // Determine how much ammo to reload
        int ammoToReload = Mathf.Min(ammoNeeded, totalAmmo);

        currentAmmo += ammoToReload;
        totalAmmo -= ammoToReload;

        isReloading = false;
        Debug.Log("Reloaded!");
    }
}

