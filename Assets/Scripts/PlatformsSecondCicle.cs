using System.Collections;
using UnityEngine;

public class PlatformsSecondCicle : MonoBehaviour
{
    public GameObject[] pinkPlatforms;
    public GameObject[] lightbluePlatforms;
    public GameObject[] purplePlatforms;
    public GameObject[] orangePlatforms;

    public AudioClip cambioColoriSound; // Aggiunta: AudioClip per il suono del cambio colori
    private AudioSource audioSource; // Aggiunta: AudioSource per riprodurre il suono

    private float switchInterval = 5f;
    private int currentColorIndex = 0;
    private GameObject[][] allPlatforms;

    void Start()
    {
        // Initialize the array of all platforms
        allPlatforms = new GameObject[][] { pinkPlatforms, lightbluePlatforms, purplePlatforms, orangePlatforms };

        // Carica l'AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.volume = 0.05f; // Imposta il volume del suono a 0.05

        // Carica l'AudioClip per il suono del cambio colori
        audioSource.clip = cambioColoriSound;

        // Start the coroutine to switch platforms visibility
        StartCoroutine(SwitchPlatformsVisibility());
    }

    IEnumerator SwitchPlatformsVisibility()
    {
        while (true)
        {
            yield return new WaitForSeconds(switchInterval);

            // Hide all platforms
            foreach (var platformGroup in allPlatforms)
            {
                foreach (var platform in platformGroup)
                {
                    platform.SetActive(false);
                }
            }

            // Show the current color platforms
            foreach (var platform in allPlatforms[currentColorIndex])
            {
                platform.SetActive(true);
            }

            // Play the sound for cambio colori
            if (cambioColoriSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(cambioColoriSound);
            }

            // Move to the next color
            currentColorIndex = (currentColorIndex + 1) % allPlatforms.Length;
        }
    }
}