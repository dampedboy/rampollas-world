using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 targetPosition; // La posizione di destinazione della piattaforma
    private Vector3 originalPosition; // La posizione originale della piattaforma
    private bool playerOnPlatform = false; // Indica se il player è sulla piattaforma

    private void Start()
    {
        originalPosition = transform.position; // Salva la posizione originale della piattaforma
    }

    public void StartMovingToTarget()
    {
        if (!playerOnPlatform)
        {
            playerOnPlatform = true;
            StopAllCoroutines();
            StartCoroutine(MovePlatform(targetPosition));
        }
    }

    public void StartMovingToOriginal()
    {
        if (playerOnPlatform)
        {
            playerOnPlatform = false;
            StopAllCoroutines();
            StartCoroutine(MovePlatform(originalPosition));
        }
    }

    private IEnumerator MovePlatform(Vector3 destination)
    {
        float speed = 2f; // Velocità di movimento della piattaforma
        while ((transform.position - destination).sqrMagnitude > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            yield return null; // Attende il frame successivo
        }

        transform.position = destination; // Assicura che la piattaforma raggiunga esattamente la posizione di destinazione
    }
}