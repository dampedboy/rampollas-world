using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;
    public TextMeshProUGUI coinText; // Assicurati di avere un oggetto TextMeshProUGUI nella scena per visualizzare le monete
    public float textChangeDuration = 1.0f; // Durata dell'effetto di cambiamento del testo
    public float buyDelay = 0.5f; // Ritardo in secondi prima di effettuare l'acquisto

    public AudioClip soldi_denied; // Suono per quando i soldi non sono sufficienti
    public AudioClip soldi_spesi; // Suono per quando i soldi vengono spesi

    public AudioSource audioSource; // Componente AudioSource per riprodurre i suoni

    private static int coinCount = 0;
    static HashSet<int> visitedScenes = new HashSet<int>();

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
        if (currentSceneIndex != 0 && currentSceneIndex != 1 && currentSceneIndex != 2 && currentSceneIndex != 8 && currentSceneIndex != 14 && currentSceneIndex != 20 && !visitedScenes.Contains(currentSceneIndex))
        {
            CoinCount++;
            UpdateCoinText();
            visitedScenes.Add(currentSceneIndex);
            Debug.Log(CoinCount);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // La logica è ora gestita nello Start, quindi questa funzione può rimanere vuota o essere rimossa.
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
        Debug.Log(CoinCount);
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