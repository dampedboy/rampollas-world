using System.Collections;
using UnityEngine;

public class KeyAbsorber : MonoBehaviour
{
    public float moveSpeed = 2f; // Velocità di avvicinamento dell'oggetto
    public float throwSpeed = 10f; // Velocità di lancio dell'oggetto
    public float maxDistance = 5f; // Distanza massima a cui può essere tenuto l'oggetto
    public Transform playerHead; // Posizione della testa del player
    public Transform player; // Riferimento al player
    public GameObject risucchio; // Riferimento all'oggetto Risucchio
    public GameObject portal; // Riferimento all'oggetto Portal
    public AudioClip assorbimento; // Audio clip per il suono di assorbimento

    private Vector3 initialPosition; // Posizione iniziale dell'oggetto
    private Vector3 targetPosition; // Posizione target verso cui muovere l'oggetto
    private bool isHoldingObject = false; // Indica se il player sta tenendo l'oggetto
    private bool isInRange = false; // Indica se il player è nel range dell'oggetto
    private AudioSource audioSource; // Componente AudioSource
    private Rigidbody rb; // Componente Rigidbody

    void Start()
    {
        initialPosition = transform.position; // Memorizza la posizione iniziale dell'oggetto
        risucchio.SetActive(false); // Inizialmente nasconde l'oggetto Risucchio
        audioSource = GetComponent<AudioSource>(); // Ottiene il componente AudioSource
        rb = GetComponent<Rigidbody>(); // Ottiene il componente Rigidbody
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic; // Imposta il modo di rilevamento delle collisioni
        rb.isKinematic = true;
    }

    void Update()
    {
        isInRange = Vector3.Distance(transform.position, player.position) <= maxDistance;

        // Controlla se il player è nel range dell'oggetto e ha premuto il tasto C
        if (isInRange && Input.GetKeyDown(KeyCode.C) && !isHoldingObject && CompareTag("Key"))
        {
            // Se non sta già tenendo l'oggetto, avvicinalo al player
            isHoldingObject = true;
            targetPosition = playerHead.position; // Imposta la posizione target come la testa del player
            risucchio.SetActive(true); // Mostra l'oggetto Risucchio

            // Riproduce il suono di assorbimento
            if (assorbimento != null)
            {
                audioSource.PlayOneShot(assorbimento);
            }

            // Rende il Rigidbody kinematic mentre si avvicina al player

            rb.isKinematic = true;

        }

        // Se stiamo tenendo l'oggetto, muovilo lentamente verso il player
        if (isHoldingObject)
        {
            // Mantieni l'oggetto Risucchio sopra la testa del player
            risucchio.transform.position = playerHead.position;

            // Usa Lerp per muovere gradualmente l'oggetto verso la posizione target
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);

            // Se l'oggetto è abbastanza vicino alla testa del player, impostalo esattamente lì
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                transform.position = targetPosition;
                risucchio.SetActive(false); // Nasconde l'oggetto Risucchio quando l'oggetto assorbito è sopra la testa del player
            }

            // Mantieni l'oggetto sopra la testa del player mentre si muove
            targetPosition = playerHead.position;

            // Controlla se il player ha premuto il tasto T per lanciare l'oggetto
            if (Input.GetKeyDown(KeyCode.T))
            {
                Vector3 throwDirection = player.forward.normalized;
                StartCoroutine(ThrowObject(throwDirection));
                isHoldingObject = false; // L'oggetto viene lanciato, non lo stiamo più tenendo

                // Rende il Rigidbody non kinematic quando viene lanciato
                if (rb != null)
                {
                    rb.isKinematic = false;
                    rb.velocity = throwDirection * throwSpeed; // Imposta la velocità del lancio
                }
            }
        }
    }

    private IEnumerator ThrowObject(Vector3 direction)
    {
        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Controlla se l'oggetto assorbito collide con il portale
        if (other.gameObject == portal)
        {
            Destroy(gameObject); // Distruggi l'oggetto chiave
        }
    }

    void OnDrawGizmos()
    {
        // Disegna il range di interazione come una sfera rossa quando il player è nel range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, maxDistance);
    }
}
