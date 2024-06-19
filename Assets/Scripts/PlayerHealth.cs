using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxLives = 3;
    public GameObject[] hearts;
    public TextMeshProUGUI gameOverText;

    private int currentLives;

    void Start()
    {
        currentLives = maxLives;
        gameOverText.gameObject.SetActive(false);
        UpdateHearts();
    }

    public void TakeDamage()
    {
        if (currentLives > 0)
        {
            currentLives--;
            UpdateHearts();

            if (currentLives <= 0)
            {
                GameOver();
            }
        }
    }

    void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].SetActive(i < currentLives);
        }
    }

    void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        StartCoroutine(LoadHubAfterDelay(3f));
    }

    IEnumerator LoadHubAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Hub");
    }
}