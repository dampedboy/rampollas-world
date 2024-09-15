using UnityEngine;
using System.Collections.Generic;

public class TeleportEnemy : MonoBehaviour
{
    public float detectionRange = 10f;
    public List<Vector3> teleportPositions;        // Lista di posizioni di teletrasporto del nemico
    public List<Vector3> teleportRotations;        // Lista di rotazioni di teletrasporto del nemico
    public List<Vector3> keyTeleportPositions;     // Lista di posizioni di teletrasporto della chiave
    public List<Vector3> keyTeleportRotations;     // Lista di rotazioni di teletrasporto della chiave
    public GameObject keyObject;                   // Oggetto chiave da portare nel teletrasporto

    private Transform player;
    private float nextTeleportTime = 0f;
    public float teleportCooldown = 5f;            // Tempo di attesa tra i teletrasporti
    private int currentTeleportIndex = 0;          // Indice per tenere traccia del punto di teletrasporto corrente

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
        // Controllo che la lista delle posizioni e delle rotazioni non sia vuota
        if (teleportPositions.Count == 0 || teleportRotations.Count == 0 ||
            keyTeleportPositions.Count == 0 || keyTeleportRotations.Count == 0) return;

        // Assicurarsi che le liste abbiano lo stesso numero di elementi
        if (teleportPositions.Count != teleportRotations.Count ||
            keyTeleportPositions.Count != keyTeleportRotations.Count ||
            teleportPositions.Count != keyTeleportPositions.Count) return;

        // Prendere la posizione e la rotazione attuale dalla lista per il nemico
        Vector3 targetPosition = teleportPositions[currentTeleportIndex];
        Vector3 targetRotation = teleportRotations[currentTeleportIndex];

        // Teletrasportare il nemico alla nuova posizione e rotazione
        transform.position = targetPosition;
        transform.rotation = Quaternion.Euler(targetRotation);

        // Prendere la posizione e la rotazione attuale dalla lista per la chiave
        Vector3 keyTargetPosition = keyTeleportPositions[currentTeleportIndex];
        Vector3 keyTargetRotation = keyTeleportRotations[currentTeleportIndex];

        // Teletrasportare l'oggetto chiave alla posizione e rotazione specifica
        if (keyObject != null)
        {
            keyObject.transform.position = keyTargetPosition;
            keyObject.transform.rotation = Quaternion.Euler(keyTargetRotation);
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
