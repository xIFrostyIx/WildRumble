using UnityEngine;

public class GunAim : MonoBehaviour
{
    public Transform activeWeapon;

    public Transform defaultPosition;
    public Transform adsPosition;
    public Vector3 weaponPosition; // set to 0 0 0 in inspector

    public float aimSpeed = 0.25f; // time to enter ADS
    public float _defaultFOV = 80f; // FOV in degrees
    public float zoomRatio = 0.5f; // 1/zoom times

    public Camera fpsCam; // player camera

    void Update()
    {
        // ADS camera and gun movement
        if (Input.GetButton("Fire2"))
        {
            weaponPosition = Vector3.Lerp(weaponPosition, adsPosition.localPosition, aimSpeed * Time.deltaTime);
            activeWeapon.localPosition = weaponPosition;
            SetFieldOfView(Mathf.Lerp(fpsCam.fieldOfView, zoomRatio * _defaultFOV, aimSpeed * Time.deltaTime));
        }
        else
        {
            weaponPosition = Vector3.Lerp(weaponPosition, defaultPosition.localPosition, aimSpeed * Time.deltaTime);
            activeWeapon.localPosition = weaponPosition;
            SetFieldOfView(Mathf.Lerp(fpsCam.fieldOfView, _defaultFOV, aimSpeed * Time.deltaTime));
        }
    }

    void SetFieldOfView(float fov)
    {
        fpsCam.fieldOfView = fov;
    }
}

