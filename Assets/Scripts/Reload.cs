using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadSceneOnKeyPress : MonoBehaviour
{
    void Update()
    {
        // Controlla se il tasto "R" viene premuto
        if (Input.GetKeyDown(KeyCode.R))
        {
            // Ricarica la scena corrente
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
        }
    }
}
