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

    //Reference to the RaycastRifle script
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

        //bools for ads and ads firing 
        bool rifleADSin = myAnimator.GetBool("rifleADSin");
        bool adsPress = Input.GetButton("Fire2");
        bool shootPress = Input.GetButton("Fire1");

        //rifle animations
        if (Input.GetButtonDown("Fire1") && Time.time > NextFire)
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

        if (Input.GetKeyDown(KeyCode.R))
        {
            myAnimator.SetTrigger("rifleReload");
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            myAnimator.SetTrigger("rifleDown");
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            myAnimator.SetTrigger("rifleUp");
        }


        //ADS aniamtions
        if (!rifleADSin && adsPress)
        {
            myAnimator.SetBool("rifleADSin", true);
        }

        if (rifleADSin && !adsPress)
        {
            myAnimator.SetBool("rifleADSin", false);
        }

        if (shootPress && adsPress)
        {
            myAnimator.SetTrigger("rifleADSshoot");
        }


    }

    private IEnumerator ShotEffect()
    {
        yield return ShotDuration;
    }

}