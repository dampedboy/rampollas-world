using UnityEngine;
using System.Collections;

public class PlatformManager_3 : MonoBehaviour
{
    public GameObject[] redPlatforms;
    public GameObject[] bluePlatforms;
    public GameObject[] greenPlatforms;
    public GameObject[] yellowPlatforms;

    private float switchInterval = 3f;
    private int currentColorIndex = 0;
    private GameObject[][] allPlatforms;
    private Color[] originalColors;

    void Start()
    {
        // Initialize the array of all platforms
        allPlatforms = new GameObject[][] { redPlatforms, bluePlatforms, greenPlatforms, yellowPlatforms };

        // Store the original colors of the platforms
        originalColors = new Color[allPlatforms.Length];
        for (int i = 0; i < allPlatforms.Length; i++)
        {
            if (allPlatforms[i].Length > 0)
            {
                originalColors[i] = allPlatforms[i][0].GetComponent<Renderer>().material.color;
            }
        }

        // Start the coroutine to switch platforms visibility
        StartCoroutine(SwitchPlatformsVisibility());
    }

    IEnumerator SwitchPlatformsVisibility()
    {
        while (true)
        {
            yield return new WaitForSeconds(switchInterval);

            // Hide all platforms and reset their transparency
            for (int i = 0; i < allPlatforms.Length; i++)
            {
                foreach (var platform in allPlatforms[i])
                {
                    platform.SetActive(false);
                    SetPlatformTransparency(platform, 1f); // Reset transparency to fully visible
                }
            }

            // Show the current color platforms
            foreach (var platform in allPlatforms[currentColorIndex])
            {
                platform.SetActive(true);
            }

            // Make the secondary color platforms semi-transparent
            int secondaryColorIndex = (currentColorIndex + 1) % allPlatforms.Length;
            foreach (var platform in allPlatforms[secondaryColorIndex])
            {
                SetPlatformTransparency(platform, 0.5f); // Set transparency to 50%
                platform.SetActive(true); // Make sure secondary color platforms are active
            }

            // Move to the next color
            currentColorIndex = (currentColorIndex + 1) % allPlatforms.Length;
        }
    }

    void SetPlatformTransparency(GameObject platform, float alpha)
    {
        var renderer = platform.GetComponent<Renderer>();
        var color = renderer.material.color;
        color.a = alpha;
        renderer.material.color = color;
    }
}