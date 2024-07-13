using UnityEngine;

public class PortalActivation : MonoBehaviour
{
    public GameObject circle; // Riferimento all'oggetto Circle
    public GameObject areaLight; // Riferimento all'oggetto Area Light
    public GameObject particleSystem; // Riferimento all'oggetto Particle System
    public GameObject door; // Riferimento all'oggetto Door
    public Transform keyObject; // Riferimento all'oggetto chiave
    public Portal portalScript; // Riferimento allo script Portal

    private bool isActivated = false; // Flag per controllare se il portale è attivato
    private float activationDistance = 3f; // Distanza di attivazione del portale

    void Start()
    {
        SetPortalState(false);
        if (particleSystem != null)
        {
            particleSystem.SetActive(true);
        }
        door.SetActive(true);
    }

    void Update()
    {
        float keyDistance = Vector3.Distance(transform.position, keyObject.position);

        if (keyDistance <= activationDistance && !isActivated)
        {
            SetPortalState(true);
            Destroy(keyObject.gameObject);
            // Attiva il collider del portale
            portalScript.ActivateCollider();
        }
    }

    void SetPortalState(bool state)
    {
        circle.SetActive(state);
        areaLight.SetActive(state);
        isActivated = state;
    }
}