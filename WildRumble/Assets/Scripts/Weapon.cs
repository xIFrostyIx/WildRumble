using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject projectilePrefab; 
    public Transform firePoint; 
    public float projectileSpeed = 20f;
    public float destroyTime = 3f; 

    public int maxAmmo = 10; 
    public float reloadTime = 2f;
    private int currentAmmo; 
    private bool isReloading = false; 

    
    public AudioClip gunShotSound; 
    private AudioSource audioSource; 
    public float gunShotVolume = 1f; 

   
    public FleeingEnemyManager fleeingEnemyManager;

    void Start()
    {
        currentAmmo = maxAmmo; 

        
        audioSource = GetComponent<AudioSource>();

        
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        
        if (isReloading)
            return;

        
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            StartCoroutine(Reload());
            return;
        }

        
        if (Input.GetButtonDown("Fire1")) 
        {
            if (currentAmmo > 0)
            {
                Shoot();
            }
            else
            {
                
                StartCoroutine(Reload());
            }
        }
    }

    void Shoot()
    {
        
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = firePoint.forward * projectileSpeed;
        }

        
        Destroy(projectile, destroyTime);

        
        currentAmmo--;

        
        PlayGunShotSound();

        
        if (fleeingEnemyManager != null)
        {
            fleeingEnemyManager.NotifyEnemiesOfGunshot();
        }
    }

    void PlayGunShotSound()
    {
        if (gunShotSound != null)
        {
            
            audioSource.PlayOneShot(gunShotSound, gunShotVolume);
        }
    }

    IEnumerator Reload()
    {
        
        isReloading = true;
        Debug.Log("Reloading...");
        yield return new WaitForSeconds(reloadTime);

        
        currentAmmo = maxAmmo;
        isReloading = false;
        Debug.Log("Reload Complete");
    }
}
