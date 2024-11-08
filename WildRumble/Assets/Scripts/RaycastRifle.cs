using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastRifle : MonoBehaviour
{
    // Created By Alex Wolfe for NVC fall 2024 Game sim
    // Followed this tutoriaal and adapted existing scripts
    // https://www.youtube.com/watch?v=AGd16aspnPA




    public int GunDamage = 20;                                                                  //Dmg Amount
    public float FireRate = .25f;                                                               //Between Shots
    public float WeaponRange = 50f;                                                             //Raycast Range
    public float HitForce = 100f;                                                               //Hit Force
    public Transform GunEnd;                                                                    //Particle Start
    public Camera Camera;                                                                       //Camera
    public WaitForSeconds ShotDuration = new WaitForSeconds(.03f);                              //How long particle lasts
    public AudioSource GunAudio;                                                                //Shot Sound
    private LineRenderer LaserLine;                                                             //Line between 2 points
    private float NextFire;                                                                     //Next shot avalible



    // Start is called before the first frame update
    void Start()
    {
        LaserLine = GetComponent<LineRenderer>();
        GunAudio = GetComponent<AudioSource>();
        Camera = GetComponentInParent<Camera>();                                           
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown ("Fire1") && Time.time > NextFire)                             //Is it allowed to shoot again
        {
            NextFire = Time.time + FireRate;    
            StartCoroutine(ShotEffect());

            Vector3 RayOrigin = Camera.ViewportToWorldPoint(new Vector3(.5f, .5f, 1));         //Center screen
            RaycastHit hit;

            LaserLine.SetPosition(0, GunEnd.position);                                         //start point                               

            if (Physics.Raycast(RayOrigin, Camera.transform.forward, out hit, WeaponRange))    //does it hit anything
            {
                LaserLine.SetPosition(1, hit.point);                                           //end point

                ObjectWithHealthBar health = hit.collider.GetComponent<ObjectWithHealthBar>();
                if (health != null)                                                            //if there is health do dmg
                {
                    health.TakeDamage(GunDamage);
                }
                if (hit.rigidbody != null)                                                     //if there is a rigidbody add force
                {
                    hit.rigidbody.AddForce(-hit.normal * HitForce);
                }

            }
            else
            {
                LaserLine.SetPosition(1, RayOrigin + (Camera.transform.forward * WeaponRange));//end point
            }
        }
    }


    private IEnumerator ShotEffect() 
    {
        GunAudio.Play();

        LaserLine.enabled = true;
        yield return ShotDuration;
        LaserLine.enabled = false;
    }





}
