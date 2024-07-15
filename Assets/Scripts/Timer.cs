using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float respawnTime = 30f; // Tempo di respawn in secondi
    public TextMeshProUGUI timerText; // Testo del timer

    private float timer; // Timer di respawn

    void Start()
    {
        timer = respawnTime;
        UpdateTimerText();
    }

    void Update()
    {
        timer -= Time.deltaTime; // Sottrai il tempo trascorso dal timer

        if (timer <= 0f)
        {
            ReloadCurrentScene(); // Chiamata alla funzione per ricaricare la scena corrente
        }

        UpdateTimerText(); // Aggiorna il testo del timer ogni frame
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
}
