using UnityEngine;

public class PlatformEmissiveSwitcher_3 : MonoBehaviour
{
    public GameObject platform1; // Prima piattaforma (rossa)
    public GameObject platform2; // Seconda piattaforma (blu)
    public GameObject platform3; // Terza piattaforma (verde)
    public GameObject platform4; // Quarta piattaforma (gialla)

    private Renderer platform1Renderer;
    private Renderer platform2Renderer;
    private Renderer platform3Renderer;
    private Renderer platform4Renderer;

    private Material platform1Material;
    private Material platform2Material;
    private Material platform3Material;
    private Material platform4Material;

    private int currentPlatformIndex = 0;
    private float switchInterval = 3f; // Tempo di attesa tra i cambiamenti
    private float timer;

    void Start()
    {
        platform1Renderer = platform1.GetComponent<Renderer>();
        platform2Renderer = platform2.GetComponent<Renderer>();
        platform3Renderer = platform3.GetComponent<Renderer>();
        platform4Renderer = platform4.GetComponent<Renderer>();

        if (platform1Renderer == null || platform2Renderer == null || platform3Renderer == null || platform4Renderer == null)
        {
            Debug.LogError("One or more platforms are missing a Renderer component.");
            return;
        }

        platform1Material = platform1Renderer.material;
        platform2Material = platform2Renderer.material;
        platform3Material = platform3Renderer.material;
        platform4Material = platform4Renderer.material;

        DisableEmission(platform1Material);
        DisableEmission(platform2Material);
        DisableEmission(platform3Material);
        DisableEmission(platform4Material);

        timer = switchInterval;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            SwitchToNextPlatform();
            timer = switchInterval;
        }
    }

    void SwitchToNextPlatform()
    {
        // Disabilita l'emissione su tutte le piattaforme prima di attivare quella corrente
        DisableEmission(platform1Material);
        DisableEmission(platform2Material);
        DisableEmission(platform3Material);
        DisableEmission(platform4Material);

        // Attiva l'emissione sulla piattaforma corrente
        switch (currentPlatformIndex)
        {
            case 0: // Rosso
                EnableEmission(platform1Material, Color.red);
                break;
            case 1: // Blu
                EnableEmission(platform2Material, Color.blue);
                break;
            case 2: // Verde
                EnableEmission(platform3Material, Color.green);
                break;
            case 3: // Giallo
                EnableEmission(platform4Material, Color.yellow);
                break;
            default:
                break;
        }

        // Incrementa l'indice per passare alla prossima piattaforma
        currentPlatformIndex = (currentPlatformIndex + 1) % 4; // 4 rappresenta il numero totale di piattaforme (rosso, blu, verde, giallo)
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