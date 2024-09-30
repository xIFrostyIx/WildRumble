using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;   
    public int maxHealth = 100;   
    private int currentHealth;   

    public int damageAmount = 10; 

    void Start()
    {
        
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); 

        
        healthSlider.value = currentHealth;

        if (currentHealth <= 0)
        {
           
            Debug.Log("Player has died.");
        }
    }

    
    void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Enemy")
        {
            TakeDamage(damageAmount);
            Debug.Log("Player took damage from " + other.gameObject.name);
        }
    }

    
    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

       
        healthSlider.value = currentHealth;
    }
}