using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance; // Singleton instance
    public TextMeshProUGUI coinText; // UI text element for displaying coins
    public float textChangeDuration = 1.0f; // Duration of text change effect
    public float buyDelay = 0.5f; // Delay before purchasing an item

    public AudioClip soldi_denied; // Sound played when purchase is denied
    public AudioClip soldi_spesi; // Sound played when purchase is successful

    public AudioSource audioSource; // Audio source for playing sounds

    private static int coinCount = 0; // Internal coin count

    // Variable to track if a respawn is happening
    public static bool isRespawning = false;

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

        // Check if the player is respawning
        if (!isRespawning && currentSceneIndex != 0 && currentSceneIndex != 1 && currentSceneIndex != 2 && currentSceneIndex != 8 && currentSceneIndex != 14 && currentSceneIndex != 20 && currentSceneIndex != 26)
        {
            CoinCount++;
            UpdateCoinText();
            Debug.Log("Coin added, current count: " + CoinCount);
        }

        // Reset the respawn flag after scene load
        isRespawning = false;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // This function can be left empty or removed since logic is handled in Start
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
        Debug.Log("Item purchased, coins left: " + CoinCount);
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
        coinText.fontSize *= 1.2f; // Increase font size by 20%
        yield return new WaitForSeconds(textChangeDuration);
        coinText.color = Color.white;
        coinText.fontSize /= 1.2f; // Restore original font size
    }
}
