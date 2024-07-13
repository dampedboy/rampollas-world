using StarterAssets;
using UnityEngine;

public class SpeedBoostPlatform : MonoBehaviour
{
    public float boostSpeed = 10.0f; // La velocità aumentata quando il player è sulla piattaforma
    private float normalSpeed;
    private float normalSprintSpeed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ThirdPersonController playerController = other.GetComponent<ThirdPersonController>();
            if (playerController != null)
            {
                normalSpeed = playerController.MoveSpeed; // Salva la velocità normale
                normalSprintSpeed = playerController.SprintSpeed; // Salva la velocità di sprint normale
                playerController.MoveSpeed = boostSpeed; // Imposta la velocità aumentata
                playerController.SprintSpeed = boostSpeed; // Imposta la velocità di sprint aumentata
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ThirdPersonController playerController = other.GetComponent<ThirdPersonController>();
            if (playerController != null)
            {
                playerController.MoveSpeed = normalSpeed; // Ripristina la velocità normale
                playerController.SprintSpeed = normalSprintSpeed; // Ripristina la velocità di sprint normale
            }
        }
    }
}
