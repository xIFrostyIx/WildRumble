using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;   // Reference to the UI Slider
    public int maxHealth = 100;   // Maximum health value
    private int currentHealth;     // Current health value

    public int damageAmount = 10;  // Amount of damage taken on collision with an enemy
    public int healAmount = 20;    // Amount of health restored from health pickups

    public GameObject losePanel;   // Reference to the Lose panel

    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;

        Debug.Log("Health initialized: " + currentHealth);
        losePanel.SetActive(false); // Ensure the Lose panel is not active at the start
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("TakeDamage called.");
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Ensure health stays between 0 and maxHealth

        healthSlider.value = currentHealth;
        Debug.Log("Health after damage: " + currentHealth);

        // Check if the player has died
        if (currentHealth <= 0)
        {
            Debug.Log("Player has died.");
            ShowLosePanel(); // Call the method to show the Lose panel
        }
    }

    public void Heal(int healAmount)
    {
        Debug.Log("Heal called.");
        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Ensure health stays between 0 and maxHealth

        healthSlider.value = currentHealth;
        Debug.Log("Health after healing: " + currentHealth);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter called. Collided with: " + other.gameObject.name);

        if (other.gameObject.CompareTag("Enemy")) // Using CompareTag for efficiency
        {
            Debug.Log("Enemy collision detected: " + other.gameObject.name);
            TakeDamage(damageAmount);
        }

        if (other.gameObject.CompareTag("HealthPickup"))
        {
            Debug.Log("Health pickup collision detected: " + other.gameObject.name);
            Heal(healAmount);
            Destroy(other.gameObject);
        }
    }

    private void ShowLosePanel()
    {
        losePanel.SetActive(true); // Activate the Lose panel
        Time.timeScale = 0; // Pause the game

        // Make the cursor visible and unlock it
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
