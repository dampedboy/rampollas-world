using UnityEngine;

public class MalocchioController : MonoBehaviour
{
    public Transform respawnPoint; // Punto di respawn (Empty game object)
    public GameObject player; // Riferimento al Player
    public Transform keyObject; // Riferimento all'oggetto chiave

    private float activationDistance = 3f; // Distanza di attivazione del portale

    // Coordinate esatte per la nuova posizione della Main Camera
    public Vector3 cameraNewPosition = new Vector3(0f, 10f, -10f);
    

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
            Camera.main.transform.position = cameraNewPosition;
            
        }
    }
}