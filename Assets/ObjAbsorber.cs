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
    private bool isHoldingObject = false; // Indica se il player sta tenendo l'oggetto
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

    // Riferimento allo script Chat_Bubble_Spawn
    
    void Start()
    {
        initialPosition = transform.position; // Memorizza la posizione iniziale dell'oggetto
        audioSource = GetComponent<AudioSource>(); // Ottiene il componente AudioSource
        rb = GetComponent<Rigidbody>(); // Ottiene il componente Rigidbody
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic; // Imposta il modo di rilevamento delle collisioni
        rb.isKinematic = true;

        // All'inizio della scena, solo "empty" è attivo
        SetActiveGameObject(empty);
        

        // Assicurati che il riferimento allo script Chat_Bubble_Spawn sia assegnato
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
        if (isInRange && (Input.GetKeyDown(KeyCode.O) || Input.GetButtonDown("Fire3")) && !isHoldingObject && (CompareTag("Glass") || CompareTag("Wood") || CompareTag("Metal") || CompareTag("Dynamite") || CompareTag("Key")))
        {
            // Se non sta già tenendo l'oggetto e nessun altro oggetto è attualmente assorbito
            if (currentAbsorbedObject == null)
            {
                isHoldingObject = true;
                rb.isKinematic = true;
                currentAbsorbedObject = this; // Imposta questo oggetto come l'oggetto attualmente assorbito

                // Distruggi la chat bubble quando l'oggetto viene risucchiato
                if (chatBubbleScript != null)
                {
                    chatBubbleScript.DestroyChatBubble();
                }

                // Imposta la visibilità degli oggetti in base al tag dell'oggetto assorbito
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


        // Se stiamo tenendo l'oggetto, muovilo lentamente verso il player
        if (isHoldingObject)
        {
            // Usa Lerp per muovere gradualmente l'oggetto verso la posizione target
            if (!PerfectPosition)
            {
                StartCoroutine(Absorbing());
            }

            // Se l'oggetto è abbastanza vicino alla testa del player, impostalo esattamente lì
            if (Vector3.Distance(transform.position, targetPosition) < 0.2f)
            {
                transform.position = targetPosition;
                PerfectPosition = true;
            }

            // Mantieni l'oggetto sopra la testa del player mentre si muove
            if (PerfectPosition)
                transform.position = playerHead.position;

            // Controlla se il player ha premuto il tasto P per lanciare l'oggetto
            if (Input.GetKeyDown(KeyCode.P) || Input.GetButtonDown("Jump"))
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

        // Rende il Rigidbody non kinematic quando viene lanciato
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.velocity = direction * throwSpeed; // Imposta la velocità del lancio
        }

        // Resetta la variabile statica dell'oggetto assorbito
        currentAbsorbedObject = null;

        // Riporta la visibilità a "empty" quando l'oggetto viene lanciato
        SetActiveGameObject(empty);
    }

    private IEnumerator LaunchRoutine()
    {
        yield return new WaitForSeconds(0.3f);
        isLaunching = true;
        yield return new WaitForSeconds(0.3f);
        isLaunching = false;
    }

    void OnDrawGizmos()
    {
        // Disegna il range di interazione come una sfera rossa quando il player è nel range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, maxDistance);
    }

    // Funzione helper per gestire la visibilità dei GameObject
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

