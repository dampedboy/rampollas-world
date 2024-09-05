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
    private TMP_Text currentButtonText;

    private bool inputReleased = true; // Stato per controllare se il tasto è stato rilasciato

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
        // Rileva il rilascio del tasto per evitare selezioni multiple
        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow) || Input.GetAxis("Vertical") == 0)
        {
            inputReleased = true; // Rilasciato il tasto, pronto per una nuova selezione
        }

        // Spostamento su o giù nella lista di bottoni solo se il tasto è stato rilasciato
        if (inputReleased)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetAxis("Vertical") > 0)
            {
                currentButtonIndex = (currentButtonIndex - 1 + buttons.Count) % buttons.Count;
                UpdateButtonColors();
                inputReleased = false; // Segna che il tasto è stato premuto
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetAxis("Vertical") < 0)
            {
                currentButtonIndex = (currentButtonIndex + 1) % buttons.Count;
                UpdateButtonColors();
                inputReleased = false; // Segna che il tasto è stato premuto
            }
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
