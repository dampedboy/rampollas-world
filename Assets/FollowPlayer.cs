using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player; // Riferimento al giocatore
    public Vector3 offset; // Offset per posizionare l'oggetto vuoto rispetto al giocatore

    void Update()
    {
        // Verifica se il giocatore è stato assegnato
        if (player != null)
        {
            // Aggiorna la posizione dell'oggetto vuoto in base alla posizione del giocatore e all'offset
            transform.position = player.position + offset;
            
            // Opzionalmente, puoi far seguire anche la rotazione del giocatore
            transform.rotation = player.rotation;
        }
    }
}