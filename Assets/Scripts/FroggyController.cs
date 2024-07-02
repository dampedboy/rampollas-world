using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FroggyController : MonoBehaviour
{
    private Animator _animator;

    private bool wake = false;

    private bool isPlayerInside = false;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }


    void Update()
    {

        if (isPlayerInside && Input.GetKeyDown(KeyCode.O))
            Wake();
        if (!isPlayerInside)
            Sleep();
    }

    // Metodo chiamato quando un altro collider entra nel trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
        }
    }

    // Metodo chiamato quando un altro collider esce dal trigger
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
        }
    }

    // Metodo per aprire l'animazione e la UI
    public void Wake()
    {
        if (_animator == null)
            return;

        wake = true;

        _animator.SetBool("wake_up", wake);
    }

    public void Sleep()
    {
        if (_animator == null)
            return;

        wake = false;

        _animator.SetBool("wake_up", wake);

    }
}
