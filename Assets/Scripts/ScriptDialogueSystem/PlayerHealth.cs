using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections; // Importa lo spazio dei nomi per le coroutine

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
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(false);
        }

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
        // Play damage sound
        if (damageSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(damageSound);
        }
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

        // Play game over sound
        if (gameOverSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(gameOverSound);
        }

        StartCoroutine(HandleGameOver()); // Avvia la coroutine per gestire il game over
    }

    private IEnumerator HandleGameOver()
    {
        yield return new WaitForSeconds(3f); // Attendi 3 secondi

        // Resetta le vite e aggiorna l'interfaccia
        currentLives = startingLives;
        PlayerPrefs.SetInt("PlayerLives", currentLives);
        UpdateHearts();

        // Carica la scena di gioco (0 rappresenta la scena principale, modificare se necessario)
        SceneManager.LoadScene(0);
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag("Projectile") ||  other.CompareTag("Spuntoni")) 
        if (other.CompareTag("Spuntoni"))
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
        if (CoinManager.CoinCount >= 4)
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

