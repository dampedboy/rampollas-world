using UnityEngine;
using System.Collections;

public class PlatformManager_2 : MonoBehaviour
{
    public GameObject[] redPlatforms;
    public GameObject[] bluePlatforms;
    public GameObject[] greenPlatforms;

    private float switchInterval = 3f;
    private int currentColorIndex = 0;
    private GameObject[][] allPlatforms;

    void Start()
    {
        // Initialize the array of all platforms
        allPlatforms = new GameObject[][] { redPlatforms, bluePlatforms, greenPlatforms };

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

            // Move to the next color
            currentColorIndex = (currentColorIndex + 1) % allPlatforms.Length;
        }
    }
}