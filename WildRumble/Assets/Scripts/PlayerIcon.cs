using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Created By Joshua Guerrero
 * Allows the player icon to rotate with the player
 */

public class PlayerIcon : MonoBehaviour
{
    public Transform player; // Assign the player's transform in the inspector
    public RectTransform icon; // Assign the mini map icon RectTransform in the inspector

    void Update()
    {
         // Rotate the icon to match the player's rotation
        icon.localEulerAngles = new Vector3(0, 0, -player.eulerAngles.y); // Negative for correct orientation
    }
}
