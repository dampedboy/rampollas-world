using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int startingLives = 3;
    int maxLives = 6;
    static public int currentLives;
    static private bool isInitialized = false;
    public Transform respawnPoint;
    public GameObject gameOverCanvas;
    public float fallThreshold = -10f;
    private bool hasRespawned = false;

    // Immagini per le vite
    public GameObject cuore1;
    public GameObject cuore2;
    public GameObject cuore3;
    public GameObject cuore4;
    public GameObject cuore5;
    public GameObject cuore6;

    // Audio variables
    public AudioClip damageSound;
    public AudioClip gameOverSound;
    private AudioSource audioSource;
    private bool hasPlayedGameOverSound = false;

    private void Start()
    {
        if (!isInitialized)
        {
            currentLives = PlayerPrefs.GetInt("PlayerLives", startingLives);
            currentLives = Mathf.Clamp(currentLives, startingLives, maxLives);
            isInitialized = true;
        }

        UpdateHeartsDisplay();

        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(false); // Disattiva il Canvas all'inizio
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

    private void UpdateHeartsDisplay()
    {
        // Disattiva tutte le immagini
        cuore1.SetActive(false);
        cuore2.SetActive(false);
        cuore3.SetActive(false);
        cuore4.SetActive(false);
        cuore5.SetActive(false);
        cuore6.SetActive(false);

        // Attiva l'immagine corrispondente al numero di vite
        switch (currentLives)
        {
            case 1:
                cuore1.SetActive(true);
                break;
            case 2:
                cuore2.SetActive(true);
                break;
            case 3:
                cuore3.SetActive(true);
                break;
            case 4:
                cuore4.SetActive(true);
                break;
            case 5:
                cuore5.SetActive(true);
                break;
            case 6:
                cuore6.SetActive(true);
                break;
        }
    }

    public void TakeDamage()
    {
        currentLives--;
        PlayerPrefs.SetInt("PlayerLives", currentLives);
        UpdateHeartsDisplay();
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
        // Imposta il flag di respawn
        CoinManager.isRespawning = true;

        // Play damage sound
        if (damageSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(damageSound);
        }
        currentLives--;
        PlayerPrefs.SetInt("PlayerLives", currentLives);
        UpdateHeartsDisplay();
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
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(true); // Attiva il Canvas di game over
            // Play game over sound
            if (gameOverSound != null && audioSource != null && !hasPlayedGameOverSound)
            {
                audioSource.PlayOneShot(gameOverSound);
                hasPlayedGameOverSound = true; // Segna il suono come già riprodotto
            }

            // Blocca il gioco
            Time.timeScale = 0f;
        }

        StartCoroutine(HandleGameOver()); // Avvia la coroutine per gestire il game over
    }

    private IEnumerator HandleGameOver()
    {
        yield return new WaitForSecondsRealtime(2f); // Usa WaitForSecondsRealtime per aspettare 2 secondi reali

        // Resetta le vite e aggiorna l'interfaccia
        currentLives = startingLives;
        PlayerPrefs.SetInt("PlayerLives", currentLives);
        UpdateHeartsDisplay();

        // Resetta la variabile del suono di game over
        hasPlayedGameOverSound = false;

        // Riavvia il gioco
        Time.timeScale = 1f;

        // Carica la scena di gioco (0 rappresenta la scena principale, modificare se necessario)
        SceneManager.LoadScene(1);
    }

    private void OnTriggerEnter(Collider other)
    {
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
            UpdateHeartsDisplay();
        }
        else
        {
            Debug.Log("Not enough coins to add a heart. CoinManager.CoinCount: " + CoinManager.CoinCount);
        }
    }
}