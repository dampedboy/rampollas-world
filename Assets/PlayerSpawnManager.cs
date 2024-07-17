using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{
    // Riferimenti alle posizioni di spawn
    public Transform doorSpawnPosition;
    public Transform trapdoorSpawnPosition;

    void Start()
    {
        // Ottieni il personaggio
        GameObject player = GameObject.FindWithTag("Player");

        // Controlla lo stato di ingresso e imposta la posizione iniziale
        if (GameManager.enteringFromTrapdoor)
        {
            player.transform.position = trapdoorSpawnPosition.position;
        }
        else
        {
            player.transform.position = doorSpawnPosition.position;
        }
    }
}