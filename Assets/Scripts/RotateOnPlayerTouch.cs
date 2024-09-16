using UnityEngine;

public class RotateOnPlayerTouch : MonoBehaviour
{
    // Variabili per la velocità di rotazione e l'angolo di rotazione target
    public float rotationSpeed = 100f;
    private bool playerOnPlatform = false;

    // Variabili per la rotazione
    private Quaternion initialRotation; // Memorizza la rotazione iniziale
    private Quaternion targetRotation;  // Rotazione target
    private float currentAngle = 0f;    // Angolo corrente della rotazione

    // Flag per sapere se sta tornando alla posizione iniziale
    private bool isReturningToStart = false;

    // Funzione chiamata quando un Collider entra nel trigger della piattaforma
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.GetComponent<CharacterController>())
        {
            playerOnPlatform = true;
            isReturningToStart = false; // Ferma il ritorno alla posizione iniziale
            SetTargetRotation(90);      // Imposta la rotazione target a 90 gradi
        }
    }

    // Funzione chiamata quando un Collider esce dal trigger della piattaforma
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnPlatform = false;
            isReturningToStart = true;  // Inizia a tornare alla posizione iniziale
        }
    }

    // Funzione per impostare la rotazione target
    private void SetTargetRotation(float angle)
    {
        targetRotation = Quaternion.Euler(initialRotation.eulerAngles.x, initialRotation.eulerAngles.y, initialRotation.eulerAngles.z + angle);
    }

    // Funzione che viene chiamata all'inizio
    private void Start()
    {
        // Memorizza la rotazione iniziale
        initialRotation = transform.rotation;
        targetRotation = initialRotation; // Inizia senza target
    }

    // Funzione che viene chiamata ad ogni frame
    private void Update()
    {
        if (playerOnPlatform)
        {
            // Rotazione verso la rotazione target
            if (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
            {
                // Interpola la rotazione
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
        else if (isReturningToStart)
        {
            // Rotazione verso la posizione iniziale
            if (Quaternion.Angle(transform.rotation, initialRotation) > 0.1f)
            {
                // Interpola la rotazione verso la rotazione iniziale
                transform.rotation = Quaternion.RotateTowards(transform.rotation, initialRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }
}
