using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoUI : MonoBehaviour
{
    public RaycastRifle weapon;
    public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        UpdateAmmoText();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAmmoText();
    }
    
    public void UpdateAmmoText() 
    {
        text.text = $"{weapon.CurrentMag} / {weapon.MaxMagSize}\n{weapon.CurrentAmmo} / {weapon.MaxAmmoSize}";
    }
}
