using System.Collections;
using UnityEngine;

public class GreenButtonController : MonoBehaviour
{
    public Animator buttonAnimator; // Riferimento all'animator del bottone
    public GameObject[] platforms; // Array di piattaforme da rendere visibili
    public float platformsVisibleDuration = 5f; // Durata per cui le piattaforme rimangono visibili

    private bool isButtonPressed = false; // Flag per controllare se il bottone è stato premuto

    void Start()
    {
        // Nasconde inizialmente le piattaforme
        foreach (GameObject platform in platforms)
        {
            platform.SetActive(true);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Controlla se il player ha toccato il bottone
        if (other.CompareTag("Player") && !isButtonPressed)
        {
            isButtonPressed = true;
            buttonAnimator.SetTrigger("Press"); // Attiva l'animazione del bottone
            StartCoroutine(ShowPlatformsTemporarily());
        }
    }

    private IEnumerator ShowPlatformsTemporarily()
    {
        // Rendi visibili le piattaforme
        foreach (GameObject platform in platforms)
        {
            platform.SetActive(false);
        }

        // Attendi per la durata specificata
        yield return new WaitForSeconds(platformsVisibleDuration);

        // Nascondi le piattaforme
        foreach (GameObject platform in platforms)
        {
            platform.SetActive(true);
        }

        // Nota: il flag `isButtonPressed` non viene resettato
    }
}