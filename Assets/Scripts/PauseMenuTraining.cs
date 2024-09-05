using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PauseMenuTraining : MonoBehaviour
{
    public GameObject pauseMenu;
    public static bool IsPaused = false;
    public GameObject player; // Riferimento al personaggio principale

    public List<Button> buttons;  // Lista dei bottoni configurabile nell'inspector
    public Color highlightedColor = Color.blue;  // Colore per il bottone evidenziato
    public Color normalColor = Color.gray;  // Colore per i bottoni non evidenziati

    private int currentButtonIndex = 0;
    private TMP_Text currentButtonText;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        UpdateButtonColors();
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
                Debug.Log("individuato P");
            }
        }

        if (IsPaused)
        {
            HandleNavigation();
            HandleButtonSelection();
        }
    }

    private void HandleNavigation()
    {
        // Spostamento su o giù nella lista di bottoni
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetAxis("Vertical") > 0)
        {
            currentButtonIndex = (currentButtonIndex - 1 + buttons.Count) % buttons.Count;
            UpdateButtonColors();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetAxis("Vertical") < 0)
        {
            currentButtonIndex = (currentButtonIndex + 1) % buttons.Count;
            UpdateButtonColors();
        }
    }

    private void HandleButtonSelection()
    {
        // Selezione del bottone con il tasto "O" o "Fire3"
        if (Input.GetKeyDown(KeyCode.O) || Input.GetButtonDown("Fire3"))
        {
            buttons[currentButtonIndex].onClick.Invoke();
        }
    }

    private void UpdateButtonColors()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            TMP_Text buttonText = buttons[i].GetComponentInChildren<TMP_Text>();

            if (i == currentButtonIndex)
            {
                buttonText.color = highlightedColor;
            }
            else
            {
                buttonText.color = normalColor;
            }
        }
    }

    public void PauseGame()
    {
        player.GetComponent<PlayerMovement>().enabled = false; // Disabilita il movimento del player

        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
        currentButtonIndex = 0;  // Imposta il primo bottone come evidenziato
        UpdateButtonColors();
    }

    public void ResumeGame()
    {
        player.GetComponent<PlayerMovement>().enabled = true; // Riabilita il movimento del player

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
        SceneManager.LoadScene("Hub_Training");
    }
}