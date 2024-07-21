using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LOADING_SCENE : MonoBehaviour
{
    public string sceneName;

    void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            SceneManager.LoadScene(sceneName);
        }
    }
}