using UnityEngine;
using System.Collections;

public class PlatformManager : MonoBehaviour
{
    public GameObject[] redPlatforms;
    public GameObject[] bluePlatforms;
    public AudioClip switchSound;  // Add a public variable for the sound clip

    private float switchInterval = 3f;
    private int currentColorIndex = 0;
    private GameObject[][] allPlatforms;
    private AudioSource audioSource;  // Add a private variable for the AudioSource

    void Start()
    {
        // Initialize the array of all platforms
        allPlatforms = new GameObject[][] { redPlatforms, bluePlatforms };

        // Add an AudioSource component to the GameObject
        audioSource = gameObject.AddComponent<AudioSource>();

        // Set the AudioClip of the AudioSource to the provided switchSound
        audioSource.clip = switchSound;

        // Set the volume to 0.2 (20% of maximum volume)
        audioSource.volume = 0.05f;

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

            // Play the switch sound
            if (audioSource != null && switchSound != null)
            {
                audioSource.Play();
            }

            // Move to the next color
            currentColorIndex = (currentColorIndex + 1) % allPlatforms.Length;
        }
    }
}
