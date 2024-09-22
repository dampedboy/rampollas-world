using System.Collections;
using UnityEngine;

public class GreenButtonController : MonoBehaviour, ITemporarilyActivatableButton
{
    public Animator buttonAnimator;
    public GameObject[] platforms;
    public float platformsVisibleDuration = 5f;
    public AudioClip buttonPressClip;
    public AudioClip cageSoundClip;

    private bool isButtonPressed = false;
    private Coroutine platformCoroutine;

    void Start()
    {
        // Le piattaforme inizialmente visibili
        foreach (GameObject platform in platforms)
        {
            platform.SetActive(true);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isButtonPressed)
        {
            isButtonPressed = true;
            buttonAnimator.SetTrigger("Press");
            PlayButtonPressSound();
            platformCoroutine = StartCoroutine(ShowPlatformsTemporarily(platformsVisibleDuration));
        }
    }

    private void PlayButtonPressSound()
    {
        AudioSource.PlayClipAtPoint(buttonPressClip, transform.position);
    }

    private void PlayCageSound()
    {
        AudioSource.PlayClipAtPoint(cageSoundClip, transform.position, 3f); // Aumenta il volume di 3x
    }

    private IEnumerator ShowPlatformsTemporarily(float duration)
    {
        // Nasconde le piattaforme
        foreach (GameObject platform in platforms)
        {
            platform.SetActive(false);
        }

        // Attende per la durata specificata
        yield return new WaitForSeconds(duration);

        // Mostra nuovamente le piattaforme
        foreach (GameObject platform in platforms)
        {
            platform.SetActive(true);
        }

        // Riproduce il suono della gabbia
        PlayCageSound();
    }

    // Implementazione dell'interfaccia ITemporarilyActivatableButton
    public void ReactivateEffect(float additionalDuration)
    {
        // Se una coroutine è già in corso, la ferma
        if (platformCoroutine != null)
        {
            StopCoroutine(platformCoroutine);
        }

        // Riavvia l'effetto temporaneo con la durata aggiuntiva
        platformCoroutine = StartCoroutine(ShowPlatformsTemporarily(additionalDuration));
    }
}
