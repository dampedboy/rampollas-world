using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PORTAL_TRANSITION : MonoBehaviour
{
    public string sceneName;

    void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            GameManager.EnterFromDoor();
            if(sceneName == "TrainingColorRush") GameManager.EnterFromColor();
            if(sceneName == "TrainingActionRush") GameManager.EnterFromAction();
            if(sceneName == "TrainingBreakRush") GameManager.EnterFromBreak();
            if(sceneName == "TrainingButtonRush") GameManager.EnterFromButton();
            if(sceneName == "TrainingActionRush") GameManager.EnterFromPortal();
            
            SceneManager.LoadScene(sceneName);
        }
    }
}
