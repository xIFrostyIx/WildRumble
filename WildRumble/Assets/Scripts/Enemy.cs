using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required for UI elements

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;       // Maximum health of the enemy
    private int currentHealth;       // Current health of the enemy

    public Slider healthBar;         // Reference to the UI Slider for the health bar
    public Canvas healthBarCanvas;   // Canvas containing the health bar (optional, to destroy on death)

    void Start()
    {
        currentHealth = maxHealth;

        // Initialize the health bar
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
    }

    public void Damage(int damage)
    {
        currentHealth -= damage;

        // Update the health bar
        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }

        Debug.Log($"{gameObject.name} took {damage} damage. Health: {currentHealth}/{maxHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log($"{gameObject.name} has died!");

        // Destroy the health bar canvas if it exists
        if (healthBarCanvas != null)
        {
            Destroy(healthBarCanvas.gameObject);
        }

        Destroy(this.gameObject); // Destroy the enemy
    }
}
