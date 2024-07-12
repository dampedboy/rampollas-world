using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int startingLives = 3;       // Vite iniziali
    public int maxLives = 6;            // Massimo numero di vite consentito
    static public int currentLives;     // Vite attuali del giocatore
    static private bool isInitialized = false; // Variabile per verificare l'inizializzazione
    public Transform respawnPoint;      // Punto di respawn del giocatore
    public GameObject[] hearts;         // Array di oggetti cuore nell'UI
    public TMP_Text gameOverText;       // Testo di game over nell'UI
    public float fallThreshold = -10f;  // Soglia di caduta per il respawn

    private void Start()
    {
        if (!isInitialized)
        {
            // Inizializza currentLives con il valore delle PlayerPrefs, default a startingLives
            currentLives = PlayerPrefs.GetInt("PlayerLives", startingLives);
            currentLives = Mathf.Clamp(currentLives, startingLives, maxLives);
            isInitialized = true;
        }

        // Aggiorna l'UI dei cuori
        UpdateHearts();

        // Nascondi il testo di game over all'avvio
        gameOverText.gameObject.SetActive(false);
    }

    private void Update()
    {
        // Se il giocatore cade sotto la soglia di caduta, esegui il respawn
        if (transform.position.y < fallThreshold)
        {
            FallRespawn();
        }
    }

    private void UpdateHearts()
    {
        // Attiva o disattiva gli oggetti cuore in base alle vite attuali del giocatore
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].SetActive(i < currentLives);
        }
    }

    public void TakeDamage()
    {
        // Sottrai una vita al giocatore
        currentLives--;

        // Salva il numero di vite rimanenti nelle PlayerPrefs
        PlayerPrefs.SetInt("PlayerLives", currentLives);

        // Aggiorna l'UI dei cuori
        UpdateHearts();

        // Se il giocatore è senza vite, esegui il game over, altrimenti respawn
        if (currentLives <= 0)
        {
            GameOver();
        }
        else
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        // Posiziona il giocatore nel punto di respawn
        transform.position = respawnPoint.position;
    }

    private void FallRespawn()
    {
        // Esegui il respawn dopo una caduta
        currentLives--;
        PlayerPrefs.SetInt("PlayerLives", currentLives);
        UpdateHearts();

        // Se il giocatore è senza vite, esegui il game over, altrimenti respawn
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
        // Mostra il testo di game over
        gameOverText.gameObject.SetActive(true);
        // Eventuali altre logiche di game over qui (blocco input, animazioni, etc.)
    }

    private void OnTriggerEnter(Collider other)
    {
        // Se il giocatore entra in collisione con un proiettile o un dinamite, subisce danni
        if (other.CompareTag("Projectile") || other.CompareTag("Dynamite"))
        {
            TakeDamage();
        }
    }

    private void OnApplicationQuit()
    {
        // Rimuovi il salvataggio delle vite quando l'applicazione viene chiusa
        PlayerPrefs.DeleteKey("PlayerLives");
    }

    public void AddHeart()
    {
        if (CoinManager.CoinCount >= 5)
        {
            currentLives++;
            PlayerPrefs.SetInt("PlayerLives", currentLives);
            UpdateHearts();
            Debug.Log(currentLives);
        }


    }
}





