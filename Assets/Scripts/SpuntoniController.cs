using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpuntoniController : MonoBehaviour
{
    public Animator spuntoniAnimator;

    void OnTriggerEnter(Collider other)
    {
        // Controlla se il player ha toccato il bottone
        if (other.CompareTag("Player") )
        {
            spuntoniAnimator.SetTrigger("Up"); // Attiva l'animazione del bottone
        }
    }
}
