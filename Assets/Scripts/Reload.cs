using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadSceneOnKeyPress : MonoBehaviour
{
    public static bool isSceneReloaded = false; // Variabile statica per tracciare se la scena è stata ricaricata

    void Update()
    {
        // Controlla se il tasto "R" viene premuto
        if (Input.GetKeyDown(KeyCode.R) || Input.GetButtonDown("Joystick Button 5"))
        {
            // Imposta la variabile a true quando la scena viene ricaricata
            isSceneReloaded = true;

            // Ricarica la scena corrente
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
        }
    }
}
