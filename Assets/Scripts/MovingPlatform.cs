using UnityEngine;
using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 moveToPosition; // La posizione verso cui muoversi
    public float moveSpeed = 2.0f; // Velocità di movimento
    public float returnDelay = 2.0f; // Tempo di ritardo prima di tornare alla posizione originale

    private Vector3 originalPosition;
    private bool movingToTarget = false;
    private Transform playerTransform;
    private Vector3 lastPlatformPosition;

    void Start()
    {
        originalPosition = transform.position;
        lastPlatformPosition = transform.position;
        // Ensure the platform has a trigger collider
        Collider platformCollider = GetComponent<Collider>();
        if (platformCollider != null)
        {
            platformCollider.isTrigger = true;
        }
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
    }

    IEnumerator MovePlatform(Vector3 targetPosition)
    {
        while (movingToTarget && Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator ReturnToOriginalPosition()
    {
        yield return new WaitForSeconds(returnDelay);

        while (!movingToTarget && Vector3.Distance(transform.position, originalPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }
}