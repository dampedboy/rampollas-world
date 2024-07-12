using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int maxLives = 3;
    private int currentLives;
    public Transform respawnPoint;
    public GameObject[] hearts;
    public TMP_Text gameOverText;
    public float fallThreshold = -10f;

    private void Start()
    {
        // Carica le vite da PlayerPrefs o imposta a maxLives se è il primo livello
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            currentLives = maxLives;
        }
        else
        {
            currentLives = PlayerPrefs.GetInt("PlayerLives", maxLives);
        }

        UpdateHearts();
        gameOverText.gameObject.SetActive(false);
    }

    private void Update()
    {
        // Controlla se il giocatore è caduto sotto la soglia
        if (transform.position.y < fallThreshold)
        {
            FallRespawn();
        }
    }

    private void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].SetActive(i < currentLives);
        }
    }

    public void TakeDamage()
    {
        currentLives--;
        PlayerPrefs.SetInt("PlayerLives", currentLives);
        UpdateHearts();

        if (currentLives <= 0)
        {
            GameOver();
        }
        else
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        transform.position = respawnPoint.position;
    }

    private void FallRespawn()
    {
        currentLives--;
        PlayerPrefs.SetInt("PlayerLives", currentLives);
        UpdateHearts();

        if (currentLives <= 0)
        {
            GameOver();
        }
        else
        {
            Respawn();
        }
    }

    private void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        // Eventuali altre logiche di game over qui (blocco input, animazioni, etc.)
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile") || other.CompareTag("Dynamite"))
        {
            TakeDamage();
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("PlayerLives");
    }
}

