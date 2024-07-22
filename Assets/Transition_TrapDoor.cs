using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Transition_TrapDoor : MonoBehaviour
{
    public string sceneName;
    
    public Animator transition;
    public float transitionTime = 1f;

    void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            GameManager.JustNo();
            GameManager.EnterFromTrapdoor();
            SceneManager.LoadScene(sceneName);
            //StartCoroutine(LoadLevel(sceneName));
        }
    }
    
    IEnumerator LoadLevel(string sceneName)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);
        
        SceneManager.LoadScene(sceneName);
    }
}
