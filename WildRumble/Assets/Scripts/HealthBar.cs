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
    public AudioClip losingMusic;
    public AudioSource bgmAudioSource;
    public AudioSource combatMusicAudioSource; // Reference to combat music audio source

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
                HandleGameOver();
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

    private void HandleGameOver()
    {
        StopAllMusic();
        PlayLosingMusic();
        ShowLosePanel();
    }

    private void StopAllMusic()
    {
        if (bgmAudioSource != null)
        {
            bgmAudioSource.Stop();
        }
        if (combatMusicAudioSource != null)
        {
            combatMusicAudioSource.Stop();
        }
    }

    private void PlayLosingMusic()
    {
        if (bgmAudioSource != null && losingMusic != null)
        {
            bgmAudioSource.clip = losingMusic;
            bgmAudioSource.volume = PlayerPrefs.GetFloat("BGMVolume", 1f);
            bgmAudioSource.Play();
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
    }

    public void SetDamageAndLoseVolume(float volume)
    {
        damageVolume = volume;
        loseVolume = volume;
        pickupVolume = volume;
    }
}
