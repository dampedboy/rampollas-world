using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float respawnTime = 30f; // Tempo di respawn in secondi
    public TextMeshProUGUI timerText; // Testo del timer
    public TextMeshProUGUI gameOverText; // Testo per la scritta Game Over

    private float timer; // Timer di respawn
    private bool isGameOver = false; // Flag per indicare se il gioco è finito

    void Start()
    {
        timer = respawnTime;
        UpdateTimerText();
        gameOverText.gameObject.SetActive(false); // Nascondi il testo Game Over inizialmente
    }

    void Update()
    {
        if (!isGameOver)
        {
            timer -= Time.deltaTime; // Sottrai il tempo trascorso dal timer

            if (timer <= 0f)
            {
                GameOver(); // Chiamata alla funzione di Game Over quando il timer arriva a zero
            }

            UpdateTimerText(); // Aggiorna il testo del timer ogni frame
        }
    }

    void GameOver()
    {
        isGameOver = true;
        timer = 0f; // Assicurati che il timer sia esattamente zero
        gameOverText.gameObject.SetActive(true); // Mostra il testo Game Over
        Invoke("LoadHubScene", 3f); // Carica la scena Hub Centrale dopo 3 secondi
    }

    void LoadHubScene()
    {
        if (SceneExists("Hub Centrale"))
        {
            SceneManager.LoadScene("Hub Centrale"); // Carica la scena Hub Centrale
        }
        else
        {
            Debug.LogError("La scena 'Hub Centrale' non esiste o non è stata aggiunta alle build settings!");
        }
    }

    void UpdateTimerText()
    {
        int seconds = Mathf.CeilToInt(timer);
        timerText.text = seconds.ToString(); // Aggiorna il testo del timer con i secondi rimanenti
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
}