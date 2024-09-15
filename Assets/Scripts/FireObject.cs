using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireObject : MonoBehaviour
{
    public AudioClip breakSound; // Reference to the audio clip
    private AudioSource audioSource; // Reference to the audio source component
    private MeshRenderer meshRenderer; // Reference to the MeshRenderer component

    void Start()
    {
        // Add an AudioSource component to the game object if it doesn't already have one
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Get the MeshRenderer component
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject otherObject = collision.gameObject;

        if (otherObject.CompareTag("MetalBlock") || otherObject.CompareTag("SteelBlock")|| otherObject.CompareTag("WoodBlock") || otherObject.CompareTag("GlassBlock"))
        {
            // Disable the MeshRenderer immediately
            if (meshRenderer != null)
            {
                meshRenderer.enabled = false;
            }

            // Play the break sound if available and start the coroutine to destroy the object
            if (breakSound != null)
            {
                audioSource.PlayOneShot(breakSound);
                StartCoroutine(DestroyAfterSound());
            }
            else
            {
                Destroy(gameObject);
            }
        }
       
    }

    private IEnumerator DestroyAfterSound()
    {
        // Wait for the sound to finish playing
        yield return new WaitForSeconds(breakSound.length);

        // Destroy the game object
        Destroy(gameObject);
    }
}
