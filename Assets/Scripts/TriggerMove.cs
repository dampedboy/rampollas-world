using UnityEngine;

public class TriggerMove : MonoBehaviour
{
    private MovingPlatform movingPlatform;

    private void Start()
    {
        movingPlatform = GetComponentInParent<MovingPlatform>(); // Ottieni il riferimento alla piattaforma
    }

    private void OnTriggerEnter(Collider other)
    {
        CharacterController characterController = other.GetComponent<CharacterController>();

        if (characterController != null)
        {
            movingPlatform.StartMovingToTarget();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CharacterController characterController = other.GetComponent<CharacterController>();

        if (characterController != null)
        {
            movingPlatform.StartMovingToOriginal();
        }
    }
}