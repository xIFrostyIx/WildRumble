using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class r700_Animations : MonoBehaviour
{

    Animator myAnimator;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //bools for ads and ads firing 
        bool rifleADSin = myAnimator.GetBool("rifleADSin");
        bool adsPress = Input.GetButton("Fire2");
        bool shootPress = Input.GetButton("Fire1");

        //rifle animations
        if (Input.GetButtonDown("Fire1"))
        {
            myAnimator.SetTrigger("rifleShoot");
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            myAnimator.SetTrigger("rifleReload");
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            myAnimator.SetTrigger("rifleDown");
        }

        if (Input.GetKeyDown(KeyCode.E))
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
}
