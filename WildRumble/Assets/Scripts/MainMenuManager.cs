using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Created by Joshua Guerrero
 * Makes cursor visable in main menu
 */

public class MainMenuManager : MonoBehaviour
{
    private void Start()
    {
        // Ensure the cursor is visible and unlocked
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
