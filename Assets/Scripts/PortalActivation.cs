using UnityEngine;

public class PortalActivation : MonoBehaviour
{
    public GameObject circle; // Riferimento all'oggetto Circle
    public GameObject areaLight; // Riferimento all'oggetto Area Light
    public GameObject particleSystem; // Riferimento all'oggetto Particle System
    public GameObject door; // Riferimento all'oggetto Door
    public Transform keyObject; // Riferimento all'oggetto chiave
    public BoxCollider boxCollider; // Riferimento al BoxCollider del portale

    private bool isActivated = false; // Flag per controllare se il portale è attivato
    private float activationDistance = 3f; // Distanza di attivazione del portale

    void Start()
    {
        // Inizialmente rendi visibili solo Particle System e Door
        SetPortalState(false);
        // Assicurati che il Particle System sia attivo
        if (particleSystem != null)
        {
            particleSystem.SetActive(true);
        }
        door.SetActive(true);

        // Disattiva il BoxCollider all'inizio
        if (boxCollider != null)
        {
            boxCollider.enabled = false;
        }
    }

    void Update()
    {
        // Controlla la distanza tra il portale e l'oggetto chiave
        float keyDistance = Vector3.Distance(transform.position, keyObject.position);

        if (keyDistance <= activationDistance && !isActivated)
        {
            SetPortalState(true);
            // Distruggi l'oggetto chiave
            Destroy(keyObject.gameObject);
        }
    }

    void SetPortalState(bool state)
    {
        // Imposta la visibilità di Circle e Area Light
        circle.SetActive(state);
        areaLight.SetActive(state);
        isActivated = state;

        // Attiva o disattiva il BoxCollider
        if (boxCollider != null)
        {
            boxCollider.enabled = state;
        }
    }
}
