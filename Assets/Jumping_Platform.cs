using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping_Platform : MonoBehaviour
{
    public float jumpForce = 15f; // Forza del salto

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();

            if (playerMovement != null)
            {
                // Modifica la velocità verticale del player per fargli eseguire un salto
                playerMovement.BoostJump(jumpForce);
            }
        }
    }
}
