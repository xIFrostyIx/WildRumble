using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Created by Alex Wolfe for NVC Fall 2024 Game Sim 
public class WeaponSwap : MonoBehaviour
{
    public int CurrentWeapon = 0;

    // Reference to the ButtonManager to call the volume setting method
    public ButtonManager buttonManager;

    // Start is called before the first frame update
    void Start()
    {
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        if (HealthBar.isGameOver || PauseManager.isPausedGlobal) // Added by Darcy
            return;

        int LastWeapon = CurrentWeapon;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)               //scroll wheel switching
        {
            if (CurrentWeapon >= transform.childCount - 1)
                CurrentWeapon = 0;
            else
                CurrentWeapon++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)               //scroll wheel switching
        {
            if (CurrentWeapon <= 0)
                CurrentWeapon = transform.childCount - 1;
            else
                CurrentWeapon--;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))                    //1 = 1st weapon in set
        {
            CurrentWeapon = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)                    //2 = 2nd weapon in set and check if there is more than 1 weapon
        {
            CurrentWeapon = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)                    //3 = 3rd weapon in set and check if there is more than 1 weapon
        {
            CurrentWeapon = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && transform.childCount >= 4)                    //4 = 4th weapon in set and check if there is more than 1 weapon
        {
            CurrentWeapon = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5) && transform.childCount >= 5)                    //5 = 5th weapon in set and check if there is more than 1 weapon
        {
            CurrentWeapon = 4;
        }
        if (Input.GetKeyDown(KeyCode.Alpha6) && transform.childCount >= 6)                    //6 = 6th weapon in set and check if there is more than 1 weapon
        {
            CurrentWeapon = 5;
        }

        if (LastWeapon != CurrentWeapon)                           //Toggle on/off weapons in set
        {
            SelectWeapon();
        }
    }

    void SelectWeapon()                                          //weapon loop
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == CurrentWeapon)
            {
                weapon.gameObject.SetActive(true);
                //Added by Darcy
                if (buttonManager != null)
                {
                    buttonManager.SetGunShotVolume(PlayerPrefs.GetFloat("GunShotVolume", 1f));  // Ensure the global volume is applied
                }
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }
}
