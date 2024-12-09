using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Created by
 * This script handles the animations for
 * the rifle
 *  
 * edited by Joshua
 * lines:
 * 24, 39, 49-60
 * These edits stop the animmation from
 * playing when there is no ammo left in the rifle
 */

public class r700_Animations : MonoBehaviour
{

    Animator myAnimator;
    public float FireRate = 1f;
    private float NextFire;
    public WaitForSeconds ShotDuration = new WaitForSeconds(.03f);

    // Reference to the RaycastRifle script
    private RaycastRifle raycastRifle;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        raycastRifle = GetComponentInParent<RaycastRifle>();
    }

    // Update is called once per frame
    void Update()
    {
        if (raycastRifle == null) return;

        AnimatorStateInfo stateInfo = myAnimator.GetCurrentAnimatorStateInfo(0);

        // Animation states
        bool isReloading = stateInfo.IsName("rifleReload"); // Replace with the actual reload animation state name
        bool isShooting = stateInfo.IsName("rifleShoot");   // Replace with the actual shoot animation state name

        // Handle shooting
        if (Input.GetButtonDown("Fire1") && Time.time > NextFire && !isReloading && !isShooting)
        {
            if (raycastRifle.CurrentMag > 0)
            {
                NextFire = Time.time + FireRate;
                StartCoroutine(ShotEffect());
                myAnimator.SetTrigger("rifleShoot");
            }
            else
            {
                Debug.Log("No ammo to shoot");
            }
        }

        // Handle reloading
        if (Input.GetKeyDown(KeyCode.R) && !isShooting && !isReloading)
        {
            myAnimator.SetTrigger("rifleReload");
        }

        // Handle rifle animations
        if (Input.GetKeyDown(KeyCode.Q) && !isShooting && !isReloading)
        {
            myAnimator.SetTrigger("rifleDown");
        }

        if (Input.GetKeyDown(KeyCode.P) && !isShooting && !isReloading)
        {
            myAnimator.SetTrigger("rifleUp");
        }

        // ADS animations
        bool rifleADSin = myAnimator.GetBool("rifleADSin");
        bool adsPress = Input.GetButton("Fire2");

        if (!rifleADSin && adsPress)
        {
            myAnimator.SetBool("rifleADSin", true);
        }

        if (rifleADSin && !adsPress)
        {
            myAnimator.SetBool("rifleADSin", false);
        }

        // ADS shooting
        if (Input.GetButton("Fire1") && adsPress && !isShooting && !isReloading)
        {
            myAnimator.SetTrigger("rifleADSshoot");
        }
    }

    private IEnumerator ShotEffect()
    {
        yield return ShotDuration;
    }

}