using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Riferimento al giocatore
    private Transform playerTransform;

    // Distanza offset tra il giocatore e la camera
    public Vector3 offset;

    // Velocità di smorzamento per movimenti più fluidi
    public float smoothSpeed = 0.125f;

    // Riferimenti ai limiti della stanza
    public Transform leftBoundary;   // Limite sinistro
    public Transform rightBoundary;  // Limite destro
    public Transform frontBoundary;  // Limite davanti
    public Transform backBoundary;   // Limite per evitare il vuoto (dietro)

    void Start()
    {
        // Trova il giocatore per tag
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        // Se non hai specificato l'offset dall'Inspector, lo calcola in base alla posizione iniziale
        if (offset == Vector3.zero)
        {
            offset = transform.position - playerTransform.position;
        }
    }

    void LateUpdate()
    {
        // Calcola la posizione desiderata della camera
        Vector3 desiredPosition = playerTransform.position + offset;

        // Applica un movimento fluido alla camera
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Blocca la posizione della camera entro i limiti della stanza
        float clampedX = Mathf.Clamp(smoothedPosition.x, leftBoundary.position.x, rightBoundary.position.x);
        float clampedY = Mathf.Clamp(smoothedPosition.y, backBoundary.position.y, frontBoundary.position.y);

        // Aggiorna la posizione della camera con i valori limitati
        transform.position = new Vector3(clampedX, clampedY, transform.position.z); // Mantieni l'asse Y invariato

        // (Opzionale) Mantieni la camera sempre orientata in una direzione fissa o verso il giocatore
        transform.LookAt(playerTransform);
    }
}
