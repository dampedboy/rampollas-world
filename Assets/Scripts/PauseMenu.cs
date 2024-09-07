using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public static bool IsPaused = false;
    public GameObject player; // Riferimento al personaggio principale

    public List<Button> buttons;  // Lista dei bottoni configurabile nell'inspector
    public Color highlightedColor = Color.blue;  // Colore per il bottone evidenziato
    public Color normalColor = Color.gray;  // Colore per i bottoni non evidenziati

    private int currentButtonIndex = 0;
    private ObjAbsorber objAbsorberScript; // Riferimento allo script ObjAbsorber

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        UpdateButtonColors();

        // Trova e assegna lo script ObjAbsorber
        objAbsorberScript = FindObjectOfType<ObjAbsorber>();
        if (objAbsorberScript == null)
        {
            Debug.LogError("ObjAbsorber script not found in the scene.");
        }

        // Disabilita l'interazione con i bottoni all'avvio (quando non è in pausa)
        SetButtonsInteractable(false);
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
            HandleNavigation();
            HandleButtonSelection();
        }
    }

    private void HandleNavigation()
    {
        // Spostamento su o giù nella lista di bottoni con frecce o W/S
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetAxis("Vertical") > 0)
        {
            currentButtonIndex = (currentButtonIndex - 1 + buttons.Count) % buttons.Count;
            UpdateButtonColors();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || Input.GetAxis("Vertical") < 0)
        {
            currentButtonIndex = (currentButtonIndex + 1) % buttons.Count;
            UpdateButtonColors();
        }
    }


    private void HandleButtonSelection()
    {
        // Selezione del bottone con il tasto "O" o "Fire3" solo se il gioco è in pausa
        if (IsPaused && (Input.GetKeyDown(KeyCode.P) || Input.GetButtonDown("Fire2")))
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

    // Funzione che abilita/disabilita l'interazione con i bottoni
    private void SetButtonsInteractable(bool interactable)
    {
        foreach (Button button in buttons)
        {
            button.interactable = interactable;
        }
    }

    public void PauseGame()
    {
        player.GetComponent<PlayerMovement>().enabled = false; // Disabilita il movimento del player
        if (objAbsorberScript != null)
        {
            objAbsorberScript.enabled = false; // Disabilita lo script ObjAbsorber
        }

        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
        currentButtonIndex = 0;  // Imposta il primo bottone come evidenziato
        UpdateButtonColors();
        SetButtonsInteractable(true);  // Abilita l'interazione con i bottoni
    }

    public void ResumeGame()
    {
        player.GetComponent<PlayerMovement>().enabled = true; // Riabilita il movimento del player
        if (objAbsorberScript != null)
        {
            objAbsorberScript.enabled = true; // Riabilita lo script ObjAbsorber
        }

        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
        SetButtonsInteractable(false);  // Disabilita l'interazione con i bottoni
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
}
