using System.Collections;
using UnityEngine;

public class EmitterSwitcher_1 : MonoBehaviour
{
    public GameObject platform1; // Prima piattaforma (rossa)
    public GameObject platform2; // Seconda piattaforma (blu)

    private Renderer platform1Renderer;
    private Renderer platform2Renderer;

    private Material platform1Material;
    private Material platform2Material;

    private bool isRedEmissive = false;
    private float switchTime = 5f; // Tempo di attesa tra i cambiamenti
    private float timer;

    void Start()
    {
        platform1Renderer = platform1.GetComponent<Renderer>();
        platform2Renderer = platform2.GetComponent<Renderer>();

        if (platform1Renderer == null || platform2Renderer == null)
        {
            Debug.LogError("One or both platforms are missing a Renderer component.");
            return;
        }

        platform1Material = platform1Renderer.material;
        platform2Material = platform2Renderer.material;

        DisableEmission(platform1Material);
        DisableEmission(platform2Material);

        timer = switchTime;
    }

    void Update()
    {
        if (platform1Renderer == null || platform2Renderer == null)
            return;

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            isRedEmissive = !isRedEmissive;
            UpdateEmission();
            timer = switchTime;
        }
    }

    void UpdateEmission()
    {
        if (isRedEmissive)
        {
            EnableEmission(platform1Material, Color.red);
            DisableEmission(platform2Material);
        }
        else
        {
            DisableEmission(platform1Material);
            EnableEmission(platform2Material, Color.blue);
        }
    }

    void EnableEmission(Material mat, Color color)
    {
        mat.EnableKeyword("_EMISSION");
        mat.SetColor("_EmissionColor", color * Mathf.LinearToGammaSpace(1.0f));
    }

    void DisableEmission(Material mat)
    {
        mat.DisableKeyword("_EMISSION");
    }
}
