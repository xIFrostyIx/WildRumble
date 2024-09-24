using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject projectilePrefab; // Assign the projectile prefab in the inspector
    public Transform firePoint; // Assign the position where the projectile will be instantiated
    public float projectileSpeed = 20f;
    public float destroyTime = 3f; // Time after which the projectile will be destroyed

    void Update()
    {
        // Check for player input
        if (Input.GetButtonDown("Fire1")) // "Fire1" is typically mapped to the mouse left-click or a controller button
        {
            Shoot();
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
    }
}