using UnityEngine;
using UnityEngine.UI;

// Edited by Joshua Guerrer
// Added by Darcy: animal audio functionality

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public Slider healthBar;
    public Canvas healthBarCanvas;

    public ObjectiveManager objectiveManager; // Reference to the ObjectiveManager

    [Header("Audio Settings")]
    public AudioSource animalAudioSource;
    public AudioClip[] animalSounds;
    public float minAnimalSoundInterval = 12f;
    public float maxAnimalSoundInterval = 30f;
    public float soundRange = 25f;
    public GameObject player;

    private bool isDead = false;
    private float timeUntilNextSound = 0f;

    void Start()
    {
        currentHealth = maxHealth;

        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }

        if (player == null)
        {
            Debug.LogError("Player object is not assigned in the Enemy script!");
        }

        if (animalAudioSource != null)
        {
            animalAudioSource.spatialBlend = 1.0f;
        }
    }

    void Update()
    {
        if (!isDead && player != null && Vector3.Distance(transform.position, player.transform.position) <= soundRange)
        {
            if (timeUntilNextSound <= 0f)
            {
                PlayRandomAnimalSound();
                timeUntilNextSound = Random.Range(minAnimalSoundInterval, maxAnimalSoundInterval);
            }
            else
            {
                timeUntilNextSound -= Time.deltaTime;
            }
        }
    }

    public void Damage(int damage)
    {
        if (isDead) return;

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
        if (isDead) return;

        Debug.Log("Die method called!");

        isDead = true;

        Debug.Log($"{gameObject.name} has died!");

        
        if (animalAudioSource != null && animalAudioSource.isPlaying)
        {
            animalAudioSource.Stop();
        }

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

        // Destroy the object after a delay (if you need to give time for any remaining effects)
        Destroy(gameObject);
    }

    void PlayRandomAnimalSound()
    {
        if (animalAudioSource != null && animalSounds.Length > 0 && !animalAudioSource.isPlaying)
        {
            int randomIndex = Random.Range(0, animalSounds.Length);
            animalAudioSource.clip = animalSounds[randomIndex];
            animalAudioSource.Play();
        }
    }
}
