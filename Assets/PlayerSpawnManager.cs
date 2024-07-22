using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{
    // Riferimenti alle posizioni di spawn
    public Transform doorSpawnPosition;
    public Transform trapdoorSpawnPosition;

    public Transform ColorPosition;
    public Transform ActionPosition;
    public Transform BreakPosition;
    public Transform ButtonPosition;
    public Transform PortalPosition;


    void Start()
    {
        // Ottieni il personaggio
        GameObject player = GameObject.FindWithTag("Player");

        if(GameManager.enteringFromColor) player.transform.position = ColorPosition.position;
        if(GameManager.enteringFromAction) player.transform.position = ActionPosition.position;
        if(GameManager.enteringFromBreak) player.transform.position = BreakPosition.position;
        if(GameManager.enteringFromButton) player.transform.position = ButtonPosition.position;
        if(GameManager.enteringFromPortal) player.transform.position = PortalPosition.position;


        // Controlla lo stato di ingresso e imposta la posizione iniziale
        if (GameManager.enteringFromTrapdoor)
        {
            player.transform.position = trapdoorSpawnPosition.position;
        }
        else
        {
            if (doorSpawnPosition != null)
            {
                player.transform.position = doorSpawnPosition.position;
            }
            
        }
    }
}