using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIcon : MonoBehaviour
{
    public GameObject player; // Assign your player GameObject in the Inspector
    public RectTransform miniMapIcon; // Assign your mini-map icon RectTransform in the Inspector

    void Update()
    {
        if (player != null && miniMapIcon != null)
        {
            // Get the player's rotation
            Quaternion playerRotation = player.transform.rotation;

            // Convert to Euler angles (using y-axis for 2D rotation)
            float angle = playerRotation.eulerAngles.y;

            // Rotate the mini-map icon
            miniMapIcon.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
