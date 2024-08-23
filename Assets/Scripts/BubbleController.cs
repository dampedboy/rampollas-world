using UnityEngine;

public class BubbleController : MonoBehaviour
{
    // Questi campi devono essere impostati dall'Inspector
    public GameObject bubble;
    public GameObject gabbia;

    void Start()
    {
        // Assicurati che bubble sia visibile all'inizio
        if (bubble != null)
        {
            bubble.SetActive(true);
        }
    }

    void Update()
    {
        // Controlla se gabbia è ancora attivo
        if (gabbia != null && !gabbia.activeInHierarchy)
        {
            // Se gabbia non è attivo, nascondi bubble
            if (bubble != null)
            {
                bubble.SetActive(false);
            }
        }
    }
}

