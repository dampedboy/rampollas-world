using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookController : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private GameObject uiPanel; // Il pannello della UI
    [SerializeField] private GameObject uiPanel_talk; // Il pannello della UI talk

    private bool _open = false;
    private bool isPlayerInside = false;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip openSound;
    [SerializeField] private AudioClip closeSound;

    void Start()
    {
        _animator = GetComponent<Animator>();

        if (uiPanel != null)
        {
            uiPanel.SetActive(false);
            uiPanel_talk.SetActive(false);
        }
    }

    void Update()
    {
        if (isPlayerInside)
        {
            if (uiPanel_talk != null)
            {
                uiPanel_talk.SetActive(true);
            }
            

            if (Input.GetKeyDown(KeyCode.C))
                Open();
        }
        else
        {
            if (uiPanel_talk != null)
            {
                uiPanel_talk.SetActive(false);
            }
            
        }

        if (Input.GetKeyDown(KeyCode.T))
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

        if (audioSource != null && openSound != null)
        {
            audioSource.clip = openSound;
            audioSource.Play();
        }

        if (uiPanel != null)
        {
            uiPanel.SetActive(true);
        }
    }

    // Metodo per chiudere l'animazione e la UI
    public void Close()
    {
        if (_animator == null)
            return;

        _open = false;
        _animator.SetBool("open", _open);

        if (audioSource != null && closeSound != null)
        {
            audioSource.clip = closeSound;
            audioSource.Play();
        }

        if (uiPanel != null)
        {
            uiPanel.SetActive(false);
        }
    }
}

