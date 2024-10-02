using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public Light ThisLight;
    public bool LightToggle;
    // Start is called before the first frame update
    void Start()
    {
        ThisLight = GetComponent<Light>();
        ThisLight.intensity = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (ThisLight.intensity == 0)
            {
                ThisLight.intensity = 15;
                LightToggle = true;
            }
            else if (ThisLight.intensity == 15)
            {
                ThisLight.intensity = 0;
                LightToggle = false;
            }
        }
    }
}
