using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class Portal : MonoBehaviour
{

    private BoxCollider boxCollider;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        // Assicurati che il box collider sia disattivato all'inizio
        boxCollider.enabled = false;
    }


    private int portalLevel = 0;
    public TMP_Text portalLevelText;

    // Metodo chiamato quando un altro collider entra nel trigger

    private void OnTriggerEnter(Collider other)
    {
        if (boxCollider.isTrigger && other.CompareTag("Player"))
        {
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
        if (CoinManager.CoinCount >= 10)
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
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        int nextSceneIndex = currentSceneIndex + 1;


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


        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("Hai completato tutti i livelli!");
        }
    }


    // Meto pubblico per attivare il box collider
    public void ActivateCollider()
    {
        boxCollider.enabled = true;
    }
}

}

