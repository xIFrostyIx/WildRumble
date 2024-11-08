using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour
{
    // Edited by Darcy
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
    public AudioClip losingMusic;
    public AudioSource bgmAudioSource;
    public AudioSource combatAudioSource;

    public float damageVolume = 1f;
    public float loseVolume = 1f;
    public float pickupVolume = 1f;
    public static bool isGameOver = false;

    private CameraMovement cameraMovement;

    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;

        losePanel.SetActive(false);
        cameraMovement = Camera.main.GetComponent<CameraMovement>();
    }

    public void TakeDamage(int damage)
    {
        if (!isInvulnerable)
        {
            currentHealth -= damage;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            healthSlider.value = currentHealth;

            if (currentHealth <= 0)
            {
                StopBGMAndPlayLosingMusic();
                ShowLosePanel();
            }
            else
            {
                PlayDamageSound();
                StartCoroutine(InvulnerabilityPeriod());

                if (cameraMovement != null)
                {
                    StartCoroutine(cameraMovement.CameraShake());
                }
            }
        }
    }

    private void StopBGMAndPlayLosingMusic()
    {
        if (bgmAudioSource != null)
        {
            bgmAudioSource.Stop(); 
            Debug.Log("BGM stopped");

            
            if (losingMusic != null)
            {
                bgmAudioSource.clip = losingMusic; 
                bgmAudioSource.volume = PlayerPrefs.GetFloat("BGMVolume", 1f); 
                bgmAudioSource.mute = false; 
                bgmAudioSource.Play(); 
                Debug.Log("Losing music started");
            }
            else
            {
                Debug.LogWarning("Losing music clip is not assigned!");
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
            AudioSource.PlayClipAtPoint(damageSound, transform.position, damageVolume);
        }
    }

    void PlayLoseSound()
    {
        if (loseSound != null)
        {
            AudioSource.PlayClipAtPoint(loseSound, transform.position, loseVolume);
        }
    }

    void PlayPickupSound()
    {
        if (pickupSound != null)
        {
            AudioSource.PlayClipAtPoint(pickupSound, transform.position, pickupVolume);
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
        Debug.Log("ShowLosePanel called");

        
        losePanel.SetActive(true);
        Time.timeScale = 0; 
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        isGameOver = true;

        
        MuteAllSounds();

        
        PlayLosingMusic();
    }

    private void PlayLosingMusic()
    {
        if (bgmAudioSource != null)
        {
            bgmAudioSource.Stop(); 
            Debug.Log("BGM stopped");

            
            if (losingMusic != null)
            {
                bgmAudioSource.clip = losingMusic; 
                bgmAudioSource.volume = PlayerPrefs.GetFloat("BGMVolume", 1f); 
                bgmAudioSource.mute = false; 
                bgmAudioSource.Play(); 
                Debug.Log("Losing music started");
            }
            else
            {
                Debug.LogWarning("Losing music clip is not assigned!");
            }
        }
    }


    private void MuteAllSounds()
    {
        if (bgmAudioSource != null)
        {
            bgmAudioSource.Stop(); // Stop the BGM
            bgmAudioSource.mute = true; // Mute the BGM
        }

        
        if (combatAudioSource != null)
        {
            combatAudioSource.Stop(); // Stop combat sound
            combatAudioSource.mute = true; // Mute combat sound
        }
    }

    public void SetDamageAndLoseVolume(float volume)
    {
        damageVolume = volume;
        loseVolume = volume;
        pickupVolume = volume;
    }
}
