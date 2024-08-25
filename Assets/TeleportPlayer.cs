using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    public Vector3 teleportPosition;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered by: " + other.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player detected, teleporting...");

            // Disabilita temporaneamente il CharacterController se presente
            CharacterController controller = other.GetComponent<CharacterController>();
            if (controller != null)
            {
                controller.enabled = false;
            }

            // Teletrasporta il giocatore
            other.transform.position = teleportPosition;

            // Riabilita il CharacterController dopo il teletrasporto
            if (controller != null)
            {
                controller.enabled = true;
            }

            Debug.Log("Player teleported to: " + teleportPosition);
        }
    }
}