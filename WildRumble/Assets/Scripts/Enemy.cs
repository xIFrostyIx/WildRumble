using UnityEngine;
using UnityEngine.UI;


//Edited by Joshua Guerrer
//Lines 50-54

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public Slider healthBar;
    public Canvas healthBarCanvas;

    public ObjectiveManager objectiveManager; // Reference to the ObjectiveManager

    void Start()
    {
        currentHealth = maxHealth;

        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
    }

    public void Damage(int damage)
    {
        currentHealth -= damage;

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

        // Notify the ObjectiveManager about the kill
        if (objectiveManager != null)
        {
            objectiveManager.UpdateObjective("Eliminate 5 Animals");
        }

        // Destroy the health bar canvas if it exists
        if (healthBarCanvas != null)
        {
            Destroy(healthBarCanvas.gameObject);
        }

        Destroy(gameObject); // Destroy the enemy
    }
}
