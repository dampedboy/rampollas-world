using UnityEngine;

public class SpeedBoostPlatform : MonoBehaviour
{
    public float boostSpeed = 10.0f; // La velocità aumentata quando il player è sulla piattaforma
    private float normalSpeed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                normalSpeed = playerMovement.maximumSpeed; // Salva la velocità normale
                playerMovement.maximumSpeed = boostSpeed; // Imposta la velocità aumentata
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