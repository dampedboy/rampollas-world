using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    public List<Button> buttons;
    private int currentButtonIndex = 0;
    public MainMenu mainMenu;  // Riferimento allo script MainMenu

    // Start is called before the first frame update
    void Start()
    {
        // Assicurati che il riferimento a MainMenu sia assegnato nell'Inspector
    }

    // Update is called once per frame
    void Update()
    {
        // Selezione del bottone con il tasto "O" o "Fire3"
        if (Input.GetKeyDown(KeyCode.O) || Input.GetButtonDown("Fire3"))
        {
            buttons[currentButtonIndex].onClick.Invoke();

            // Riattiva la navigazione nel menu principale
            if (mainMenu != null)
            {
                mainMenu.EnableNavigation();
            }
        }
    }
}
