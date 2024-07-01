using UnityEngine;

public class PlatformTargetController : MonoBehaviour
{
    public GameObject block;             // Il blocco che deve muoversi
    public Transform originalPosition;   // La posizione originale del blocco
    public Transform platformPosition;   // La posizione della piattaforma dove il blocco deve scendere
    public float moveSpeed = 2.0f;       // La velocità di movimento del blocco

    private Vector3 targetPosition;      // La posizione di destinazione del blocco
    private bool playerOnPlatform = false; // Indica se il player è sulla piattaforma

    void Start()
    {
        // Salva la posizione iniziale del blocco
        targetPosition = originalPosition.position;
    }

    void Update()
    {
        // Muove il blocco verso la posizione di destinazione
        block.transform.position = Vector3.Lerp(block.transform.position, targetPosition, Time.deltaTime * moveSpeed);
    }

    void OnTriggerEnter(Collider other)
    {
        // Se il player entra in contatto con la piattaforma
        if (other.CompareTag("Player"))
        {
            playerOnPlatform = true;
            targetPosition = platformPosition.position;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Se il player esce dal contatto con la piattaforma
        if (other.CompareTag("Player"))
        {
            playerOnPlatform = false;
            targetPosition = originalPosition.position;
        }
    }
}