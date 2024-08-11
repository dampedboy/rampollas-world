using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;


public class TimerProve : MonoBehaviour
{
    public float totalTime = 150f; // 2 minuti e 30 secondi in secondi
    public TextMeshProUGUI gameOverText; // Riferimento al testo "Game Over"
    public TextMeshProUGUI timerText; // Riferimento al testo "Game Over"

    public string hubSceneName = "Hub"; // Nome della scena Hub

    private bool timerRunning = true;
    private string[] sceneNames = { "Prima prova", "Seconda prova", "Terza prova", "Quarta prova", "Quinta prova" };

    void Start()
    {
        DontDestroyOnLoad(gameObject); // Mantiene questo oggetto tra le scene
        gameOverText.gameObject.SetActive(false); // Nasconde il testo "Game Over" all'inizio
    }

    void Update()
    {
        // Controlla se il tasto "E" è stato premuto
        if (Input.GetKeyDown(KeyCode.E))
        {
            LoadNextLevel();
        }

        if (timerRunning)
        {
            UpdateTimerText();

            totalTime -= Time.deltaTime;

            if (totalTime <= 0)
            {
                TimerEnded();
            }
        }
    }

    void TimerEnded()
    {
        timerRunning = false;
        gameOverText.gameObject.SetActive(true); // Mostra il testo "Game Over"
        StartCoroutine(ReturnToHub());
    }

    private IEnumerator ReturnToHub()
    {
        yield return new WaitForSeconds(2f); // Aspetta 2 secondi per mostrare "Game Over"
        SceneManager.LoadScene(hubSceneName); // Carica la scena "Hub"
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (System.Array.Exists(sceneNames, element => element == scene.name))
        {
            gameOverText = FindObjectOfType<TextMeshProUGUI>(); // Trova il nuovo testo "Game Over" nella nuova scena
            gameOverText.gameObject.SetActive(false); // Nasconde di nuovo il testo "Game Over"
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // Aggiunge il listener per il caricamento delle scene
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Rimuove il listener per il caricamento delle scene
    }

    void UpdateTimerText()
    {
        int seconds = Mathf.CeilToInt(timerRunning);
        timerText.text = seconds.ToString(); // Aggiorna il testo del timer con i secondi rimanenti
    }

    private void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        int nextSceneIndex = currentSceneIndex;

        if (currentSceneIndex == 24)
        {
            SceneManager.LoadScene(1);
        }

        if (currentSceneIndex == 1)
        {
            nextSceneIndex = currentSceneIndex + 1;
        }
        else
        {
            nextSceneIndex = currentSceneIndex + 1;
        }

        if (nextSceneIndex < 25)
        {
            SceneManager.LoadScene(nextSceneIndex);

            SceneManager.LoadScene(nextSceneIndex);


        }
        else
        {
            Debug.Log("Hai completato tutti i livelli!");
        }
    }

}