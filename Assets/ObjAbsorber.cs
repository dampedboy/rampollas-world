using System.Collections;
using UnityEngine;

public class ObjAbsorber : MonoBehaviour
{
    public float moveSpeed = 4f; // Velocità di avvicinamento dell'oggetto
    public float throwSpeed = 10f; // Velocità di lancio dell'oggetto
    public float maxDistance = 5f; // Distanza massima a cui può essere tenuto l'oggetto
    public Transform playerHead; // Posizione della testa del player
    public Transform player; // Riferimento al player
    public GameObject risucchio; // Riferimento all'oggetto Risucchio
    public AudioClip assorbimento; // Audio clip per il suono di assorbimento

    private Vector3 initialPosition; // Posizione iniziale dell'oggetto
    private Vector3 targetPosition; // Posizione target verso cui muovere l'oggetto
    public bool isHoldingObject = false; // Indica se il player sta tenendo l'oggetto
    private bool isInRange = false; // Indica se il player è nel range dell'oggetto
    public bool isThrown = false; // Indica se l'oggetto è stato lanciato
    [SerializeField] private Chat_Bubble_Spawn chatBubbleScript;
    private AudioSource audioSource; // Componente AudioSource
    public bool isLaunching = false; // Serve per far partire l'animazione di lancio
    public bool PerfectPosition = false; // Indica se l'oggetto risucchiato ha raggiunto correttamente lo sphere empty

    private Rigidbody rb; // Componente Rigidbody

    // Variabile statica per tenere traccia dell'oggetto attualmente assorbito
    private static ObjAbsorber currentAbsorbedObject = null;

    public GameObject empty;
    public GameObject key;
    public GameObject dynamite;
    public GameObject metal;
    public GameObject glass;
    public GameObject wood;

    public PlayerMovement Rampolla;

    void Start()
    {
        initialPosition = transform.position; // Memorizza la posizione iniziale dell'oggetto
        audioSource = GetComponent<AudioSource>(); // Ottiene il componente AudioSource
        rb = GetComponent<Rigidbody>(); // Ottiene il componente Rigidbody
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic; // Imposta il modo di rilevamento delle collisioni
        rb.isKinematic = true;

        // All'inizio della scena, solo "empty" è attivo
        SetActiveGameObject(empty);

        if (chatBubbleScript == null)
        {
            Debug.LogError("Chat_Bubble_Spawn script reference not set in ObjAbsorber.");
        }
    }

    private IEnumerator Absorbing()
    {
        yield return new WaitForSeconds(0.3f);
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);
    }

    void Update()
    {
        isInRange = Vector3.Distance(transform.position, player.position) <= maxDistance;
        targetPosition = playerHead.position; // Imposta la posizione target come la testa del player

        // Controlla se il player è nel range dell'oggetto e ha premuto il tasto O
        if (isInRange && (Input.GetKeyDown(KeyCode.O) || Input.GetButtonDown("Fire3")) && !isHoldingObject && 
            (CompareTag("Glass") || CompareTag("Wood") || CompareTag("Metal") || CompareTag("Dynamite") || CompareTag("Key")) && 
            !Rampolla.launchable && Rampolla.isGrounded)
        {
            if (currentAbsorbedObject == null)
            {
                isHoldingObject = true;
                rb.isKinematic = true;
                currentAbsorbedObject = this;

                if (chatBubbleScript != null)
                {
                    chatBubbleScript.DestroyChatBubble();
                }

                if (CompareTag("Metal"))
                    SetActiveGameObject(metal);
                else if (CompareTag("Wood"))
                    SetActiveGameObject(wood);
                else if (CompareTag("Glass"))
                    SetActiveGameObject(glass);
                else if (CompareTag("Dynamite"))
                    SetActiveGameObject(dynamite);
                else if (CompareTag("Key"))
                    SetActiveGameObject(key);
            }
        }

        if (isHoldingObject)
        {
            if (!PerfectPosition)
            {
                StartCoroutine(Absorbing());
            }

            if (Vector3.Distance(transform.position, targetPosition) < 0.2f)
            {
                transform.position = targetPosition;
                PerfectPosition = true;
            }

            if (PerfectPosition)
                transform.position = playerHead.position;

            // Quando il giocatore rilascia il pulsante
            if (Input.GetKeyUp(KeyCode.O) || Input.GetButtonUp("Fire3"))
            {
                Rampolla.launchable = true;
                Rampolla.isAbsorbing = false;
            }

            // Se il giocatore preme di nuovo il pulsante e l'oggetto è pronto per essere lanciato
            if ((Input.GetKeyDown(KeyCode.O) || Input.GetButtonDown("Fire3")) && Rampolla.launchable)
            {
                StartCoroutine(LaunchRoutine());
                Vector3 throwDirection = player.forward.normalized;
                StartCoroutine(ThrowObject(throwDirection));
            }
        }
    }

    private IEnumerator ThrowObject(Vector3 direction)
    {
        yield return new WaitForSeconds(0.4f);
        isHoldingObject = false; // L'oggetto viene lanciato, non lo stiamo più tenendo
        PerfectPosition = false;
        isThrown = true;

        if (rb != null)
        {
            rb.isKinematic = false;
            rb.velocity = direction * throwSpeed; // Imposta la velocità del lancio
        }

        currentAbsorbedObject = null;

        SetActiveGameObject(empty);
    }

    private IEnumerator LaunchRoutine()
    {
        yield return new WaitForSeconds(0.3f);
        isLaunching = true;
        yield return new WaitForSeconds(0.3f);
        isLaunching = false;
        Rampolla.launchable = false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, maxDistance);
    }

    private void SetActiveGameObject(GameObject activeGameObject)
    {
        empty.SetActive(activeGameObject == empty);
        metal.SetActive(activeGameObject == metal);
        wood.SetActive(activeGameObject == wood);
        glass.SetActive(activeGameObject == glass);
        dynamite.SetActive(activeGameObject == dynamite);
        key.SetActive(activeGameObject == key);
    }
}
