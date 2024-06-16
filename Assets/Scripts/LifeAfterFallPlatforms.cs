using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LifeAfterFallPlatforms : MonoBehaviour
{
    public int maxLives = 3;
    public GameObject[] lifeModels; // Array di modelli 3D delle vite
    public TextMeshProUGUI gameOverText; // Text Mesh Pro per la scritta Game Over
    public float gameOverDelay = 3f; // Tempo di ritardo prima di tornare alla scena dell'hub centrale
    public Transform respawnPoint; // Punto di respawn del giocatore

    private int currentLives;
    private bool isFalling; // Flag per controllare la caduta

    void Start()
    {
        currentLives = maxLives;

        // Controlla se i riferimenti sono assegnati correttamente
        if (lifeModels == null || lifeModels.Length != maxLives || respawnPoint == null || gameOverText == null)
        {
            Debug.LogError("Uno o più riferimenti non sono assegnati correttamente!");
            return;
        }

        // Attiva i modelli delle vite iniziali
        for (int i = 0; i < maxLives; i++)
        {
            lifeModels[i].SetActive(true);
        }

        gameOverText.gameObject.SetActive(false); // Nascondi il testo Game Over inizialmente
        isFalling = false; // Inizialmente il giocatore non è in caduta
    }

    // Funzione per gestire la perdita di una vita
    public void LoseLife()
    {
        if (currentLives > 0)
        {
            currentLives--;
            lifeModels[currentLives].SetActive(false); // Disattiva il modello della vita persa

            if (currentLives == 0)
            {
                // Game Over
                StartCoroutine(GameOverRoutine());
            }
            else
            {
                // Respawn
                RespawnPlayer();
            }
        }
    }

    // Routine per gestire il Game Over
    private IEnumerator GameOverRoutine()
    {
        gameOverText.gameObject.SetActive(true); // Mostra il testo Game Over
        yield return new WaitForSeconds(gameOverDelay); // Attendi qualche secondo
        if (SceneExists("Hub"))
        {
            SceneManager.LoadScene("Hub"); // Carica la scena dell'hub centrale
        }
        else
        {
            Debug.LogError("La scena 'Hub Centrale' non esiste o non è stata aggiunta alle build settings!");
        }
    }

    // Funzione per verificare se una scena esiste nelle build settings
    private bool SceneExists(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string path = SceneUtility.GetScenePathByBuildIndex(i);
            string scene = System.IO.Path.GetFileNameWithoutExtension(path);
            if (scene == sceneName)
            {
                return true;
            }
        }
        return false;
    }

    // Funzione per respawnare il giocatore
    private void RespawnPlayer()
    {
        transform.position = respawnPoint.position;
        isFalling = false; // Resetta il flag di caduta
    }

    // Esempio di chiamata a LoseLife() quando il giocatore cade
    void Update()
    {
        if (transform.position.y < -10 && !isFalling) // Supponendo che il giocatore cada sotto una certa altezza
        {
            isFalling = true; // Imposta il flag di caduta
            LoseLife();
        }
    }
}