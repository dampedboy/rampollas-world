using UnityEngine;

public class BlueButtonController : MonoBehaviour
{
    public Animator buttonAnimator; // Riferimento all'animator del bottone blu
    public GameObject keyObject; // Riferimento all'oggetto chiave

    private bool isButtonPressed = false; // Flag per controllare se il bottone è stato premuto

    void Start()
    {
        keyObject.SetActive(false); // Nasconde inizialmente l'oggetto chiave
    }

    void OnTriggerEnter(Collider other)
    {
        // Controlla se il player ha toccato il bottone
        if (other.CompareTag("Player") && !isButtonPressed)
        {
            isButtonPressed = true;
            buttonAnimator.SetTrigger("Press"); // Attiva l'animazione del bottone
            keyObject.SetActive(true); // Rende visibile l'oggetto chiave
        }
    }
}