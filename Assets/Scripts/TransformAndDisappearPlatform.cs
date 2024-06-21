using UnityEngine;

public class TransformAndDisappearPlatform : MonoBehaviour
{
    public GameObject newModel; // Modello 3D della nuova piattaforma
    private GameObject currentModel; // Modello 3D corrente
    private bool isTransformed = false;

    private void Start()
    {
        currentModel = gameObject; // Imposta il modello corrente come l'oggetto iniziale
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isTransformed)
            {
                TransformPlatform();
            }
            else
            {
                DisappearPlatform();
            }
        }
    }

    private void TransformPlatform()
    {
        // Distruggi il modello corrente
        Destroy(currentModel);

        // Instanzia il nuovo modello nella stessa posizione e rotazione
        currentModel = Instantiate(newModel, transform.position, transform.rotation);

        // Aggiorna lo stato
        isTransformed = true;
    }

    private void DisappearPlatform()
    {
        // Distruggi il modello corrente
        Destroy(currentModel);

        // Distruggi anche il GameObject della piattaforma per farla scomparire
        Destroy(gameObject);
    }
}