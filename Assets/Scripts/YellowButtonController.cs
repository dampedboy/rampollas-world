using UnityEngine;

public class YellowButtonController : MonoBehaviour
{
    public Animator buttonAnimator; // Riferimento all'animator del bottone giallo
    public GameObject portalObject; // Riferimento all'oggetto del portale
    public AudioClip buttonPressClip; // Riferimento all'AudioClip per il suono del bottone

    private bool isButtonPressed = false; // Flag per controllare se il bottone è stato premuto

    void Start()
    {
        portalObject.SetActive(false); // Nasconde inizialmente l'oggetto del portale
    }

    void OnTriggerEnter(Collider other)
    {
        // Controlla se il player ha toccato il bottone
        if (other.CompareTag("Player") && !isButtonPressed)
        {
            isButtonPressed = true;
            buttonAnimator.SetTrigger("Press"); // Attiva l'animazione del bottone
            PlayButtonPressSound(); // Riproduce il suono del bottone
            portalObject.SetActive(true); // Rende visibile l'oggetto del portale
        }
    }

    private void PlayButtonPressSound()
    {
        // Riproduce l'AudioClip del bottone alla posizione del bottone stesso
        AudioSource.PlayClipAtPoint(buttonPressClip, transform.position);
    }
}
