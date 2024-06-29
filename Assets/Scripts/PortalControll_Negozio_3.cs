using UnityEngine.SceneManagement;
using UnityEngine;

public class PortalControll_Negozio_3 : MonoBehaviour
{
    public GameObject circle; // Riferimento all'oggetto Circle
    public GameObject areaLight; // Riferimento all'oggetto Area Light
    public GameObject particleSystem; // Riferimento all'oggetto Particle System
    public GameObject door; // Riferimento all'oggetto Door
    public Transform keyObject; // Riferimento all'oggetto chiave
    public Transform player; // Riferimento al player

    private bool isActivated = false; // Flag per controllare se il portale è attivato
    private float activationDistance = 3f; // Distanza di attivazione del portale

    void Start()
    {
        // Inizialmente rendi visibili solo Particle System e Door
        SetPortalState(false);
    }

    void Update()
    {
        // Controlla la distanza tra il portale e l'oggetto chiave
        float keyDistance = Vector3.Distance(transform.position, keyObject.position);

        if (keyDistance <= activationDistance)
        {
            SetPortalState(true);
        }
        else
        {
            SetPortalState(false);
        }

        // Controlla la distanza tra il portale e il player
        float playerDistance = Vector3.Distance(transform.position, player.position);

        if (playerDistance <= activationDistance && isActivated)
        {
            SceneManager.LoadScene("Negozio_3");
        }
    }

    void SetPortalState(bool state)
    {
        // Imposta la visibilità di Circle e Area Light
        circle.SetActive(state);
        areaLight.SetActive(state);
        isActivated = state;
    }
}
