using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wand_Animations : MonoBehaviour
{

    Animator myAnimator;
    public float FireRate = 1f;
    private float NextFire;
    public WaitForSeconds ShotDuration = new WaitForSeconds(.03f);

    // Start is called before the first frame update
    IEnumerator Start()
    {
        myAnimator = GetComponent<Animator>(); //Grabs animation component/controller

        while (true)
        {
            yield return new WaitForSeconds(3);

            myAnimator.SetInteger("wandInt", Random.Range(0, 2));
            myAnimator.SetTrigger("wandIdle");
        }
    }



    // Update is called once per frame
    void Update()
    {
        //bools for ads and ads firing 
        bool adsPress = Input.GetButton("Fire2");
        bool shootPress = Input.GetButton("Fire1");

        //rifle animations
        if (Input.GetButtonDown("Fire1") && Time.time > NextFire)
        {
            NextFire = Time.time + FireRate;
            StartCoroutine(ShotEffect());

            myAnimator.SetTrigger("wandShoot");

        }


        if (Input.GetKeyDown(KeyCode.P))
        {
            myAnimator.SetTrigger("wandUp");
        }


    }

    private IEnumerator ShotEffect()
    {
        yield return ShotDuration;
    }

}
