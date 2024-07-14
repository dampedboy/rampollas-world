using UnityEngine;

public class PlatformTargetController : MonoBehaviour
{
    // Oggetto da assegnare nel Inspector per il blocco
    public GameObject block;

    // Trasform del blocco (posizione di partenza)
    public Transform OriginalPosition;

    // Trasform della piattaforma (posizione di arrivo)
    public Transform PlatformPosition;

    // Variabile per determinare se il blocco è stato rilasciato
    private bool isTriggered = false;

    // Velocità di caduta del blocco
    public float dropSpeed = 7.0f;



    void Start()
    {
        // Imposta la posizione iniziale del blocco
        block.transform.position = OriginalPosition.position;
        // Inizializza il timer di ritardo
    }

    void Update()
    {
        if (isTriggered)
        {


                // Movimento del blocco verso la posizione della piattaforma
                block.transform.position = Vector3.MoveTowards(block.transform.position, PlatformPosition.position, dropSpeed * Time.deltaTime);

                // Verifica se il blocco ha raggiunto la piattaforma
                if (block.transform.position == PlatformPosition.position)
                {
                    // Blocca il movimento quando raggiunge la piattaforma
                    isTriggered = false;
                }

        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Verifica se il player entra nel collider
        if (other.CompareTag("Player"))
        {
            // Imposta il flag per iniziare il conto alla rovescia per la caduta
            isTriggered = true;
        }
    }
}
