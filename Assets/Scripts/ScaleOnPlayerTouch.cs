using UnityEngine;

public class ScaleOnPlayerTouch : MonoBehaviour
{
    // Velocità di riduzione della scala
    public float scalingSpeed = 1f;

    // Flag per sapere se il player sta toccando la piattaforma
    private bool playerOnPlatform = false;

    // Memorizza la scala originale della piattaforma
    private Vector3 initialScale;
    private Vector3 targetScale = Vector3.zero; // Scala target (scomparire)

    // Flag per sapere se sta tornando alla scala iniziale
    private bool isReturningToStart = false;

    // Funzione chiamata quando un Collider entra nel trigger della piattaforma
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.GetComponent<CharacterController>())
        {
            playerOnPlatform = true;
            isReturningToStart = false; // Ferma il ritorno alla scala originale
        }
    }

    // Funzione chiamata quando un Collider esce dal trigger della piattaforma
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnPlatform = false;
            isReturningToStart = true;  // Inizia a tornare alla scala originale
        }
    }

    // Funzione che viene chiamata all'inizio
    private void Start()
    {
        // Memorizza la scala originale
        initialScale = transform.localScale;
    }

    // Funzione che viene chiamata ad ogni frame
    private void Update()
    {
        if (playerOnPlatform)
        {
            // Riduce la scala della piattaforma verso zero (scomparire)
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, scalingSpeed * Time.deltaTime);
        }
        else if (isReturningToStart)
        {
            // Torna gradualmente alla scala iniziale
            transform.localScale = Vector3.Lerp(transform.localScale, initialScale, scalingSpeed * Time.deltaTime);
        }
    }
}
