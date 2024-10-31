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
        if (Input.GetButtonDown("Fire1"))
        {
            myAnimator.SetBool("rifleShoot", true);
        }

        if (!Input.GetButtonDown("Fire1"))
        {
            myAnimator.SetBool("rifleShoot", false);
        }

    }
}
