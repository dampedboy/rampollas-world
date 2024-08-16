using System.Collections;
using UnityEngine;

public class KeyMalocchioAbsorber : MonoBehaviour
{
    public float moveSpeed = 4f; // Velocità di avvicinamento dell'oggetto
    public float throwSpeed = 10f; // Velocità di lancio dell'oggetto
    public float maxDistance = 8f; // Distanza massima a cui può essere tenuto l'oggetto
    public Transform playerHead; // Posizione della testa del player
    public Transform player; // Riferimento al player
    public GameObject malocchio; // Riferimento a Malocchio
    public GameObject portal; // Riferimento all'oggetto Portal

    private Vector3 initialPosition; // Posizione iniziale dell'oggetto
    private Vector3 targetPosition; // Posizione target verso cui muovere l'oggetto
    public bool isHoldingObject = false; // Indica se il player sta tenendo l'oggetto
    private bool isInRange = false; // Indica se il player è nel range dell'oggetto
    public bool PerfectPosition = false; // Indica se l'oggetto risucchiato ha raggiunto correttamente lo sphere empty
    public bool isLaunching = false; // Indicatore per inizio animazione di lancio
    private Rigidbody rb; // Componente Rigidbody

    void Start()
    {
           
        rb = GetComponent<Rigidbody>(); // Ottiene il componente Rigidbody
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic; // Imposta il modo di rilevamento delle collisioni
        rb.isKinematic = true;

        portal.SetActive(false); // Rende il portale invisibile all'inizio
       
       
    }

    

    

    void OnTriggerEnter(Collider other)
    {
        // Controlla se l'oggetto assorbito collide con il Malocchio
        if (other.gameObject == malocchio)
        {
            Destroy(gameObject); // Distruggi l'oggetto chiave
            Destroy(malocchio); // Distruggi l'oggetto Malocchio
            portal.SetActive(true); // Rendi il portale visibile
        }
    }

    void OnDrawGizmos()
    {
        // Disegna il range di interazione come una sfera rossa quando il player è nel range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, maxDistance);
    }
}