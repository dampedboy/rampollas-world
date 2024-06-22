using UnityEngine;

public class PositionEmpty : MonoBehaviour
{
    public Transform player; // Riferimento al Transform del player
    public Vector3 offset; // Offset dalla testa del player

    void Start()
    {
        // Imposta la posizione dello Empty
        transform.position = player.position + offset;
    }

    void Update()
    {
        // Aggiorna la posizione dello Empty se necessario
        transform.position = player.position + offset;
    }
}
