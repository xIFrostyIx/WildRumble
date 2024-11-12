using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public GameObject optionsMenuUI;
    public Slider sfxVolumeSlider;
    public Slider bgmVolumeSlider;
    public Slider gunShotSoundSlider;
    public btnFX buttonFXScript;
    public AudioSource bgmAudioSource;
    public AudioSource combatMusicAudioSource; // Added by Darcy: Combat music audio source

    public SceneLoader sceneLoader;
    public HealthBar healthBar;
    public GameObject combatModeObject; // Added by Darcy: Reference to the CombatMode object

    private bool isInCombat = false; // Track if in combat
    private float combatExitTimer = 0f; // Timer to delay combat music stop
    private const float combatExitDelay = 2f; // 2-second delay before stopping combat music

    void Start()
    {
        // Initialize volume sliders
        InitializeVolumeSliders();

        // Set initial combat music volume to match BGM slider
        if (combatMusicAudioSource != null)
        {
            combatMusicAudioSource.volume = bgmVolumeSlider.value;
        }
    }

    void Update()
    {
        // Check if there are any enemies within range of the CombatMode object
        bool enemyInRange = CheckForEnemyInRange();

        // Start combat music if enemies are in range
        if (enemyInRange)
        {
            if (!isInCombat)
            {
                isInCombat = true;
                StartCombatMusic();
            }
            combatExitTimer = 0f; // Reset timer as enemies are in range
        }
        else if (isInCombat)
        {
            // Increment timer if no enemies are in range
            combatExitTimer += Time.deltaTime;

            // Stop combat music after delay
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
            bgmAudioSource.Pause();
        }
        if (combatMusicAudioSource != null && !combatMusicAudioSource.isPlaying)
        {
            combatMusicAudioSource.Play();
        }
    }

    private void StopCombatMusic()
    {
        if (combatMusicAudioSource != null && combatMusicAudioSource.isPlaying)
        {
            combatMusicAudioSource.Stop();
        }
        if (bgmAudioSource != null)
        {
            bgmAudioSource.UnPause();
        }
    }

    bool CheckForEnemyInRange()
    {
        Collider[] hitColliders = Physics.OverlapSphere(combatModeObject.transform.position, 10f);
        foreach (var collider in hitColliders)
        {
            if (collider.GetComponent<EnemyAI>() != null)
            {
                return true; // Found an enemy within range
            }
        }
        return false; // No enemies within range
    }

    private void InitializeVolumeSliders()
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
            healthBar.combatMusicAudioSource = combatMusicAudioSource; // Pass the combat audio source
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
        if (combatMusicAudioSource != null) // Added by Darcy: Adjust combat music volume as well
        {
            combatMusicAudioSource.volume = volume;
        }
        PlayerPrefs.SetFloat("BGMVolume", volume);
    }

    public void SetGunShotVolume(float volume)
    {
        Weapon weaponScript = FindObjectOfType<Weapon>();
        if (weaponScript != null)
        {
            weaponScript.gunShotVolume = volume; // Set the gunshot volume
            PlayerPrefs.SetFloat("GunShotVolume", volume);
        }

        if (healthBar != null)
        {
            healthBar.SetDamageAndLoseVolume(volume);
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
        Application.OpenURL("https://docs.google.com/forms/d/e/1FAIpQLScRfhtPjWDwGOMTjS4z-ZSTv0WlvOKasJFr4TOSuq6kP4tpzA/viewform");
    }

    public void OptionsMenu()
    {
        optionsMenuUI.SetActive(true);
    }

    public void CloseOptionsMenu()
    {
        optionsMenuUI.SetActive(false);
    }

    // New load level function
    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    // New function to load the next level by build index
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
}
