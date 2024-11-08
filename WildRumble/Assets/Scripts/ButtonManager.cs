using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
//Created by Darcy
public class ButtonManager : MonoBehaviour
{
    public GameObject optionsMenuUI;
    public Slider sfxVolumeSlider;
    public Slider bgmVolumeSlider;
    public Slider gunShotSoundSlider;
    public btnFX buttonFXScript;
    public AudioSource bgmAudioSource;
    public AudioSource combatAudioSource;

    public float combatRange = 10f;
    public Transform combatModeTrigger;
    public LayerMask enemyLayer;

    public SceneLoader sceneLoader;
    public HealthBar healthBar;

    public AudioClip losingMusic; 

    private bool isInCombat = false; 

    private void Start()
    {
        if (sfxVolumeSlider != null && buttonFXScript != null)
        {
            sfxVolumeSlider.value = PlayerPrefs.GetFloat("ButtonFXVolume", 1f);
            buttonFXScript.SetVolume(sfxVolumeSlider.value);
            sfxVolumeSlider.onValueChanged.AddListener(SetButtonFXVolume);
        }

        if (bgmVolumeSlider != null && bgmAudioSource != null)
        {
            bgmVolumeSlider.value = PlayerPrefs.GetFloat("BGMVolume", 1f);
            bgmAudioSource.volume = bgmVolumeSlider.value;
            combatAudioSource.volume = bgmVolumeSlider.value;
            bgmVolumeSlider.onValueChanged.AddListener(SetBGMVolume);
        }

        if (gunShotSoundSlider != null)
        {
            gunShotSoundSlider.value = PlayerPrefs.GetFloat("GunShotVolume", 1f);
            gunShotSoundSlider.onValueChanged.AddListener(SetGunShotVolume);
        }

        if (healthBar != null)
        {
            healthBar.bgmAudioSource = bgmAudioSource;
        }

        if (combatAudioSource != null)
        {
            combatAudioSource.loop = true;
            combatAudioSource.mute = true; 
        }
    }

    private void Update()
    {
        CheckForCombat();
    }

    private void CheckForCombat()
    {
        Collider[] enemiesInRange = Physics.OverlapSphere(combatModeTrigger.position, combatRange, enemyLayer);

        if (enemiesInRange.Length > 0)
        {
            
            if (!isInCombat)
            {
                isInCombat = true;
                StartCombat();
            }
        }
        else
        {
            
            if (isInCombat)
            {
                StartCoroutine(ExitCombatAfterDelay());
            }
        }
    }

    private void StartCombat()
    {
        if (combatAudioSource != null && !combatAudioSource.isPlaying)
        {
            combatAudioSource.Play();
        }

        combatAudioSource.mute = false;
        bgmAudioSource.mute = true; 
    }

    private IEnumerator ExitCombatAfterDelay()
    {
        
        yield return new WaitForSeconds(3f);

        Collider[] enemiesInRange = Physics.OverlapSphere(combatModeTrigger.position, combatRange, enemyLayer);
        if (enemiesInRange.Length == 0)
        {
            isInCombat = false;
            combatAudioSource.mute = true;
            bgmAudioSource.mute = false; 
        }
    }

    public void SetButtonFXVolume(float volume)
    {
        buttonFXScript.SetVolume(volume);
        PlayerPrefs.SetFloat("ButtonFXVolume", volume);
    }

    public void SetBGMVolume(float volume)
    {
        if (bgmAudioSource != null)
        {
            bgmAudioSource.volume = volume;
        }
        if (combatAudioSource != null)
        {
            combatAudioSource.volume = volume;
        }
        PlayerPrefs.SetFloat("BGMVolume", volume);
    }

    public void SetGunShotVolume(float volume)
    {
        Weapon weaponScript = FindObjectOfType<Weapon>();
        if (weaponScript != null)
        {
            weaponScript.gunShotVolume = volume;
            PlayerPrefs.SetFloat("GunShotVolume", volume);
        }

        if (healthBar != null)
        {
            healthBar.SetDamageAndLoseVolume(volume);
        }
    }

    
    public void ShowLosePanel()
    {
        
        Debug.Log("Game Over! Stopping combat music.");

        
        StopCombatAudio();

        
        MuteBGM();

        
        if (bgmAudioSource != null && losingMusic != null)
        {
            bgmAudioSource.clip = losingMusic;  
            bgmAudioSource.volume = 1f;  
            bgmAudioSource.mute = false; 
            bgmAudioSource.Play(); 
            Debug.Log("Losing music started.");
        }
        else
        {
            Debug.LogWarning("Losing music clip is not assigned!");
        }
    }

    private void StopCombatAudio()
    {
        if (combatAudioSource != null)
        {
            if (combatAudioSource.isPlaying)
            {
                combatAudioSource.Stop();  
                combatAudioSource.mute = true;  
                Debug.Log("Combat music stopped.");
            }
            else
            {
                Debug.Log("Combat audio is already stopped.");
            }
        }
        else
        {
            Debug.LogWarning("combatAudioSource is null.");
        }
    }

    private void MuteBGM()
    {
        if (bgmAudioSource != null)
        {
            if (bgmAudioSource.isPlaying)
            {
                bgmAudioSource.Stop();  
                bgmAudioSource.mute = true;  
                Debug.Log("Background music stopped.");
            }
            else
            {
                Debug.Log("BGM audio is already stopped.");
            }
        }
        else
        {
            Debug.LogWarning("bgmAudioSource is null.");
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        sceneLoader.LoadScene("Menu");
    }

    public void Play()
    {
        sceneLoader.LoadScene("LevelOne");
    }

    public void QuitGame()
    {
        Application.Quit();
        Application.OpenURL("https://docs.google.com/forms/d/e/1FAIpQLSeQ-pE-3Fk9g7x_3E20kTP95STGiwg681mYpJRIM9yRPZ2LJQ/viewform?usp=sf_link");
    }

    public void OpenOptions()
    {
        if (optionsMenuUI != null)
        {
            optionsMenuUI.SetActive(true);
        }
    }

    public void CloseOptions()
    {
        if (optionsMenuUI != null)
        {
            optionsMenuUI.SetActive(false);
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
