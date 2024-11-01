using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Created by Joshua Guerrer
 * This script will force the enemy health 
 * bar to rotate and always face the player
 * 
 * This script idea is from GDTitan
 * "3D ENEMY AI in UNITY"
 */

public class LookAtPlayer : MonoBehaviour
{
    public Transform cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(cam);
    }
}
