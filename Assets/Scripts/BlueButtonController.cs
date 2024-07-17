using UnityEngine;

public class BlueButtonController : MonoBehaviour
{
    public Animator buttonAnimator; // Riferimento all'animator del bottone blu
    public GameObject keyObject; // Riferimento all'oggetto chiave
    public AudioClip buttonPressClip; // Riferimento all'AudioClip

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
            PlayButtonPressSound(); // Riproduce il suono del bottone
            keyObject.SetActive(true); // Rende visibile l'oggetto chiave
        }
    }

    private void PlayButtonPressSound()
    {
        // Crea un nuovo AudioSource, assegna la clip e riproduce il suono
        AudioSource.PlayClipAtPoint(buttonPressClip, transform.position);
    }
}
