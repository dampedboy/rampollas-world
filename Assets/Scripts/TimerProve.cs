using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class TimerProve : MonoBehaviour
{
    public float respawnTime = 150f; // Timer impostato su 2 minuti e 30 secondi (150 secondi)
    public GameObject gameOverCanvas; // Testo per Game Over
    public AudioClip timerSound; // Clip audio per il suono del timer
    public AnimationCurve textScaleCurve; // Curva per l'animazione di scala del testo
    public float textScaleMagnitude = 0.1f; // Magnitudine di scala del testo

    private float timer; // Timer di respawn
    private AudioSource audioSource; // AudioSource per riprodurre il suono
    private bool playedSound = false; // Flag per assicurarsi che il suono parta solo una volta per secondo
    private bool isGameOver = false; // Flag per verificare se è terminato il tempo
    private TextMeshProUGUI timerText; // Riferimento al TextMeshPro che mostra il timer

    private static TimerProve instance; // Per assicurarsi che ci sia solo un'istanza del timer

    void Awake()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        // Assicurati che ci sia solo un'istanza di TimerProve e che non venga distrutta tra i caricamenti delle scene
        if (instance == null)
        {
            if (IsProvaScene(currentSceneName))
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
                DontDestroyOnLoad(gameOverCanvas);
            }
            else
            {
                Destroy(gameObject); // Distruggi l'oggetto se non siamo in una delle scene di prova
            }
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            Destroy(gameOverCanvas);
        }
    }

    void Start()
    {
        timer = respawnTime;
        FindAndSetTimerText(); // Trova e assegna il riferimento a TimerText
        UpdateTimerText();

        // Aggiungi un AudioSource dinamicamente se non è già presente
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = timerSound;
        audioSource.loop = false;

        // Assicurati che il testo "Game Over" sia nascosto all'inizio
        gameOverCanvas.SetActive(false);
        SceneManager.sceneLoaded += OnSceneLoaded; // Registrati per ricevere eventi di caricamento della scena
    }

    void Update()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        if (!isGameOver && IsProvaScene(currentSceneName))
        {
            timer -= Time.deltaTime; // Sottrai il tempo trascorso dal timer

            // Controlla se il tasto "E" è stato premuto
            if (Input.GetKeyDown(KeyCode.E))
            {
                LoadNextLevel();
            }

            if (timer <= 0f && !playedSound)
            {
                StartCoroutine(ShowGameOverAndReturnToHub());
                playedSound = true;
                isGameOver = true;
            }

            UpdateTimerText(); // Aggiorna il testo del timer ogni frame

            // Suono del timer negli ultimi 5 secondi
            if (timer <= 5f)
            {
                int secondsRemaining = Mathf.CeilToInt(timer);
                if (!playedSound && secondsRemaining <= 5)
                {
                    audioSource.Play();
                    playedSound = true;
                    Invoke("ResetPlayedSound", 1f); // Resetta il flag dopo un secondo
                }
            }
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (IsProvaScene(scene.name))
        {
            FindAndSetTimerText(); // Ricarica il riferimento al TimerText nella nuova scena
        }
    }

    private void FindAndSetTimerText()
    {
        timerText = GameObject.Find("TimerText")?.GetComponent<TextMeshProUGUI>();
        if (timerText == null)
        {
            Debug.LogError("TimerText non trovato nella scena corrente. Assicurati che l'oggetto abbia il nome corretto.");
        }
    }

    IEnumerator ShowGameOverAndReturnToHub()
    {
        // Mostra la scritta "Game Over"
        gameOverCanvas.SetActive(true);

        yield return new WaitForSeconds(2.5f); // Aspetta 5 secondi

        // Torna alla scena "Hub"
        SceneManager.LoadScene("Hub");
        Destroy(gameObject); // Distruggi il timer quando torni alla scena "Hub"
    }

    void UpdateTimerText()
    {
        if (timerText != null)
        {
            int seconds = Mathf.CeilToInt(timer);
            timerText.text = seconds.ToString(); // Aggiorna il testo del timer con i secondi rimanenti
        }
    }

    void ResetPlayedSound()
    {
        playedSound = false;
    }

    private bool IsProvaScene(string sceneName)
    {
        // Controlla se il nome della scena è uno dei nomi delle scene di prova
        return sceneName == "Prima Prova" || sceneName == "Seconda Prova" ||
               sceneName == "Terza Prova" || sceneName == "Quarta Prova" ||
               sceneName == "Quinta Prova";
    }

    private void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        int nextSceneIndex = currentSceneIndex;

        if (currentSceneIndex == 30)
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

        if (nextSceneIndex < 31)
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