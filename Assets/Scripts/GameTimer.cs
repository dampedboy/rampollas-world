using UnityEngine;
using TMPro;
using System;

public class GameTimer : MonoBehaviour
{
    public float totalTime = 30f; // Tempo totale del timer in secondi
    private float timeRemaining;
    public GameObject timerTextObject; // GameObject per visualizzare il timer
    private TMP_Text timerText; // Riferimento al componente TMP_Text del GameObject
    public GameObject gameOverUI; // UI per il messaggio di "Game Over"
    public PlayerController playerController; // Riferimento al controller del player
    public Transform respawnPoint; // Punto di respawn nell'hub centrale
    private bool isTimerRunning;

    void Start()
    {
        timeRemaining = totalTime;
        gameOverUI.SetActive(false);
        timerText = timerTextObject.GetComponent<TMP_Text>(); // Ottiene il componente TMP_Text
        isTimerRunning = true;
        UpdateTimerDisplay(); // Inizializza il display del timer
    }

    void Update()
    {
        if (isTimerRunning && timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerDisplay();
        }
        else if (timeRemaining <= 0 && isTimerRunning)
        {
            GameOver();
        }
    }

    void UpdateTimerDisplay()
    {
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = seconds.ToString(); // Mostra solo il numero dei secondi rimanenti
    }

    public void StopTimer()
    {
        isTimerRunning = false;
        timeRemaining = 0;
        UpdateTimerDisplay();
    }

    void GameOver()
    {
        gameOverUI.SetActive(true);
        playerController.enabled = false; // Disabilita i controlli del player
        // Torna all'hub centrale dopo un breve ritardo
        Invoke("RespawnAtHub", 3f);
    }

    void RespawnAtHub()
    {
        playerController.transform.position = respawnPoint.position;
        playerController.transform.rotation = respawnPoint.rotation;
        playerController.enabled = true; // Riabilita i controlli del player
        gameOverUI.SetActive(false);
        timeRemaining = totalTime; // Resetta il timer
        isTimerRunning = true; // Riavvia il timer
        UpdateTimerDisplay(); // Aggiorna il display del timer dopo il reset
    }
}