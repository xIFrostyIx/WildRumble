using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Created by: Eben Martinez
 * This script animates the player character
 * 
 */

public class hunterMovmentAnimation : MonoBehaviour
{
    Animator myAnimator; // Used for animations

    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    void Update()
    {

        //Walk forward
        if (Input.GetKey("w"))
        {
            myAnimator.SetBool("isWalking", true);
        }

        if (!Input.GetKey("w"))
        {
            myAnimator.SetBool("isWalking", false);
        }

        //walk Left
        if (Input.GetKey("a"))
        {
            myAnimator.SetBool("isWalkingLeft", true);
        }

        if (!Input.GetKey("a"))
        {
            myAnimator.SetBool("isWalkingLeft", false);
        }

        //walk Right
        if (Input.GetKey("d"))
        {
            myAnimator.SetBool("isWalkingRight", true);
        }

        if (!Input.GetKey("d"))
        {
            myAnimator.SetBool("isWalkingRight", false);
        }

        //walk Back
        if (Input.GetKey("s"))
        {
            myAnimator.SetBool("isWalkingBack", true);
        }

        if (!Input.GetKey("s"))
        {
            myAnimator.SetBool("isWalkingBack", false);
        }

        //Run
        if (Input.GetKey("left shift"))
        {
            myAnimator.SetBool("isRunning", true);
        }

        if (!Input.GetKey("left shift"))
        {
            myAnimator.SetBool("isRunning", false);
        }

    }
}
