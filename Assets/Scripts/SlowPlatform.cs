using UnityEngine;

public class SlowPlatform : MonoBehaviour
{
    public float slowSpeed = 1.0f; // La velocità ridotta quando il player è sulla piattaforma
    private float normalSpeed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                normalSpeed = playerMovement.maximumSpeed; // Salva la velocità normale
                playerMovement.maximumSpeed = slowSpeed; // Imposta la velocità ridotta
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.maximumSpeed = normalSpeed; // Ripristina la velocità normale
            }
        }
    }
}