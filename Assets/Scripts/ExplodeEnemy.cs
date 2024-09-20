using UnityEngine;
using System.Collections.Generic;

public class ExplodeEnemy : MonoBehaviour
{
    public float detectionRange = 10f;           // Raggio di rilevamento del nemico
    public List<GameObject> firstExplosionPlatforms;  // Prima lista di piattaforme da distruggere
    public List<GameObject> secondExplosionPlatforms; // Seconda lista di piattaforme da distruggere
    public List<GameObject> thirdExplosionPlatforms;  // Terza lista di piattaforme da distruggere

    private Transform player;
    private int entryCount = 0;                // Conta quante volte il player entra nel raggio
    private bool hasExploded = false;          // Controlla se il nemico ha già esploso per evitare ripetizioni

    void Start()
    {
        // Trova il giocatore tramite il tag "Player"
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Se il player entra nel range e non ha ancora esploso, fai esplodere
        if (distanceToPlayer <= detectionRange && !hasExploded)
        {
            Explode();
            entryCount++;  // Incrementa il contatore per tenere traccia degli ingressi nel range
            hasExploded = true; // Impedisce che l'esplosione avvenga più di una volta senza uscita
        }
        // Reset quando il player esce dal range
        if (distanceToPlayer > detectionRange)
        {
            hasExploded = false; // Permetti un'esplosione al prossimo ingresso
        }
    }

    void Explode()
    {
        // Controlla quale lista gestire in base al numero di ingressi del player nel range
        if (entryCount == 0)
        {
            DestroyPlatforms(firstExplosionPlatforms);
        }
        else if (entryCount == 1)
        {
            DestroyPlatforms(secondExplosionPlatforms);
        }
        else if (entryCount == 2)
        {
            DestroyPlatforms(thirdExplosionPlatforms);
        }
    }

    void DestroyPlatforms(List<GameObject> platforms)
    {
        // Cicla attraverso la lista di piattaforme e le distrugge
        foreach (GameObject platform in platforms)
        {
            if (platform != null)
            {
                Destroy(platform);
            }
        }
    }

    // Visualizza il range di rilevamento del nemico nell'editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
