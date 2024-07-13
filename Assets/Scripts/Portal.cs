using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    private BoxCollider boxCollider;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        // Assicurati che il box collider sia disattivato all'inizio
        boxCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (boxCollider.isTrigger && other.CompareTag("Player"))
        {
            LoadNextLevel();
        }
    }

    private void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

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