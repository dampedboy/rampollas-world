using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance; // Istanza singleton
    public TextMeshProUGUI coinText; // Elemento UI per visualizzare le monete
    public float textChangeDuration = 1.0f; // Durata dell'effetto di cambio testo
    public float buyDelay = 0.5f; // Ritardo prima dell'acquisto di un oggetto

    public AudioClip soldi_denied; // Suono riprodotto quando l'acquisto viene negato
    public AudioClip soldi_spesi; // Suono riprodotto quando l'acquisto è riuscito

    public AudioSource audioSource; // Sorgente audio per riprodurre i suoni

    private static int coinCount = 0; // Contatore interno delle monete

    // Variabile per tracciare se è in corso una respawn
    public static bool isRespawning = false;

    private static int previousSceneIndex = -1; // Traccia l'indice della scena precedente

    public static int CoinCount
    {
        get { return coinCount; }
        private set { coinCount = value; }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        UpdateCoinText();
        SceneManager.sceneLoaded += OnSceneLoaded;

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Verifica se la scena è stata ricaricata tramite ReloadSceneOnKeyPress
        if (ReloadSceneOnKeyPress.isSceneReloaded)
        {
            ReloadSceneOnKeyPress.isSceneReloaded = false;
        }
        else if (!isRespawning &&
                 (currentSceneIndex != 0 && currentSceneIndex != 1 && currentSceneIndex != 2 &&
                  currentSceneIndex != 8 && currentSceneIndex != 14 && currentSceneIndex != 20 &&
                  currentSceneIndex != 26) ||
                 (currentSceneIndex == 1 && previousSceneIndex == 30))
        {
            CoinCount++;
            UpdateCoinText();
            Debug.Log("Moneta aggiunta, conteggio attuale: " + CoinCount);
        }

        // Aggiorna l'indice della scena precedente dopo il caricamento
        previousSceneIndex = currentSceneIndex;

        // Resetta il flag di respawn dopo il caricamento della scena
        isRespawning = false;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Questa funzione può essere lasciata vuota o rimossa poiché la logica è gestita in Start
    }

    void UpdateCoinText()
    {
        if (coinText != null)
        {
            coinText.text = CoinCount.ToString();
        }
    }

    public void BuyHeart()
    {
        if (CoinCount >= 4)
        {
            StartCoroutine(BuyWithDelay(4));
        }
        else
        {
            UpdateCoinTextWithEffect();
        }
    }

    public void BuyPortal()
    {
        if (CoinCount >= 8)
        {
            StartCoroutine(BuyWithDelay(8));
        }
        else
        {
            UpdateCoinTextWithEffect();
        }
    }

    private IEnumerator BuyWithDelay(int cost)
    {
        yield return new WaitForSeconds(buyDelay);

        CoinCount -= cost;
        Debug.Log("Oggetto acquistato, monete rimanenti: " + CoinCount);
        UpdateCoinText();

        if (audioSource != null && soldi_spesi != null)
        {
            audioSource.PlayOneShot(soldi_spesi);
        }
    }

    public void UpdateCoinTextWithEffect()
    {
        if (coinText != null)
        {
            coinText.text = CoinCount.ToString();
            StartCoroutine(ChangeTextEffect());
        }
    }

    private IEnumerator ChangeTextEffect()
    {
        if (audioSource != null && soldi_denied != null)
        {
            audioSource.PlayOneShot(soldi_denied);
        }

        coinText.color = Color.red;
        coinText.fontSize *= 1.2f; // Aumenta la dimensione del font del 20%
        yield return new WaitForSeconds(textChangeDuration);
        coinText.color = Color.white;
        coinText.fontSize /= 1.2f; // Ripristina la dimensione originale del font
    }
}


