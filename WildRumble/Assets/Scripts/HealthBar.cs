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

    public AudioClip damageSound;
    public AudioClip loseSound;
    public AudioClip pickupSound; 
    private AudioSource audioSource;
    public float damageVolume = 1f;
    public float loseVolume = 1f;
    public float pickupVolume = 1f; 

    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;

        losePanel.SetActive(false);

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
    //Added by Darcy
    public void TakeDamage(int damage)
    {
        if (!isInvulnerable)
        {
            currentHealth -= damage;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            healthSlider.value = currentHealth;

            if (currentHealth <= 0)
            {
                PlayLoseSound();
                ShowLosePanel();
            }
            else
            {
                PlayDamageSound();
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

    void PlayDamageSound()
    {
        if (damageSound != null)
        {
            audioSource.PlayOneShot(damageSound, damageVolume);
        }
    }

    void PlayLoseSound()
    {
        if (loseSound != null)
        {
            audioSource.PlayOneShot(loseSound, loseVolume);
        }
    }

    void PlayPickupSound() 
    {
        if (pickupSound != null)
        {
            audioSource.PlayOneShot(pickupSound, pickupVolume);
        }
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthSlider.value = currentHealth;

        PlayPickupSound(); 
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(damageAmount);
        }

        if (other.gameObject.CompareTag("HealthPickup"))
        {
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

    public void SetDamageAndLoseVolume(float volume)
    {
        damageVolume = volume;
        loseVolume = volume;
        pickupVolume = volume; 
    }
}
