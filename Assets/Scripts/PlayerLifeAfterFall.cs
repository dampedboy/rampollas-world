using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLifeAfterFall : MonoBehaviour
{
    public Transform respawnPoint;
    public int maxLives = 3;
    private int currentLives;
    public GameObject[] lifeModels; // Array di modelli 3D dei cuori
    public CoinController coinController; // Riferimento allo script CoinController

    void Start()
    {
        currentLives = maxLives;
        UpdateLivesUI();

        if (coinController == null)
        {
            coinController = FindObjectOfType<CoinController>();
        }
    }

    void Update()
    {
        if (transform.position.y < -10) // Se il player cade
        {
            LoseLife();
        }
    }

    void LoseLife()
    {
        currentLives--;
        UpdateLivesUI();

        // Chiama il metodo LoseLife nel CoinController per dimezzare le monete
        if (coinController != null)
        {
            coinController.LoseLife();
        }

        if (currentLives <= 0)
        {
            GameOver();
        }
        else
        {
            Respawn();
        }
    }

    void UpdateLivesUI()
    {
        for (int i = 0; i < lifeModels.Length; i++)
        {
            if (i < currentLives)
            {
                lifeModels[i].SetActive(true);
            }
            else
            {
                lifeModels[i].SetActive(false);
            }
        }
    }

    void Respawn()
    {
        transform.position = respawnPoint.position;
        // Altre azioni di respawn, come ripristinare la velocità o animazioni
    }

    void GameOver()
    {
        SceneManager.LoadScene("GameOverScene");
        // O altre azioni per gestire il ritorno all'hub centrale
    }
}