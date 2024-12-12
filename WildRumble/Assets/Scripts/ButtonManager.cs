using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

// Created by Darcy
public class ButtonManager : MonoBehaviour
{
    public GameObject optionsMenuUI;
    public Slider sfxVolumeSlider;
    public Slider bgmVolumeSlider;
    public Slider gunShotSoundSlider;
    public Slider mouseSensitivitySlider;
    public btnFX buttonFXScript;
    public AudioSource bgmAudioSource;
    public AudioSource combatMusicAudioSource;
    public CameraMovement cameraMovementScript;

    public SceneLoader sceneLoader;
    public HealthBar healthBar;
    public GameObject combatModeObject;

    private bool isInCombat = false;
    private float combatExitTimer = 0f;
    private const float combatExitDelay = 2f;

    void Start()
    {
        gunShotSoundSlider.value = PlayerPrefs.GetFloat("GunShotVolume", 1f);
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);
        InitializeVolumeSliders();

        if (combatMusicAudioSource != null)
        {
            combatMusicAudioSource.volume = bgmVolumeSlider.value;
        }

        if (mouseSensitivitySlider != null && cameraMovementScript != null)
        {
            mouseSensitivitySlider.value = PlayerPrefs.GetFloat("MouseSensitivity", 1f);
            SetMouseSensitivity(mouseSensitivitySlider.value);
            mouseSensitivitySlider.onValueChanged.AddListener(SetMouseSensitivity);
        }
    }

    private void SetMouseSensitivity(float sensitivity)
    {
        if (cameraMovementScript != null)
        {
            float sensitivityMultiplier = 5f;
            cameraMovementScript.UpdateSensitivity(sensitivity * sensitivityMultiplier, sensitivity * sensitivityMultiplier);
        }
        PlayerPrefs.SetFloat("MouseSensitivity", sensitivity);
    }

    void Update()
    {
        bool enemyInRange = CheckForEnemyInRange();

        if (enemyInRange)
        {
            if (!isInCombat)
            {
                isInCombat = true;
                StartCombatMusic();
            }
            combatExitTimer = 0f;
        }
        else if (isInCombat)
        {
            combatExitTimer += Time.deltaTime;

            if (combatExitTimer >= combatExitDelay)
            {
                isInCombat = false;
                StopCombatMusic();
            }
        }
    }

    private void StartCombatMusic()
    {
        if (bgmAudioSource != null)
        {
            StartCoroutine(FadeOut(bgmAudioSource, 1f));
        }
        if (combatMusicAudioSource != null && !combatMusicAudioSource.isPlaying)
        {
            combatMusicAudioSource.Play();
            StartCoroutine(FadeIn(combatMusicAudioSource, 1f));
        }
    }

    private void StopCombatMusic()
    {
        if (combatMusicAudioSource != null && combatMusicAudioSource.isPlaying)
        {
            StartCoroutine(FadeOut(combatMusicAudioSource, 1f, () => combatMusicAudioSource.Stop()));
        }
        if (bgmAudioSource != null)
        {
            StartCoroutine(FadeIn(bgmAudioSource, 1f));
        }
    }

    bool CheckForEnemyInRange()
    {
        Collider[] hitColliders = Physics.OverlapSphere(combatModeObject.transform.position, 10f);
        foreach (var collider in hitColliders)
        {
            if (collider.GetComponent<EnemyAI>() != null || collider.GetComponent<Enemy>() != null)
            {
                return true;
            }
        }
        return false;
    }

    public void InitializeVolumeSliders()
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
            healthBar.combatMusicAudioSource = combatMusicAudioSource;
        }
        if (gunShotSoundSlider != null)
        {
            gunShotSoundSlider.value = PlayerPrefs.GetFloat("GunShotVolume", 1f);
            gunShotSoundSlider.onValueChanged.AddListener(SetGunShotVolume);
        }

        if (sfxVolumeSlider != null)
        {
            sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);
            sfxVolumeSlider.onValueChanged.AddListener(SetButtonFXVolume);
        }
    }

    public void SetButtonFXVolume(float volume)
    {
        buttonFXScript.SetVolume(volume);
        PlayerPrefs.SetFloat("ButtonFXVolume", volume);
        PlayerPrefs.SetFloat("SFXVolume", volume);
        PlayerPrefs.Save();

        RaycastRifle rifle = FindObjectOfType<RaycastRifle>();
        if (rifle != null)
        {
            rifle.AmmoPickupAudio.volume = volume;
        }
    }

    public void SetBGMVolume(float volume)
    {
        if (bgmAudioSource != null)
        {
            bgmAudioSource.volume = volume;
        }
        if (combatMusicAudioSource != null)
        {
            combatMusicAudioSource.volume = volume;
        }
        PlayerPrefs.SetFloat("BGMVolume", volume);
    }

    public void SetGunShotVolume(float volume)
    {
        
        PlayerPrefs.SetFloat("GunShotVolume", volume);
        PlayerPrefs.Save();

        
        Weapon[] weapons = FindObjectsOfType<Weapon>();
        foreach (Weapon weapon in weapons)
        {
            weapon.UpdateGunShotVolume(volume);  
        }

        
        RaycastRifle rifle = FindObjectOfType<RaycastRifle>();
        if (rifle != null)
        {
            rifle.ApplyGunShotVolume(volume);  
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

    public void OptionsMenu()
    {
        optionsMenuUI.SetActive(true);
    }

    public void CloseOptionsMenu()
    {
        optionsMenuUI.SetActive(false);
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("No next level found in build settings.");
        }
    }

    private IEnumerator FadeIn(AudioSource audioSource, float duration)
    {
        float initialVolume = 0f;
        float targetVolume = bgmVolumeSlider.value;

        float fadeMultiplier = initialVolume;
        while (fadeMultiplier < 1f)
        {
            fadeMultiplier += Time.deltaTime / duration;
            audioSource.volume = targetVolume * fadeMultiplier;
            yield return null;
        }

        audioSource.volume = targetVolume;
    }

    private IEnumerator FadeOut(AudioSource audioSource, float duration, System.Action onComplete = null)
    {
        float targetVolume = bgmVolumeSlider.value;

        float fadeMultiplier = 1f;
        while (fadeMultiplier > 0f)
        {
            fadeMultiplier -= Time.deltaTime / duration;
            audioSource.volume = targetVolume * fadeMultiplier;
            yield return null;
        }

        audioSource.volume = 0f;
        onComplete?.Invoke();
    }
}
