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
        bool forwardPressed = Input.GetKey("w");
        bool backwardPressed = Input.GetKey("s");
        bool runPressed = Input.GetKey("left shift");
        bool isWalking = myAnimator.GetBool("isWalking");
        bool isRunning = myAnimator.GetBool("isRunning");
        bool isBackWalking = myAnimator.GetBool("isBackWalking");

        if (isWalking && forwardPressed)
        {
            myAnimator.SetBool("isWalking", true);
        }

        if (isWalking && !forwardPressed)
        {
            myAnimator.SetBool("isWalking", false);
        }

        if (!isRunning && (isWalking && forwardPressed))
        {
            myAnimator.SetBool("isRunning", true);
        }

        if (isRunning && (!isWalking || !forwardPressed))
        {
            myAnimator.SetBool("isRunning", false);
        }

        if (isBackWalking && backwardPressed)
        {
            myAnimator.SetBool("isBackWalking", true);
        }

        if (isBackWalking && !backwardPressed)
        {
            myAnimator.SetBool("isBackWalking", false);
        }



    }
}
