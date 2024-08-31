using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadScene3 : MonoBehaviour
{
    // Start is called before the first frame update
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene("TrainingPortalRush");
        }
    }
}
