using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlatformTrigger : MonoBehaviour
{
    public GameObject fallingObjectPrefab; // Prefab dell'oggetto che cade
    public Transform spawnPoint; // Punto di spawn dell'oggetto che cade
    public TextMeshProUGUI gameOverText; // TextMeshPro per il Game Over
    public GameObject[] hearts; // Array dei cuori 3D sull'interfaccia
    public int playerHearts = 3; // Numero di cuori del player

    private void Start()
    {
        gameOverText.gameObject.SetActive(false); // Nasconde il testo di Game Over all'inizio
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Instantiate(fallingObjectPrefab, spawnPoint.position, Quaternion.identity); // Materializza l'oggetto
        }
    }

    public void PlayerHit()
    {
        if (playerHearts > 0)
        {
            playerHearts--;
            UpdateHearts();
        }

        if (playerHearts <= 0)
        {
            GameOver();
        }
    }

    private void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < playerHearts)
            {
                hearts[i].SetActive(true); // Mostra il cuore
            }
            else
            {
                hearts[i].SetActive(false); // Nasconde il cuore
            }
        }
    }

    private void GameOver()
    {
        gameOverText.gameObject.SetActive(true); // Mostra il testo di Game Over
        Invoke("ReturnToHub", 2f); // Attende 2 secondi prima di tornare alla scena "Hub"
    }

    private void ReturnToHub()
    {
        SceneManager.LoadScene("Hub");
    }
}