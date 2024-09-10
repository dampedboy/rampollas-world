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
    private ObjAbsorber objAbsorberScript; // Riferimento allo script ObjAbsorber

    // Variabili per il suono di navigazione e apertura del menu
    public AudioClip navigationSound;
    public AudioClip menuOpenSound; // Nuovo suono per l'apertura del menu
    private AudioSource audioSource;

    private bool inputReleased = true; // Per gestire il rilascio del tasto

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        UpdateButtonColors();
        IsPaused = false;

        // Trova e assegna lo script ObjAbsorber
        objAbsorberScript = FindObjectOfType<ObjAbsorber>();
        if (objAbsorberScript == null)
        {
            Debug.LogError("ObjAbsorber script not found in the scene.");
        }

        // Crea un AudioSource dinamicamente se non esiste già
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown("joystick button 7"))
        {
            if (IsPaused)
            {
                PlayMenuOpenSound();
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
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Verifica se il tasto su/giù o W/S è stato rilasciato
        if ((Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W) ||
             Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S)) ||
             (verticalInput == 0 && !inputReleased))
        {
            inputReleased = true; // Tasto rilasciato, pronto per la nuova navigazione
        }

        bool moved = false;

        // Navigazione verso l'alto
        if (inputReleased && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || verticalInput > 0.5f))
        {
            currentButtonIndex = (currentButtonIndex - 1 + buttons.Count) % buttons.Count;
            moved = true;
            inputReleased = false; // Blocca la navigazione fino al rilascio del tasto
        }
        // Navigazione verso il basso
        else if (inputReleased && (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || verticalInput < -0.5f))
        {
            currentButtonIndex = (currentButtonIndex + 1) % buttons.Count;
            moved = true;
            inputReleased = false; // Blocca la navigazione fino al rilascio del tasto
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
        if (objAbsorberScript != null)
        {
            objAbsorberScript.enabled = false; // Disabilita lo script ObjAbsorber
        }

        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
        currentButtonIndex = 0;  // Imposta il primo bottone come evidenziato
        UpdateButtonColors();

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
        }
    }

    public void GoToMainMenu()
    {
        if (IsPaused)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("MainMenu");
            IsPaused = false;
        }
    }

    public void Quit()
    {
        if (IsPaused)
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
            SceneManager.LoadScene("Hub_Training");
            IsPaused = false;
        }
    }
}
