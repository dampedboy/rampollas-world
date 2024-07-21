using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 moveToPosition; // La posizione verso cui muoversi
    public float moveSpeed = 2.0f; // Velocità di movimento
    public float returnDelay = 2.0f; // Tempo di ritardo prima di tornare alla posizione originale
    public AudioClip spuntoniSound; // Suono da riprodurre per i spuntoni
    public AudioClip movingPlatformSound; // Suono da riprodurre per la moving platform

    private Vector3 originalPosition;
    private bool movingToTarget = false;
    private Transform playerTransform;
    private Vector3 lastPlatformPosition;
    private AudioSource audioSource;

    void Start()
    {
        originalPosition = transform.position;
        lastPlatformPosition = transform.position;
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerTransform = other.transform;
            movingToTarget = true;
            StopAllCoroutines();
            StartCoroutine(MovePlatform(moveToPosition));
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerTransform = null;
            movingToTarget = false;
            StopAllCoroutines();
            StartCoroutine(ReturnToOriginalPosition());
        }
    }

    void LateUpdate()
    {
        if (playerTransform != null)
        {
            Vector3 platformMovement = transform.position - lastPlatformPosition;
            playerTransform.position += platformMovement;
        }
        lastPlatformPosition = transform.position;

        // Controlla se la piattaforma ha smesso di muoversi e ferma il suono
        if (!movingToTarget && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    IEnumerator MovePlatform(Vector3 targetPosition)
    {
        if (gameObject.tag != "Spuntoni" && movingToTarget && playerTransform != null)
        {
            PlayMovingPlatformSound();
        }
        else if (gameObject.tag == "Spuntoni")
        {
            PlaySpuntoniSound();
        }

        while (movingToTarget && Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // Ferma il suono quando la piattaforma smette di muoversi
        if (!movingToTarget)
        {
            audioSource.Stop();
        }
    }

    IEnumerator ReturnToOriginalPosition()
    {
        if (gameObject.tag != "Spuntoni"  && playerTransform != null)
        {
            PlayMovingPlatformSound();
        }


        yield return new WaitForSeconds(returnDelay);

        while (!movingToTarget && Vector3.Distance(transform.position, originalPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }


    }

    void PlaySpuntoniSound()
    {
        if (spuntoniSound != null)
        {
            audioSource.clip = spuntoniSound;
            audioSource.Play();
        }
    }

    void PlayMovingPlatformSound()
    {
        if (movingPlatformSound != null)
        {
            audioSource.clip = movingPlatformSound;
            audioSource.Play();
        }
    }
}
