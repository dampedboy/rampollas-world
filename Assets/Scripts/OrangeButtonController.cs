using System.Collections;
using UnityEngine;

public class OrangeButtonController : MonoBehaviour
{
    public GameObject objectToTeleport; // Oggetto da teletrasportare
    public Vector3 teleportPosition; // Posizione di teletrasporto da impostare nell'inspector
    public float stayDuration = 5f; // Durata in cui l'oggetto resta teletrasportato
    public Animator buttonAnimator; // Riferimento all'animator del bottone (facoltativo)
    public AudioClip buttonPressClip; // Suono da riprodurre quando il bottone è premuto (facoltativo)

    private Vector3 initialPosition; // Posizione iniziale dell'oggetto
    private bool isButtonPressed = false; // Flag per controllare se il bottone è stato premuto

    void Start()
    {
        // Salva la posizione iniziale dell'oggetto
        initialPosition = objectToTeleport.transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        // Controlla se il player ha toccato il bottone
        if (other.CompareTag("Player") && !isButtonPressed)
        {
            isButtonPressed = true; // Imposta il flag per prevenire doppie attivazioni

            if (buttonAnimator != null)
            {
                buttonAnimator.SetTrigger("Press"); // Attiva l'animazione del bottone
            }

            if (buttonPressClip != null)
            {
                PlayButtonPressSound(); // Riproduce il suono del bottone
            }

            // Inizia la coroutine per teletrasportare l'oggetto e riportarlo indietro
            StartCoroutine(TeleportObject());
        }
    }

    private void PlayButtonPressSound()
    {
        // Riproduce il suono del bottone
        AudioSource.PlayClipAtPoint(buttonPressClip, transform.position);
    }

    private IEnumerator TeleportObject()
    {
        // Teletrasporta l'oggetto nella posizione specificata
        objectToTeleport.transform.position = teleportPosition;

        // Mantiene l'oggetto nella nuova posizione per il tempo specificato
        yield return new WaitForSeconds(stayDuration);

        // Riporta l'oggetto alla posizione iniziale
        objectToTeleport.transform.position = initialPosition;

        // Resetta lo stato del bottone per permettere nuove attivazioni
        isButtonPressed = false;
    }
}
