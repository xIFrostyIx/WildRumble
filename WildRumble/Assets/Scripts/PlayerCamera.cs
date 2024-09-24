using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Created by; Joshua Guerrer
 * Allows the camera to be connected
 * through a game object attached
 * to the player
 */
public class PlayerCamera : MonoBehaviour
{
    public Transform cameraPosition;

    private void Update()
    {
        transform.position = cameraPosition.position;
    }
}
