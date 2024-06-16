using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookController : MonoBehaviour
{
    private Animator _animator;

    [SerializeField] GameObject uiPanel; // Il pannello della UI


    private bool _open = false;

    private bool isPlayerInside = false;

    void Start()
    {
        _animator = GetComponent<Animator>();

        if (uiPanel != null)
        {
            uiPanel.SetActive(false);
        }
    }


    void Update()
    {


        if (isPlayerInside && Input.GetKeyDown(KeyCode.O))
            Open();
        if ( Input.GetKeyDown(KeyCode.C))
            Close();
        if (!isPlayerInside)
            Close();
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
    public void Open()
    {
        if (_animator == null)
            return;

        _open = true;


        _animator.SetBool("open", _open);

        if (uiPanel != null)
        {
            uiPanel.SetActive(true);
        }

    }

    public void Close()
    {
        if (_animator == null)
            return;

        _open = false;

        _animator.SetBool("open", _open);


        if (uiPanel != null)
        {
            uiPanel.SetActive(false);
        }

    }

}
