using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LifePlayerAfterBlock : MonoBehaviour
{
    public GameObject[] hearts;        // Array dei cuori 3D importati da Blender
    public TMP_Text gameOverText;      // Testo Game Over (Text Mesh Pro)
    public string hubSceneName = "Hub"; // Nome della scena dell'Hub
    public float gameOverDelay = 3.0f; // Ritardo prima di tornare alla scena dell'Hub

    private int lives = 3;             // Vite del Player

    void Start()
    {
        gameOverText.gameObject.SetActive(false); // Nasconde il testo Game Over all'inizio
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            LoseLife();
        }
    }

    void LoseLife()
    {
        if (lives > 0)
        {
            lives--;
            hearts[lives].SetActive(false); // Disattiva un cuore

            if (lives <= 0)
            {
                GameOver();
            }
        }
    }

    void GameOver()
    {
        gameOverText.gameObject.SetActive(true); // Mostra il testo Game Over
        Invoke("ReturnToHub", gameOverDelay);    // Torna alla scena dell'Hub dopo un ritardo
    }

    void ReturnToHub()
    {
        SceneManager.LoadScene(hubSceneName); // Carica la scena dell'Hub
    }
}