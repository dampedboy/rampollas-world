using UnityEngine;
using TMPro;


public class CoinController : MonoBehaviour
{
    public GameObject player;
    public GameObject lastPlatform;
    public GameObject coinTextObject; // Riferimento al GameObject che contiene il testo delle monete
    public GameObject heartContainer; // Assumiamo che ci sia un GameObject che contiene i cuori come figli

    private TMP_Text coinText; // Riferimento al componente TMP_Text
    private int coinCount = 0;
    private int initialCoinCount = 150;
    private int lifeCount;

    void Start()
    {
        // Assicurati che il riferimento al coinTextObject sia stato assegnato nell'Inspector
        if (coinTextObject != null)
        {
            // Assicurati che il GameObject del testo delle monete sia attivo
            if (!coinTextObject.activeSelf)
            {
                coinTextObject.SetActive(true);
            }

            // Ottieni il componente TMP_Text dal GameObject
            coinText = coinTextObject.GetComponent<TMP_Text>();

            // Assicurati che il componente TMP_Text esista sul GameObject del testo delle monete
            if (coinText != null)
            {
                // Imposta il contatore delle monete a 0 all'inizio
                coinCount = 0;
                UpdateCoinUI();

                // Conta il numero di vite (cuori) all'inizio del gioco
                lifeCount = heartContainer.transform.childCount;
            }
            else
            {
                Debug.LogError("Il GameObject del testo delle monete non contiene un componente TMP_Text.");
            }
        }
        else
        {
            Debug.LogError("Il riferimento al GameObject del testo delle monete non è stato assegnato nell'Inspector.");
        }
    }

    void Update()
    {
        // Controlla se il player ha raggiunto l'ultima piattaforma
        if (Vector3.Distance(player.transform.position, lastPlatform.transform.position) < 1.0f)
        {
            // Aumenta il contatore delle monete
            coinCount = initialCoinCount;
            UpdateCoinUI();
        }
    }

    // Metodo per aggiornare l'interfaccia delle monete
    void UpdateCoinUI()
    {
        if (coinText != null)
        {
            coinText.text = coinCount.ToString();
        }
        else
        {
            Debug.LogError("Il riferimento al componente TMP_Text del GameObject del testo delle monete è nullo.");
        }
    }

    // Metodo per gestire la perdita di una vita
    public void LoseLife()
    {
        // Dimezza le monete
        coinCount = Mathf.Max(coinCount / 2, 0);
        UpdateCoinUI();
    }
}