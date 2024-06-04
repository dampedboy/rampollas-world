using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;
    public GameObject[] hearts;
    public GameObject gameOverText;
    public float hitVisibilityDuration = 2f;
    private float hitVisibilityTimer = 0f;
    private bool isPlayerVisible = true;
    private int hitCount = 0; // Contatore per tenere traccia del numero di colpi ricevuti

    void Start()
    {
        currentHealth = maxHealth;
        gameOverText.SetActive(false);
    }

    void Update()
    {
        if (!isPlayerVisible)
        {
            hitVisibilityTimer += Time.deltaTime;
            if (hitVisibilityTimer >= hitVisibilityDuration)
            {
                isPlayerVisible = true;
                SetPlayerVisibility(true);
                hitVisibilityTimer = 0f;
                hitCount++;
            }
        }

        // Controllo se il giocatore è stato colpito per un numero di volte dispari e lo rendo invisibile
        if (hitCount % 2 != 0 && isPlayerVisible)
        {
            isPlayerVisible = false;
            SetPlayerVisibility(false);
            hitVisibilityTimer = 0f;
        }
    }

    public void TakeDamage(int amount)
    {
        if (isPlayerVisible)
        {
            currentHealth -= amount;
            UpdateHearts();

            if (currentHealth <= 0)
            {
                GameOver();
            }
            else
            {
                isPlayerVisible = false;
                SetPlayerVisibility(false);
                hitVisibilityTimer = 0f;
                hitCount = 1; // Reset del contatore quando il giocatore viene colpito la prima volta
            }
        }
    }

    void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].SetActive(true);
            }
            else
            {
                hearts[i].SetActive(false);
            }
        }
    }

    void GameOver()
    {
        gameOverText.SetActive(true);
        GetComponent<PlayerMovement>().enabled = false;
    }

    void SetPlayerVisibility(bool isVisible)
    {
        Renderer playerRenderer = GetComponent<Renderer>();
        if (playerRenderer != null)
        {
            playerRenderer.enabled = isVisible;
        }
    }
}