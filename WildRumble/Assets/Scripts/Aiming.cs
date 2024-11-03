using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Aiming : MonoBehaviour
{
    public bool toggle = false;
    public Image crosshair;
    // Start is called before the first frame update
    void Start()
    {
        crosshair = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        //if right click down switch toggle
        if (Input.GetMouseButtonDown(1))
        {
            toggle = !toggle;
            crosshair.enabled = toggle;
        }
        if (Input.GetMouseButtonUp(1)) 
        {
            toggle = !toggle;
            crosshair.enabled = toggle;
        }
    }
}
