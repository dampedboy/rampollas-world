using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    private int portalLevel = 0;


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

    public void UpdatePortal()
    {
        portalLevel ++;

        Debug.Log("Livello attuale del portale: " + portalLevel);
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
        // Calcola l'indice del prossimo livello
        

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