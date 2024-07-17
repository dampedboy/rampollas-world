using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float respawnTime = 30f; // Tempo di respawn in secondi
    public TextMeshProUGUI timerText; // Testo del timer
    public AudioClip timerSound; // Clip audio per il suono del timer
    public AnimationCurve textScaleCurve; // Curva per l'animazione di scala del testo
    public float textScaleMagnitude = 0.1f; // Magnitudine di scala del testo

    private float timer; // Timer di respawn
    private AudioSource audioSource; // AudioSource per riprodurre il suono
    private bool playedSound = false; // Flag per assicurarsi che il suono parta solo una volta per secondo

    void Start()
    {
        timer = respawnTime;
        UpdateTimerText();

        // Aggiungi un AudioSource dinamicamente se non è già presente
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = timerSound;
        audioSource.loop = false;
    }

    void Update()
    {
        timer -= Time.deltaTime; // Sottrai il tempo trascorso dal timer

        if (timer <= 0f)
        {
            ReloadCurrentScene(); // Chiamata alla funzione per ricaricare la scena corrente
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

    void ReloadCurrentScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName); // Ricarica la scena corrente
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
}
