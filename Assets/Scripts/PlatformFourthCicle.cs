using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFourthCicle : MonoBehaviour
{
    public GameObject[] whitePlatforms; // Solo blackPlatforms
    public AudioClip cambioColoriSound; // AudioClip per il suono del cambio colori
    private AudioSource audioSource;    // AudioSource per riprodurre il suono

    private float switchInterval = 1f;  // Intervallo di 2 secondi per l'accensione/spegnimento
    private float visibilityDuration = 1f; // Durata della visibilità di 2 secondi

    void Start()
    {
        // Carica l'AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.volume = 0.05f; // Imposta il volume del suono a 0.05

        // Carica l'AudioClip per il suono del cambio colori
        audioSource.clip = cambioColoriSound;

        // Start the coroutine to manage the visibility of black platforms
        StartCoroutine(ManageBlackPlatformsVisibility());
    }

    IEnumerator ManageBlackPlatformsVisibility()
    {
        while (true)
        {
            // Rendere le piattaforme visibili
            foreach (var platform in whitePlatforms)
            {
                platform.SetActive(true);
            }

            // Riproduci il suono se definito
            if (cambioColoriSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(cambioColoriSound);
            }

            // Aspetta per la durata della visibilità (2 secondi)
            yield return new WaitForSeconds(visibilityDuration);

            // Nascondere le piattaforme
            foreach (var platform in whitePlatforms)
            {
                platform.SetActive(false);
            }

            // Aspetta per il prossimo ciclo di visibilità (2 secondi di intermittenza)
            yield return new WaitForSeconds(switchInterval);
        }
    }
}
