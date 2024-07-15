using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookController : MonoBehaviour
{
    private Animator _animator;
    private AudioSource _audioSource;

    [SerializeField] GameObject uiPanel; // Il pannello della UI
    [SerializeField] GameObject uiPanel_talk; // Il pannello della UI talk
    [SerializeField] AudioClip openCloseSound; // Suono di apertura e chiusura del libro

    private bool _open = false;
    private bool isPlayerInside = false;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();

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
            uiPanel_talk.SetActive(true);
        }
        if (isPlayerInside && Input.GetKeyDown(KeyCode.O))
            Open();
        if (Input.GetKeyDown(KeyCode.C))
            Close();
        if (!isPlayerInside)
        {
            Close();
            uiPanel_talk.SetActive(false);
        }
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

        // Riproduci il suono di apertura
        if (_audioSource != null && openCloseSound != null)
        {
            _audioSource.PlayOneShot(openCloseSound);
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

        // Riproduci il suono di chiusura
        if (_audioSource != null && openCloseSound != null)
        {
            _audioSource.PlayOneShot(openCloseSound);
        }

        if (uiPanel != null)
        {
            uiPanel.SetActive(false);
        }
    }
}
