using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowButtonController : MonoBehaviour
{
    public Animator buttonAnimator; // Riferimento all'animator del bottone blu
    public GameObject portalObject; // Riferimento all'oggetto chiave

    private bool isButtonPressed = false; // Flag per controllare se il bottone è stato premuto

    void Start()
    {
        portalObject.SetActive(false); // Nasconde inizialmente l'oggetto chiave
    }

    void OnTriggerEnter(Collider other)
    {
        // Controlla se il player ha toccato il bottone
        if (other.CompareTag("Player") && !isButtonPressed)
        {
            isButtonPressed = true;
            buttonAnimator.SetTrigger("Press"); // Attiva l'animazione del bottone
            portalObject.SetActive(true); // Rende visibile l'oggetto chiave
        }
    }
}
