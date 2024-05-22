using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    private bool firstSentence = false;
    private bool continueDialogue = false;
    //public static bool isMoving;

    private void Update()
    {
        
        if (!continueDialogue && firstSentence && Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("Sono nel primo if");
            
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            firstSentence = false;
            continueDialogue = true;
            return;
        }

        if (continueDialogue && !firstSentence && Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("Sono nel secondo if");
            FindObjectOfType<DialogueManager>().DisplayNextSentence();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            firstSentence = true;
            //isMoving = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            firstSentence = false;
            continueDialogue = false;
            //isMoving = true;
        }
    }
}
