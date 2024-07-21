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
    private AudioSource audioSource; // Componente AudioSource
    public bool isLaunching = false; // serve per far partire l animazione di lancio
    public bool PerfectPosition = false; // Indica se l'oggetto risucchiato ha raggiunto correttamente lo sphere empty

    private Rigidbody rb; // Componente Rigidbody

    void Start()
    {
        initialPosition = transform.position; // Memorizza la posizione iniziale dell'oggetto
        audioSource = GetComponent<AudioSource>(); // Ottiene il componente AudioSource
        rb = GetComponent<Rigidbody>(); // Ottiene il componente Rigidbody
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic; // Imposta il modo di rilevamento delle collisioni
        rb.isKinematic = true;
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
        if (isInRange && (Input.GetKeyDown(KeyCode.O) || Input.GetButtonDown("Fire1")) && !isHoldingObject && (CompareTag("Glass")||CompareTag("Wood")||CompareTag("Metal")||CompareTag("Dynamite")))
        {
            // Se non sta già tenendo l'oggetto, avvicinalo al player
            isHoldingObject = true;
             rb.isKinematic = true;

        }

        // Se stiamo tenendo l'oggetto, muovilo lentamente verso il player
        if (isHoldingObject)
        {
         
            // Usa Lerp per muovere gradualmente l'oggetto verso la posizione target
              if (PerfectPosition == false){
                 StartCoroutine(Absorbing());
              
            }
            // Se l'oggetto è abbastanza vicino alla testa del player, impostalo esattamente lì
            if (Vector3.Distance(transform.position, targetPosition) < 0.2f)
            {
                transform.position = targetPosition;
                PerfectPosition = true;
                    }

            // Mantieni l'oggetto sopra la testa del player mentre si muove
           if(PerfectPosition)
            transform.position = playerHead.position;

            // Controlla se il player ha premuto il tasto P per lanciare l'oggetto
            if (Input.GetKeyDown(KeyCode.P) || Input.GetButtonDown("Fire2") )
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
        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            transform.position += direction * throwSpeed * Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
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
}
