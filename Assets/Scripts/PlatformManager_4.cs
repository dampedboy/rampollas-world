using UnityEngine;
using System.Collections;

public class PlatformManager_4 : MonoBehaviour
{
    public GameObject[] redPlatforms;
    public GameObject[] bluePlatforms;
    public GameObject[] greenPlatforms;
    public GameObject[] yellowPlatforms;

    private float switchInterval = 2f;
    private GameObject[][] allPlatforms;
    private System.Random random = new System.Random();

    void Start()
    {
        // Initialize the array of all platforms
        allPlatforms = new GameObject[][] { redPlatforms, bluePlatforms, greenPlatforms, yellowPlatforms };

        // Start the coroutine to switch platforms visibility
        StartCoroutine(SwitchPlatformsVisibility());
    }

    IEnumerator SwitchPlatformsVisibility()
    {
        while (true)
        {
            yield return new WaitForSeconds(switchInterval);

            // Choose a random color index to hide
            int randomColorIndex = random.Next(0, allPlatforms.Length);

            // Ensure at least one color is visible
            bool[] visibility = new bool[allPlatforms.Length];
            visibility[randomColorIndex] = false;
            for (int i = 0; i < allPlatforms.Length; i++)
            {
                if (i != randomColorIndex)
                {
                    visibility[i] = true;
                }
            }

            // Hide all platforms
            for (int i = 0; i < allPlatforms.Length; i++)
            {
                foreach (var platform in allPlatforms[i])
                {
                    platform.SetActive(visibility[i]);
                }
            }
        }
    }
}