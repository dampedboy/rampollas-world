using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform destinationPortal;  // Riferimento all'altro portale

    private void OnTriggerEnter(Collider other)
    {
        // Verifica se l'oggetto che entra nel portale è il player
        if (other.CompareTag("Player"))
        {
            // Teletrasporta il player all'altro portale
            TeleportPlayer(other.gameObject);
        }

        if (other.CompareTag("Key"))
        {
            // Teletrasporta la chiave all'altro portale
            TeleportKey(other.gameObject);
        }
    }

    private void TeleportPlayer(GameObject player)
    {
        // Posiziona il player al punto del portale di destinazione
        CharacterController characterController = player.GetComponent<CharacterController>();

        if (characterController != null)
        {
            // Disattiva momentaneamente il CharacterController per evitare problemi di collisione
            characterController.enabled = false;

            // Teletrasporta il player al portale di destinazione
            player.transform.position = destinationPortal.position;

            // Riattiva il CharacterController
            characterController.enabled = true;
        }
        else
        {
            // Se il player non ha un CharacterController, semplicemente cambia la posizione
            player.transform.position = destinationPortal.position;
        }
    }

    private void TeleportKey(GameObject key)
    {
        // Posiziona la chiave al punto del portale di destinazione
        Collider keyCollider = key.GetComponent<Collider>();

        if (keyCollider != null)
        {
            // Disattiva momentaneamente il Collider per evitare problemi di collisione
            keyCollider.enabled = false;

            // Teletrasporta la chiave al portale di destinazione
            key.transform.position = destinationPortal.position;

            // Riattiva il Collider
            keyCollider.enabled = true;
        }
        else
        {
            // Se la chiave non ha un Collider, semplicemente cambia la posizione
            key.transform.position = destinationPortal.position;
        }
    }
}
