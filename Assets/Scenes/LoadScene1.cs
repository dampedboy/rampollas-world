using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadScene1 : MonoBehaviour
{
    // Start is called before the first frame update
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene("TrainingBreakRush");
        }
    }
}
