using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class duckGun_Animations : MonoBehaviour
{

    Animator myAnimator;
    public float FireRate = 1f;
    private float NextFire;
    public WaitForSeconds ShotDuration = new WaitForSeconds(.03f);

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }



    // Update is called once per frame
    void Update()
    {
        //bools for ads and ads firing 
        bool duckADSin = myAnimator.GetBool("duckADSin");
        bool adsPress = Input.GetButton("Fire2");
        bool shootPress = Input.GetButton("Fire1");

        //rifle animations
        if (Input.GetButtonDown("Fire1") && Time.time > NextFire)
        {
            NextFire = Time.time + FireRate;
            StartCoroutine(ShotEffect());

            myAnimator.SetTrigger("duckShoot");

        }


        if (Input.GetKeyDown(KeyCode.Q))
        {
            myAnimator.SetTrigger("duckDown");
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            myAnimator.SetTrigger("duckUp");
        }


        //ADS aniamtions
        if (!duckADSin && adsPress)
        {
            myAnimator.SetBool("duckADSin", true);
        }

        if (duckADSin && !adsPress)
        {
            myAnimator.SetBool("duckADSin", false);
        }

        if (shootPress && adsPress)
        {
            myAnimator.SetTrigger("duckADSshoot");
        }


    }

    private IEnumerator ShotEffect()
    {
        yield return ShotDuration;
    }

}
