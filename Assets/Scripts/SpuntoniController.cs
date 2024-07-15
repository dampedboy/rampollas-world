using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpuntoniController : MonoBehaviour
{
    private Animator _animator;
    private AudioSource _audioSource;


    [SerializeField] AudioClip triggerSpuntoniSound; // Suono di apertura e chiusura del libro

    private bool _up = false;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();

    }



    // Metodo chiamato quando un altro collider entra nel trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_animator == null)
                return;

            _up = true;
            _animator.SetBool("up", _up);

            // Riproduci il suono di apertura
            if (_audioSource != null && triggerSpuntoniSound != null)
            {
                Debug.Log("sound spuntoni");
                _audioSource.PlayOneShot(triggerSpuntoniSound);
            }
        }
    }


}
