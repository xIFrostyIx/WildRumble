using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public int maxHealth = 100;
    private int currentHealth;

    public int damageAmount = 10;
    public int healAmount = 20;

    public GameObject losePanel;
    private bool isInvulnerable = false;  

    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;

        Debug.Log("Health initialized: " + currentHealth);
        losePanel.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        if (!isInvulnerable)  
        {
            Debug.Log("TakeDamage called.");
            currentHealth -= damage;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

            healthSlider.value = currentHealth;
            Debug.Log("Health after damage: " + currentHealth);

            if (currentHealth <= 0)
            {
                Debug.Log("Player has died.");
                ShowLosePanel();
            }
            else
            {
                StartCoroutine(InvulnerabilityPeriod());  
            }
        }
    }

    IEnumerator InvulnerabilityPeriod()
    {
        isInvulnerable = true;  
        yield return new WaitForSeconds(1f);  
        isInvulnerable = false;  
    }

    public void Heal(int healAmount)
    {
        Debug.Log("Heal called.");
        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        healthSlider.value = currentHealth;
        Debug.Log("Health after healing: " + currentHealth);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter called. Collided with: " + other.gameObject.name);

        if (other.gameObject.CompareTag("Enemy"))
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
        losePanel.SetActive(true);
        Time.timeScale = 0;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
