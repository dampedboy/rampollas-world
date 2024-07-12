using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;
    public TextMeshProUGUI coinText; // Assicurati di avere un oggetto TextMeshProUGUI nella scena per visualizzare le monete

    static int coinCount = 0;

    

    void Start()
    {
        UpdateCoinText();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex != 0 && currentSceneIndex != 6 && currentSceneIndex != 12 && currentSceneIndex != 18)
        {
            coinCount++;
            UpdateCoinText();
            Debug.Log(coinCount);
        }
        
    }

    void UpdateCoinText()
    {
        if (coinText != null)
        {
            coinText.text = coinCount.ToString();
        }
    }

    void BuyHeart()
    {
        coinCount = coinCount - 5;
        UpdateCoinText();
        Debug.Log(coinCount);
    }

    void BuyPortal()
    {
        coinCount = coinCount - 10;
        UpdateCoinText();
        Debug.Log(coinCount);
    }

}

