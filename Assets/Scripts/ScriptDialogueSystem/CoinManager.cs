using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;
    public TextMeshProUGUI coinText; // Assicurati di avere un oggetto TextMeshProUGUI nella scena per visualizzare le monete

    static int coinCount = 0;
    static HashSet<int> visitedScenes = new HashSet<int>();

    void Start()
    {
        UpdateCoinText();
        SceneManager.sceneLoaded += OnSceneLoaded;

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex != 0 && currentSceneIndex != 1 && currentSceneIndex != 7 && currentSceneIndex != 13 && currentSceneIndex != 19 && !visitedScenes.Contains(currentSceneIndex))
        {
            coinCount++;
            UpdateCoinText();
            visitedScenes.Add(currentSceneIndex);
            Debug.Log(coinCount);
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
            coinText.text = coinCount.ToString();
        }
    }

    public void BuyHeart()
    {
        coinCount = coinCount - 5;
        UpdateCoinText();
        Debug.Log(coinCount);
    }

    public void BuyPortal()
    {
        coinCount = coinCount - 10;
        UpdateCoinText();
        Debug.Log(coinCount);
    }
}

