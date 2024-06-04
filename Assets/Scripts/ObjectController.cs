using UnityEngine;

public class ObjectController : MonoBehaviour
{
    public Transform player; // Riferimento al Transform del player
    public Transform head; // Riferimento al punto sopra la testa del player
    public float attractDistance = 2f; // Distanza alla quale l'oggetto viene attirato
    public float moveSpeed = 2f; // Velocità di movimento dell'oggetto
    public float throwForce = 10f; // Forza con cui l'oggetto viene lanciato
    private Transform attachedObject; // Riferimento all'oggetto attaccato
    private bool moveToPlayer = false; // Flag per muovere l'oggetto verso il player
    private bool moveToHead = false; // Flag per muovere l'oggetto verso la testa
    private Vector3 playerMoveDirection; // Direzione di movimento del player

    void Update()
    {
        // Aggiorna la direzione di movimento del player
        playerMoveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

        // Controlla se il tasto "C" è premuto e l'oggetto è vicino
        if (Input.GetKeyDown(KeyCode.C))
        {
            AttachNearbyObject();
        }

        // Controlla se il tasto "P" è premuto e l'oggetto è attaccato
        if (Input.GetKeyDown(KeyCode.P) && attachedObject != null)
        {
            ThrowObject();
        }

        // Muovi l'oggetto verso il player
        if (moveToPlayer && attachedObject != null)
        {
            attachedObject.position = Vector3.Lerp(attachedObject.position, player.position, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(attachedObject.position, player.position) < 0.1f)
            {
                moveToPlayer = false;
                moveToHead = true; // Avvia il movimento verso la testa
                attachedObject.SetParent(player);
            }
        }

        // Muovi l'oggetto sulla testa del player
        if (moveToHead && attachedObject != null)
        {
            attachedObject.position = Vector3.Lerp(attachedObject.position, head.position, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(attachedObject.position, head.position) < 0.1f)
            {
                moveToHead = false;
                attachedObject.SetParent(head);
            }
        }
    }

    void AttachNearbyObject()
    {
        Collider[] hitColliders = Physics.OverlapSphere(player.position, attractDistance);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("Attachable"))
            {
                attachedObject = hitCollider.transform;
                attachedObject.GetComponent<Rigidbody>().isKinematic = true;
                moveToPlayer = true;
                break;
            }
        }
    }

    void ThrowObject()
    {
        if (attachedObject != null)
        {
            attachedObject.SetParent(null);
            Rigidbody rb = attachedObject.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            Vector3 throwDirection = playerMoveDirection != Vector3.zero ? playerMoveDirection : player.forward;
            rb.AddForce(throwDirection * throwForce, ForceMode.Impulse);
            attachedObject = null;
            moveToPlayer = false;
            moveToHead = false;
        }
    }
}