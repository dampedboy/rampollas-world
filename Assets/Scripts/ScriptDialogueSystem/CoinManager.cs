using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;
    public TextMeshProUGUI coinText; // Assicurati di avere un oggetto TextMeshProUGUI nella scena per visualizzare le monete
    public float textChangeDuration = 2.0f; // Durata dell'effetto di cambiamento del testo

    private static int coinCount = 0;
    static HashSet<int> visitedScenes = new HashSet<int>();

    public static int CoinCount
    {
        get { return coinCount; }
        private set { coinCount = value; }
    }

    void Start()
    {
        UpdateCoinText();
        SceneManager.sceneLoaded += OnSceneLoaded;

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex != 0 && currentSceneIndex != 1 && currentSceneIndex != 7 && currentSceneIndex != 13 && currentSceneIndex != 19 && !visitedScenes.Contains(currentSceneIndex))
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
        if (CoinCount >= 5)
        {
            CoinCount -= 5;
            Debug.Log(CoinCount);
        }
        else
        {
            UpdateCoinTextWithEffect();
        }
    }

    public void BuyPortal()
    {
        if (CoinCount >= 10)
        {
            CoinCount -= 10;
            Debug.Log(CoinCount);
        }
        else
        {
            UpdateCoinTextWithEffect();
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
        coinText.color = Color.red;
        coinText.fontSize *= 1.2f; // Aumenta la dimensione del font del 20%
        yield return new WaitForSeconds(textChangeDuration);
        coinText.color = Color.white;
        coinText.fontSize /= 1.2f; // Ripristina la dimensione originale del font
    }
}



