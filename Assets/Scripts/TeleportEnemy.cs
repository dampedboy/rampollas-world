using UnityEngine;
using System.Collections.Generic;

public class TeleportEnemy : MonoBehaviour
{
    public float detectionRange = 10f;
    public List<Vector3> teleportPositions;  // Lista di posizioni di teletrasporto da inserire nell'inspector
    public List<Vector3> teleportRotations;  // Lista di rotazioni di teletrasporto da inserire nell'inspector
    public GameObject keyObject;             // Oggetto chiave da portare nel teletrasporto

    private Transform player;
    private float nextTeleportTime = 0f;
    public float teleportCooldown = 5f;      // Tempo di attesa tra i teletrasporti
    private int currentTeleportIndex = 0;    // Indice per tenere traccia del punto di teletrasporto corrente

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Teletrasporto attivato solo quando il player è nel raggio di rilevamento
        if (distanceToPlayer <= detectionRange && Time.time >= nextTeleportTime)
        {
            Teleport();
            nextTeleportTime = Time.time + teleportCooldown; // Impostiamo il cooldown per il prossimo teletrasporto
        }
    }

    void Teleport()
    {
        if (teleportPositions.Count == 0 || teleportRotations.Count == 0) return;  // Se non ci sono punti di teletrasporto, non fare nulla

        // Prendere la posizione e la rotazione attuale dalla lista
        Vector3 targetPosition = teleportPositions[currentTeleportIndex];
        Vector3 targetRotation = teleportRotations[currentTeleportIndex];

        // Teletrasportare il nemico alla nuova posizione e rotazione
        transform.position = targetPosition;
        transform.rotation = Quaternion.Euler(targetRotation);

        // Teletrasportare anche l'oggetto chiave alla stessa posizione e rotazione del nemico
        if (keyObject != null)
        {
            keyObject.transform.position = targetPosition;
            keyObject.transform.rotation = Quaternion.Euler(targetRotation);
        }

        // Aggiornare l'indice per il prossimo punto di teletrasporto ciclico
        currentTeleportIndex = (currentTeleportIndex + 1) % teleportPositions.Count;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
