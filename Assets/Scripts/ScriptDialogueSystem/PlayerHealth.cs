using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int startingLives = 3;
    public int maxLives = 6;
    static public int currentLives;
    static private bool isInitialized = false;
    public Transform respawnPoint;
    public GameObject[] hearts;
    public TMP_Text gameOverText;
    public float fallThreshold = -10f;
    private bool hasRespawned = false;

    // Audio variables
    public AudioClip damageSound;
    public AudioClip gameOverSound;
    private AudioSource audioSource;

    private void Start()
    {
        if (!isInitialized)
        {
            currentLives = PlayerPrefs.GetInt("PlayerLives", startingLives);
            currentLives = Mathf.Clamp(currentLives, startingLives, maxLives);
            isInitialized = true;
        }

        UpdateHearts();
        gameOverText.gameObject.SetActive(false);

        // Initialize audio source
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Update()
    {
        if (transform.position.y < fallThreshold && !hasRespawned)
        {
            FallRespawn();
            hasRespawned = true;
        }
    }

    private void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].SetActive(i < currentLives);
        }
    }

    public void TakeDamage()
    {
        currentLives--;
        PlayerPrefs.SetInt("PlayerLives", currentLives);
        UpdateHearts();
        if (currentLives <= 0)
        {
            GameOver();
        }
        else
        {
            // Play damage sound
            if (damageSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(damageSound);
            }
        }
    }

    private void Respawn()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void FallRespawn()
    {
        currentLives--;
        PlayerPrefs.SetInt("PlayerLives", currentLives);
        UpdateHearts();
        if (currentLives <= 0)
        {
            GameOver();
        }
        else
        {
            Respawn();
        }
    }

    private void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        currentLives = startingLives; // Reset lives
        PlayerPrefs.SetInt("PlayerLives", currentLives);
        UpdateHearts();
        SceneManager.LoadScene(0);

        // Play game over sound
        if (gameOverSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(gameOverSound);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile") || other.CompareTag("Dynamite"))
        {
            TakeDamage();
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("PlayerLives");
    }

    public void AddHeart()
    {
        if (CoinManager.CoinCount >= 5)
        {
            currentLives++;
            PlayerPrefs.SetInt("PlayerLives", currentLives);
            UpdateHearts();
        }
        else
        {
            Debug.Log("Not enough coins to add a heart. CoinManager.CoinCount: " + CoinManager.CoinCount);
        }
    }
}



