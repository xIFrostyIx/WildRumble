using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // For the health bar UI

public class ObjectWithHealthBar : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public Slider healthBar;

    public ObjectiveManager objectiveManager;

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
            TakeDamage(20);  // Assuming a fixed damage of 20 per bullet
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            TakeDamage(20);  // Assuming a fixed damage of 20 per bullet
        }
    }

    public void TakeDamage(int damageAmount)
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
        // Assuming the enemy has the "Enemy" tag
        if (gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Animal eliminated!");
            objectiveManager.UpdateObjective("Eliminate 13 Animals");  // Update the objective progress
        }

        // Destroy the object after death
        Debug.Log("Object destroyed!");
        Destroy(gameObject);
    }
}
