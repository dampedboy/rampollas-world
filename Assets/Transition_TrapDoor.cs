using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Transition_TrapDoor : MonoBehaviour
{
    public string sceneName;

    void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            GameManager.JustNo();
            GameManager.EnterFromTrapdoor();
            SceneManager.LoadScene(sceneName);
        }
    }
}
