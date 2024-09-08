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

    // Variabili per il suono di navigazione e apertura del menu
    public AudioClip navigationSound;
    public AudioClip menuOpenSound; // Nuovo suono per l'apertura del menu
    private AudioSource audioSource;

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

        // Crea un AudioSource dinamicamente se non esiste già
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown("joystick button 7"))
        {
            if (IsPaused)
            {
                ResumeGame();
                PlayMenuOpenSound();
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
        bool moved = false;

        // Spostamento su o giù nella lista di bottoni con frecce o W/S
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetAxis("Vertical") > 0)
        {
            currentButtonIndex = (currentButtonIndex - 1 + buttons.Count) % buttons.Count;
            moved = true;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || Input.GetAxis("Vertical") < 0)
        {
            currentButtonIndex = (currentButtonIndex + 1) % buttons.Count;
            moved = true;
        }

        if (moved)
        {
            // Riproduci il suono quando il giocatore si sposta tra i bottoni
            PlayNavigationSound();
            UpdateButtonColors();
        }
    }

    private void PlayNavigationSound()
    {
        if (audioSource != null && navigationSound != null)
        {
            audioSource.PlayOneShot(navigationSound);
        }
    }

    private void HandleButtonSelection()
    {
        // Selezione del bottone con il tasto "O" o "Fire3" solo se il gioco è in pausa
        if (IsPaused && (Input.GetKeyDown(KeyCode.O) || Input.GetButtonDown("Fire3")))
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

        // Riproduci il suono di apertura del menu
        PlayMenuOpenSound();
    }

    private void PlayMenuOpenSound()
    {
        if (audioSource != null && menuOpenSound != null)
        {
            audioSource.PlayOneShot(menuOpenSound);
        }
    }

    public void ResumeGame()
    {
        if (IsPaused)
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
    }

    public void GoToMainMenu()
    {
        if (IsPaused)  // Controlla se il gioco è in pausa
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("MainMenu");
            IsPaused = false;
        }
    }

    public void Quit()
    {
        if (IsPaused)  // Controlla se il gioco è in pausa
        {
            Time.timeScale = 1f;
            Application.Quit();
            IsPaused = false;
        }
    }

    public void Hub()
    {
        if (IsPaused)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Hub");
            IsPaused = false;
        }
    }
}
