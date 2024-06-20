using StarterAssets;
using UnityEngine;

public class SlowPlatform : MonoBehaviour
{
    public float slowSpeed = 1.0f; // La velocità ridotta quando il player è sulla piattaforma
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
                playerController.MoveSpeed = slowSpeed; // Imposta la velocità ridotta
                playerController.SprintSpeed = slowSpeed; // Imposta la velocità di sprint ridotta
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