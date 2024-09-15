using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteButtonController : MonoBehaviour
{
    public Animator buttonAnimator; // Riferimento all'animator del bottone
    public GameObject[] platforms; // Array di piattaforme da rendere visibili
    public float platformsVisibleDuration = 4f; // Durata per cui le piattaforme rimangono visibili
    public AudioClip buttonPressClip; // Riferimento all'AudioClip

    private bool isButtonPressed = false; // Flag per controllare se il bottone è stato premuto
    private Vector3[] initialPositions; // Array per salvare le posizioni iniziali delle piattaforme

    void Start()
    {
        // Inizializza l'array delle posizioni iniziali
        initialPositions = new Vector3[platforms.Length];

        // Salva le posizioni iniziali e nasconde inizialmente le piattaforme
        for (int i = 0; i < platforms.Length; i++)
        {
            initialPositions[i] = platforms[i].transform.position; // Salva la posizione iniziale
            platforms[i].SetActive(false); // Nasconde la piattaforma
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
            StartCoroutine(ShowPlatformsTemporarily());
        }
    }

    private void PlayButtonPressSound()
    {
        // Crea un nuovo AudioSource, assegna la clip e riproduce il suono
        AudioSource.PlayClipAtPoint(buttonPressClip, transform.position);
    }

    private IEnumerator ShowPlatformsTemporarily()
    {
        // Ripristina le posizioni iniziali e rendi visibili le piattaforme
        for (int i = 0; i < platforms.Length; i++)
        {
            platforms[i].transform.position = initialPositions[i]; // Ripristina la posizione
            platforms[i].SetActive(true); // Rendi visibile la piattaforma
        }

        // Attendi per la durata specificata
        yield return new WaitForSeconds(platformsVisibleDuration);

        // Nascondi le piattaforme
        foreach (GameObject platform in platforms)
        {
            platform.SetActive(false);
        }

        // Nota: il flag `isButtonPressed` non viene resettato
    }
}
