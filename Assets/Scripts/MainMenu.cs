using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    public List<Button> buttons;  // Lista dei bottoni configurabile nell'inspector
    public Color highlightedColor = Color.black;  // Colore per il bottone evidenziato
    public Color normalColor = Color.gray;  // Colore per i bottoni non evidenziati

    private int currentButtonIndex = 0;
    private bool inputReleased = true; // Per gestire il rilascio del tasto
    private bool navigationEnabled = true; // Nuova variabile per abilitare/disabilitare la navigazione

    // Variabili per il suono di navigazione
    public AudioClip navigationSound;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        UpdateButtonColors();

        // Crea un AudioSource dinamicamente se non esiste gi�
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (navigationEnabled)
        {
            HandleNavigation();
        }
        HandleButtonSelection();
    }

    private void HandleNavigation()
    {
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Se il tasto su/gi� � stato rilasciato (freccia su/gi�, W/S o joystick verticale)
        if ((Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W) ||
             Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S)) ||
             (verticalInput == 0 && !inputReleased))
        {
            inputReleased = true; // Il tasto � stato rilasciato, pronto per una nuova navigazione
        }

        bool moved = false; // Variabile per rilevare se ci si � mossi

        // Navigazione verso l'alto
        if (inputReleased && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetAxis("Vertical") > 0))
        {
            currentButtonIndex = (currentButtonIndex - 1 + buttons.Count) % buttons.Count;
            moved = true;
            inputReleased = false; // Impedisce la navigazione finch� il tasto non viene rilasciato
        }
        // Navigazione verso il basso
        else if (inputReleased && (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || Input.GetAxis("Vertical") < 0))
        {
            currentButtonIndex = (currentButtonIndex + 1) % buttons.Count;
            moved = true;
            inputReleased = false; // Impedisce la navigazione finch� il tasto non viene rilasciato
        }

        if (moved)
        {
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
        // Selezione del bottone con il tasto "O" o "Fire2"
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

    public void Play()
    {
        StartCoroutine(LoadHub());
    }

    public void Quit()
    {
        Application.Quit();
    }

    IEnumerator LoadHub()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene("Hub");
    }

    // Funzione per disabilitare la navigazione
    public void DisableNavigation()
    {
        navigationEnabled = false;
    }

    // Funzione per abilitare la navigazione
    public void EnableNavigation()
    {
        navigationEnabled = true;
    }
}
