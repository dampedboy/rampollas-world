using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro; // Importa TextMeshPro

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public static bool IsPaused = false;

    public Button[] menuButtons; // Assegna i bottoni nel menu
    public Color normalColor = Color.gray;
    public Color highlightedColor = Color.black;

    private int currentButtonIndex = 0;
    private float inputCooldown = 0.15f;
    private float lastInputTime;
    private float axisInputCooldown = 0.15f;
    private float lastAxisInputTime;
    private const float deadZone = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        UpdateButtonSelection(); // Evidenzia il primo bottone
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown("joystick button 7"))
        {
            if (IsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        if (IsPaused)
        {
            if (Time.time - lastInputTime >= inputCooldown)
            {
                HandleInput(); // Gestisce la navigazione e la selezione
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
        currentButtonIndex = 0; // Evidenzia il primo bottone quando il gioco � in pausa
        UpdateButtonSelection();
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }

    public void Hub()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Hub");
    }

    private void HandleInput()
    {
        bool inputReceived = false;
        float verticalAxis = Input.GetAxisRaw("Vertical");

        // Gestione asse verticale con dead zone e cooldown
        if (verticalAxis > deadZone && Time.time - lastAxisInputTime >= axisInputCooldown)
        {
            currentButtonIndex = currentButtonIndex - 1;
            inputReceived = true;
            lastAxisInputTime = Time.time;
        }
        else if (verticalAxis < -deadZone && Time.time - lastAxisInputTime >= axisInputCooldown)
        {
            currentButtonIndex = currentButtonIndex + 1;
            inputReceived = true;
            lastAxisInputTime = Time.time;
        }

        // Gestione frecce su/gi�
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentButtonIndex = currentButtonIndex - 1;
            inputReceived = true;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentButtonIndex = currentButtonIndex + 1;
            inputReceived = true;
        }

        // Seleziona il bottone con O o Fire3
        if (Input.GetKeyDown(KeyCode.O) || Input.GetButtonDown("Fire3"))
        {
            menuButtons[currentButtonIndex].onClick.Invoke();
        }

        if (inputReceived)
        {
            lastInputTime = Time.time; // Aggiorna l'input time per evitare input multipli
            UpdateButtonSelection(); // Aggiorna l'evidenziazione
        }
    }

    private void UpdateButtonSelection()
    {
        for (int i = 0; i < menuButtons.Length; i++)
        {
            TMP_Text buttonText = menuButtons[i].GetComponentInChildren<TMP_Text>();
            if (i == currentButtonIndex)
            {
                buttonText.color = highlightedColor; // Cambia il colore in nero quando � selezionato
            }
            else
            {
                buttonText.color = normalColor; // Cambia il colore in grigio se non � selezionato
            }
        }
    }
}

