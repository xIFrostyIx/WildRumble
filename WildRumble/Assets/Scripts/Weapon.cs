using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 20f;
    public float destroyTime = 3f;

    public int currentAmmo;
    public int maxAmmo = 10;
    public float reloadTime = 2f;
    private bool isReloading = false;

    public AudioClip gunShotSound;
    public AudioClip reloadSound;
    private AudioSource audioSource;
    public float gunShotVolume = 1f; // Gunshot volume

    public bool isWand = false;

    public FleeingEnemyManager fleeingEnemyManager;

    void Start()
    {
        currentAmmo = maxAmmo;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Load gunShotVolume from PlayerPrefs
        gunShotVolume = PlayerPrefs.GetFloat("GunShotVolume", 1f);
    }

    void Update()
    {
        if (HealthBar.isGameOver || isReloading || PauseManager.isPausedGlobal) // Added by Darcy
            return;

        if (Input.GetKeyDown(KeyCode.R) && !isReloading)  
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButtonDown("Fire1"))  
        {
            if (currentAmmo > 0)
            {
                if (isWand == true)
                {
                    Wait();
                    Shoot();
                    StartCoroutine(Reload());
                }
                else
                {
                    Shoot();
                    StartCoroutine(Reload());
                }
            }
            /* else
             {
                 StartCoroutine(Reload());
             }*/
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
        yield return new WaitForSeconds(reloadTime);
        isReloading = true;
        Debug.Log("Reloading...");
        PlayReloadSound();


        currentAmmo = maxAmmo;
        isReloading = false;
        Debug.Log("Reload Complete");
    }

    void PlayReloadSound()
    {
        if (reloadSound != null)
        {
            audioSource.PlayOneShot(reloadSound, gunShotVolume);
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3f);
    }
}
