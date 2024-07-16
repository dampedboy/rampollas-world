using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    //private void Start()
    //{
        //MusicManager.Instance.PlayMusic("MainMenu");
    //}

    public void Play()
    {
        StartCoroutine(LoadHub());
        //MusicManager.Instance.PlayMusic("MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }
    
    IEnumerator LoadHub()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);
        
        SceneManager.LoadScene("Hub");
    }
}
