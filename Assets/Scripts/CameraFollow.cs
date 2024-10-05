using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Riferimento al giocatore
    public Transform playerTransform;

    // Distanza offset tra il giocatore e la camera
    public Vector3 offset;

    // Velocità di smorzamento per un movimento fluido
    public float smoothSpeed = 0.125f;

    // Limite per identificare quando il giocatore si muove "indietro"
    public float rotationThreshold = -0.1f; // Movimento sull'asse Z negativo

    // Rotazione della camera per guardare dietro
    public Vector3 backViewRotation = new Vector3(30, 180, 0); // Un esempio di rotazione per "dietro"

    // Rotazione standard (davanti)
    public Vector3 frontViewRotation = new Vector3(30, 0, 0); // Rotazione di default

    void Start()
    {
        // Se non è stato impostato l'offset, calcola l'offset iniziale
        if (offset == Vector3.zero)
        {
            offset = transform.position - playerTransform.position;
        }
    }

    void LateUpdate()
    {
        // Calcola la posizione desiderata della camera in base alla posizione del giocatore
        Vector3 desiredPosition = playerTransform.position + offset;

        // Movimento fluido verso la posizione desiderata
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Aggiorna la posizione della camera
        transform.position = smoothedPosition;

        // Verifica se il player si sta muovendo indietro o avanti (asse Z)
        if (playerTransform.forward.z < rotationThreshold) // Se si muove indietro
        {
            // Ruota la camera per guardare "dietro"
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(backViewRotation), smoothSpeed);
        }
        else
        {
            // Torna alla rotazione standard (davanti)
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(frontViewRotation), smoothSpeed);
        }
    }
}
