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

    // Start is called before the first frame update
    void Start()
    {
        UpdateButtonColors();
    }

    // Update is called once per frame
    void Update()
    {
        HandleNavigation();
        HandleButtonSelection();
    }

    private void HandleNavigation()
    {
        // Se il tasto su/giù è stato rilasciato (freccia su/giù o W/S o joystick verticale)
        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W) ||
            Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S) ||
            (Input.GetAxisRaw("Vertical") == 0 && !inputReleased))
        {
            inputReleased = true; // Il tasto è stato rilasciato, pronto per una nuova navigazione
        }

        // Navigazione verso l'alto
        if (inputReleased && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetAxisRaw("Vertical") > 0))
        {
            currentButtonIndex = (currentButtonIndex - 1 + buttons.Count) % buttons.Count;
            UpdateButtonColors();
            inputReleased = false; // Impedisce la navigazione finché il tasto non viene rilasciato
        }
        // Navigazione verso il basso
        else if (inputReleased && (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || Input.GetAxisRaw("Vertical") < 0))
        {
            currentButtonIndex = (currentButtonIndex + 1) % buttons.Count;
            UpdateButtonColors();
            inputReleased = false; // Impedisce la navigazione finché il tasto non viene rilasciato
        }
    }


    private void HandleButtonSelection()
    {
        // Selezione del bottone con il tasto "P" o "Fire2"
        if (Input.GetKeyDown(KeyCode.P) || Input.GetButtonDown("Fire1"))
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
}
