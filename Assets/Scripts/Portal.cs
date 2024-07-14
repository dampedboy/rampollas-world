using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class Portal : MonoBehaviour
{
    private int portalLevel = 0;
    public TMP_Text portalLevelText;

    // Metodo chiamato quando un altro collider entra nel trigger
    private void OnTriggerEnter(Collider other)
    {
        // Controlla se l'oggetto che entra nel trigger è il player
        if (other.CompareTag("Player"))
        {
            // Carica il prossimo livello
            LoadNextLevel();
        }
    }

    private void Update()
    {
        // Assicuriamoci che il riferimento al Text sia valido
        if (portalLevelText != null)
        {
            int pl = portalLevel + 1;
            // Mostra il valore di portalLevel nel Text
            portalLevelText.text = "Lvl. " + pl;
        }
    }

    public void UpdatePortal()
    {
        if (CoinManager.CoinCount >= 0)
        {
            portalLevel++;
            Debug.Log("Livello attuale del portale: " + portalLevel);

            // Avvia la coroutine per ruotare il portale
            StartCoroutine(RotatePortalForTime(2f)); // Ruota il portale per 2 secondi

            // Assicuriamoci che il riferimento al Text sia valido
            if (portalLevelText != null)
            {
                int pl = portalLevel + 1;
                // Mostra il valore di portalLevel nel Text
                portalLevelText.text = "Lvl. " + pl;
            }
        }
    }

    private IEnumerator RotatePortalForTime(float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            transform.Rotate(Vector3.forward, 90f * Time.deltaTime / duration); // Ruota gradualmente lungo l'asse z nel tempo
            elapsed += Time.deltaTime;
            yield return null;
        }

    }


    // Metodo per caricare il prossimo livello
    private void LoadNextLevel()
    {
        // Ottieni l'indice della scena attuale
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        int nextSceneIndex = currentSceneIndex;
        // Calcola l'indice del prossimo livello
        if (currentSceneIndex == 0)
        {
            nextSceneIndex = currentSceneIndex + 1 + portalLevel;
        }
        else
        {
            nextSceneIndex = currentSceneIndex + 1;
        }

        // Controlla se l'indice del prossimo livello è valido
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            // Carica il prossimo livello
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            // Se non ci sono più livelli, ritorna al primo livello o gestisci diversamente
            Debug.Log("Hai completato tutti i livelli!");
        }
    }
}
