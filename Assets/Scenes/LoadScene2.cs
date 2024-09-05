using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadScene2 : MonoBehaviour
{
    // Start is called before the first frame update
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Fire2"))
        {
            SceneManager.LoadScene("TrainingButtonRush");
        }
    }
}
