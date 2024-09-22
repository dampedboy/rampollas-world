using System.Collections;
using UnityEngine;

public class RedButtonController : MonoBehaviour, ITemporarilyActivatableButton
{
    public Animator buttonAnimator; // Riferimento all'animator del bottone
    public GameObject[] platforms; // Array di piattaforme da rendere visibili
    public float platformsVisibleDuration = 5f; // Durata per cui le piattaforme rimangono visibili
    public AudioClip buttonPressClip; // Riferimento all'AudioClip

    private bool isButtonPressed = false; // Flag per controllare se il bottone è stato premuto
    private Coroutine platformCoroutine; // Per gestire la coroutine attuale

    void Start()
    {
        // Nasconde inizialmente le piattaforme
        foreach (GameObject platform in platforms)
        {
            platform.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Controlla se il player ha toccato il bottone
        if (other.CompareTag("Player") && !isButtonPressed)
        {
            isButtonPressed = true;
            buttonAnimator.SetTrigger("Press"); // Attiva l'animazione del bottone
            PlayButtonPressSound(); // Riproduce il suono del bottone
            platformCoroutine = StartCoroutine(ShowPlatformsTemporarily(platformsVisibleDuration));
        }
    }

    private void PlayButtonPressSound()
    {
        // Crea un nuovo AudioSource, assegna la clip e riproduce il suono
        AudioSource.PlayClipAtPoint(buttonPressClip, transform.position);
    }

    private IEnumerator ShowPlatformsTemporarily(float duration)
    {
        // Rendi visibili le piattaforme
        foreach (GameObject platform in platforms)
        {
            platform.SetActive(true);
        }

        // Attendi per la durata specificata
        yield return new WaitForSeconds(duration);

        // Nascondi le piattaforme
        foreach (GameObject platform in platforms)
        {
            platform.SetActive(false);
        }
    }

    // Implementazione del metodo ReactivateEffect dall'interfaccia ITemporarilyActivatableButton
    public void ReactivateEffect(float additionalDuration)
    {
        // Se una coroutine è già attiva, fermala prima di riattivarla
        if (platformCoroutine != null)
        {
            StopCoroutine(platformCoroutine);
        }

        // Riavvia l'effetto temporaneo con la nuova durata
        platformCoroutine = StartCoroutine(ShowPlatformsTemporarily(additionalDuration));
    }
}
