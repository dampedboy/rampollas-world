using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class TimerProve : MonoBehaviour
{
    public float respawnTime = 150f; // Timer impostato su 2 minuti e 30 secondi (150 secondi)
    public TextMeshProUGUI timerText; // Testo del timer
    public TextMeshProUGUI gameOverText; // Testo per Game Over
    public AudioClip timerSound; // Clip audio per il suono del timer
    public AnimationCurve textScaleCurve; // Curva per l'animazione di scala del testo
    public float textScaleMagnitude = 0.1f; // Magnitudine di scala del testo

    private float timer; // Timer di respawn
    private AudioSource audioSource; // AudioSource per riprodurre il suono
    private bool playedSound = false; // Flag per assicurarsi che il suono parta solo una volta per secondo
    private bool isGameOver = false; // Flag per verificare se è terminato il tempo

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
            }
            else
            {
                Destroy(gameObject); // Distruggi l'oggetto se non siamo in una delle scene di prova
            }
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        timer = respawnTime;
        UpdateTimerText();

        // Aggiungi un AudioSource dinamicamente se non è già presente
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = timerSound;
        audioSource.loop = false;

        // Assicurati che il testo "Game Over" sia nascosto all'inizio
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        // Controlla se il tasto "E" è stato premuto
        if (Input.GetKeyDown(KeyCode.E))
        {
            LoadNextLevel();
        }

        if (!isGameOver && IsProvaScene(currentSceneName))
        {
            timer -= Time.deltaTime; // Sottrai il tempo trascorso dal timer

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

    IEnumerator ShowGameOverAndReturnToHub()
    {
        // Mostra la scritta "Game Over"
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(5f); // Aspetta 5 secondi

        // Torna alla scena "Hub"
        SceneManager.LoadScene("Hub");
        Destroy(gameObject); // Distruggi il timer quando torni alla scena "Hub"
    }

    void UpdateTimerText()
    {
        int seconds = Mathf.CeilToInt(timer);
        timerText.text = seconds.ToString(); // Aggiorna il testo del timer con i secondi rimanenti
    }

    void ResetPlayedSound()
    {
        playedSound = false;
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
        }
        else
        {
            Debug.Log("Hai completato tutti i livelli!");
        }
    }

    private bool IsProvaScene(string sceneName)
    {
        // Controlla se il nome della scena è uno dei nomi delle scene di prova
        return sceneName == "Prima Prova" || sceneName == "Seconda Prova" ||
               sceneName == "Terza Prova" || sceneName == "Quarta Prova" ||
               sceneName == "Quinta Prova";
    }
}