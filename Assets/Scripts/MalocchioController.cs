using UnityEngine;

public class MalocchioController : MonoBehaviour
{
    public Transform respawnPoint; // Punto di respawn (Empty game object)
    public GameObject player; // Riferimento al Player
    public Transform keyObject; // Riferimento all'oggetto chiave
    public Transform cameraPosition; // Nuova posizione per la Main Camera

    private float activationDistance = 3f; // Distanza di attivazione del portale

    void Update()
    {
        // Controlla la distanza tra il portale e l'oggetto chiave
        float keyDistance = Vector3.Distance(transform.position, keyObject.position);

        if (keyDistance <= activationDistance)
        {
            // Distruggi l'oggetto chiave
            Destroy(keyObject.gameObject);

            // Respawna il Player al punto di respawn
            player.transform.position = respawnPoint.position;

            // Sposta la Main Camera nella nuova posizione
            Camera.main.transform.position = cameraPosition.position;
            Camera.main.transform.rotation = cameraPosition.rotation;
        }
    }
}