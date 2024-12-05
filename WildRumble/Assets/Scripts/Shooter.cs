using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public Camera PlayerCamera; // Reference to the player's camera
    public GameObject HitPoint;
    public int DamageAmount = 0;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shooting();
        }
    }

    public void Shooting()
    {
        RaycastHit hit;

        // Raycast from the center of the screen using the camera's forward direction
        Vector3 rayOrigin = PlayerCamera.transform.position;
        Vector3 rayDirection = PlayerCamera.transform.forward;

        if (Physics.Raycast(rayOrigin, rayDirection, out hit, 100))
        {
            Debug.DrawRay(rayOrigin, rayDirection * hit.distance, Color.red);

            // Instantiate hit effect at the hit point
            GameObject b = Instantiate(HitPoint, hit.point, Quaternion.identity);
            Destroy(b, 1);

            // Deal damage if the hit object has an Enemy component
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.Damage(DamageAmount);
            }
        }
    }
}
