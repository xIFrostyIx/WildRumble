using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class RaycastRifle : MonoBehaviour
{
    // Created By Alex Wolfe for NVC fall 2024 Game sim
    // Followed this tutorial and adapted existing scripts
    // https://www.youtube.com/watch?v=AGd16aspnPA
    // Followed for ammo system
    // https://www.youtube.com/watch?v=cjNMQkODh1M

    public int GunDamage = 20;                                                                  //Dmg Amount
    public float FireRate = .25f;                                                               //Between Shots
    public float WeaponRange = 50f;                                                             //Raycast Range
    public float HitForce = 100f;                                                               //Hit Force
    public Transform GunEnd;                                                                    //Particle Start
    public Camera Camera;                                                                       //Camera
    public WaitForSeconds ShotDuration = new WaitForSeconds(.03f);                              //How long particle lasts
    public AudioSource GunAudio;                                                                //Shot Sound
    public AudioSource GunEmptyAudio;                                                           //Click Sound
    public AudioSource GunReloadAudio;                                                          //reload Sound
    public AudioSource AmmoPickupAudio;                                                         //ammo pickup Sound
    public GameObject HitPoint;                                                                 //effect at spot
    private LineRenderer LaserLine;                                                             //Line between 2 points
    private float NextFire;                                                                     //Next shot available
    public TextMeshProUGUI ReloadText;                                                          //Reload Text


    public int CurrentMag;                                                                      //Current mag amount
    public int MaxMagSize = 5;                                                                  //Max mag size
    public int CurrentAmmo;                                                                     //current ammo stash
    public int MaxAmmoSize = 20;                                                                //max stash size
    public int AmmoPickupAmount = 5;
    public Slider gunShotSoundSlider;
    public Slider sfxVolumeSlider;

    // Added by Darcy

    private bool isPaused = false;
    private PauseManager pauseManager;

    // Start is called before the first frame update
    void Start()
    {
        LaserLine = GetComponent<LineRenderer>();
        GunAudio = GetComponent<AudioSource>();
        Camera = GetComponentInParent<Camera>();
        pauseManager = FindObjectOfType<PauseManager>();
        ReloadText.enabled = false;
        if (gunShotSoundSlider != null)
        {
            gunShotSoundSlider.onValueChanged.AddListener(SetGunShotVolume);
            gunShotSoundSlider.value = GunAudio.volume;
        }

        if (sfxVolumeSlider != null)
        {
            sfxVolumeSlider.onValueChanged.AddListener(SetSFXVolume);
            sfxVolumeSlider.value = AmmoPickupAudio.volume;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Added by Darcy
        if (pauseManager != null && PauseManager.isPausedGlobal)
        {
            return;
        }


        if (Input.GetButtonDown("Fire1") && Time.time > NextFire && CurrentMag > 0)             //Is it allowed to shoot again, checks mag
        {
            NextFire = Time.time + FireRate;
            StartCoroutine(ShotEffect());

            Vector3 RayOrigin = Camera.ViewportToWorldPoint(new Vector3(.5f, .5f, 1));         //Center screen
            RaycastHit hit;

            LaserLine.SetPosition(0, GunEnd.position);                                         //start point                               

            if (Physics.Raycast(RayOrigin, Camera.transform.forward, out hit, WeaponRange))    //does it hit anything
            {
                LaserLine.SetPosition(1, hit.point);                                           //end point
                                                                                               // Instantiate hit effect at the hit point
                GameObject b = Instantiate(HitPoint, hit.point, Quaternion.identity);
                Destroy(b, 1);



                ObjectWithHealthBar health = hit.collider.GetComponent<ObjectWithHealthBar>();
                if (health != null)                                                            //if there is health do dmg
                {
                    health.TakeDamage(GunDamage);
                }
                if (hit.rigidbody != null)                                                     //if there is a rigidbody add force
                {
                    hit.rigidbody.AddForce(-hit.normal * HitForce);
                }
                CurrentMag--;                                                                  //mag -1
            }
            else
            {
                LaserLine.SetPosition(1, RayOrigin + (Camera.transform.forward * WeaponRange));//end point
            }
        }
        if (CurrentMag == 0 && Input.GetButtonDown("Fire1")) 
        {
            ReloadText.enabled = true;
            GunEmptyAudio.Play();
        }

        if (Input.GetKeyDown(KeyCode.R))                                                      //Reload the Mag on R
        {
            RestockMag();
            GunReloadAudio.Play();
            ReloadText.enabled = false;
        }

    }

    private IEnumerator ShotEffect()
    {
        GunAudio.Play();

        LaserLine.enabled = true;
        yield return ShotDuration;
        LaserLine.enabled = false;
    }

    // Added by Darcy
    private IEnumerator Reload()
    {

        if (PauseManager.isPausedGlobal)
            yield break;



        yield return new WaitForSeconds(2f);

    }

    public void RestockMag()                                                                       //Reload the Mag
    {
        int ReloadAmount = MaxMagSize - CurrentMag;                                             //how many bullets to refill mag
        ReloadAmount = (CurrentAmmo - ReloadAmount) >= 0 ? ReloadAmount : CurrentAmmo;          //check how much can actually be refilled
        CurrentMag += ReloadAmount;
        CurrentAmmo -= ReloadAmount;
    }


    public void RestockAmmo(int AmmoPickupAmount)                                               //Ammo Pickup
    {
        CurrentAmmo += AmmoPickupAmount;
        CurrentAmmo = Mathf.Clamp(CurrentAmmo, 0, MaxAmmoSize);
    }
    private void OnTriggerEnter(Collider other)                                                 //Ammo Pickup
    {
        if (other.gameObject.CompareTag("Ammo"))
        {
            RestockAmmo(AmmoPickupAmount);
            AmmoPickupAudio.Play();
            Destroy(other.gameObject);
        }

    }

    public void AddAmmo(int AmmoAmount)                                                         //Add from ammo to current mag
    {
        CurrentAmmo += AmmoAmount;                                                              //increase by amount
        if (CurrentAmmo > MaxAmmoSize)
        {
            CurrentAmmo = MaxAmmoSize;
        }
    }
    public void SetGunShotVolume(float volume)
    {
        ApplyGunShotVolume(volume);
        PlayerPrefs.SetFloat("GunShotVolume", volume);
    }

    
    public void ApplyGunShotVolume(float volume)
    {
        GunAudio.volume = volume;
        GunEmptyAudio.volume = volume;
        GunReloadAudio.volume = volume;
    }



    public void SetSFXVolume(float volume)
    {
        ApplySFXVolume(volume);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    public void ApplySFXVolume(float volume)
    {
        AmmoPickupAudio.volume = volume;
    }
}