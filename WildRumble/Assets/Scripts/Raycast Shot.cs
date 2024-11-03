using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastShot : MonoBehaviour
{
    public Transform Firepoint;
    public Transform LookHere;

    // Update is called once per frame
    void Update()
    {
        Shooting();
    }

    public void Shooting()
    { 
        RaycastHit hit;

        if (Physics.Raycast(Firepoint.position, transform.TransformDirection(Vector3.forward), out hit,1000))               //send out raycast forward from base object
        {
            Debug.DrawRay(Firepoint.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.cyan);    //if it hits show a cyan line
            LookHere.transform.LookAt(hit.transform.position);                                                              //have where bullet comes out look at where the raycast hits
        }
        //The idea is to have the raycast correct where the bullet is supposed to go
    }
}
