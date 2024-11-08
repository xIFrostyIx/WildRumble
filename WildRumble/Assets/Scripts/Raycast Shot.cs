using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastShot : MonoBehaviour
{
    public float WeaponRange = 50f;
    public Camera Camera;


    private void Start()
    {
        Camera = GetComponentInParent<Camera>();
    }

    void Update()
    {
        Vector3 LineOrigin = Camera.ViewportToWorldPoint(new Vector3(.5f, .5f, 0));          //Center screen
        Debug.DrawRay(LineOrigin, Camera.transform.forward * WeaponRange, Color.cyan);
    }

}
