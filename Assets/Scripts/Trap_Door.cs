using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Door : MonoBehaviour
{
   
    public Animator doorAnim;
    public Transform door;
    public Transform player;

    public AudioClip trapdoorCloseSound; // Aggiungi questa riga per l'audio clip
    private AudioSource audioSource; // Aggiungi questa riga per l'AudioSource

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>(); // Inizializza l'AudioSource
    }

    void Update()
    {
        float distance = Vector3.Distance(player.position,door.position);
        if (distance<=6){
            doorAnim.SetBool("Near",true);
            PlayTrapdoorCloseSound(); // Riproduce il suono di apertura botola
        }
        else
        {
            doorAnim.SetBool("Near",false);
        }
    }

    void PlayTrapdoorCloseSound()
    {
        audioSource.clip = trapdoorCloseSound;
        audioSource.Play();
    }

}
