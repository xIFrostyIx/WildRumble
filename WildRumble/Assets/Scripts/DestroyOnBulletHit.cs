using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // For the health bar UI

public class ObjectWithHealthBar : MonoBehaviour
{
    public int maxHealth = 100;  
    private int currentHealth;

    public Slider healthBar;  

    void Start()
    {
        currentHealth = maxHealth; 
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth; 
            healthBar.value = currentHealth; 
        }
    }

   
    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(20);  
        }
    }

   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            TakeDamage(20);  
        }
    }

   
    private void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (healthBar != null)
        {
            healthBar.value = currentHealth;  
        }

      
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    
    private void Die()
    {
      
        Debug.Log("Object destroyed!");
        Destroy(gameObject);  
    }
}
