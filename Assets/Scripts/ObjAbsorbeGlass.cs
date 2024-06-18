using System.Collections;
using UnityEngine;

public class ObjAbsorbeGlass : MonoBehaviour
{
    public float moveSpeed = 2f; // Velocità di avvicinamento dell'oggetto
    public float throwSpeed = 10f; // Velocità di lancio dell'oggetto
    public float maxDistance = 5f; // Distanza massima a cui può essere tenuto l'oggetto
    public Transform playerHead; // Posizione della testa del player
    public Transform player; // Riferimento al player
    public GameObject risucchio; // Riferimento all'oggetto Risucchio


    private Vector3 initialPosition; // Posizione iniziale dell'oggetto
    private Vector3 targetPosition; // Posizione target verso cui muovere l'oggetto
    private bool isHoldingObject = false; // Indica se il player sta tenendo l'oggetto
    private bool isInRange = false; // Indica se il player è nel range dell'oggetto

    void Start()
    {
        initialPosition = transform.position; // Memorizza la posizione iniziale dell'oggetto
        risucchio.SetActive(false); // Inizialmente nasconde l'oggetto Risucchio
    }

    void Update()
    {
        isInRange = Vector3.Distance(transform.position, player.position) <= maxDistance;

        // Controlla se il player è nel range dell'oggetto e ha premuto il tasto C
        if (isInRange && Input.GetKeyDown(KeyCode.C) && !isHoldingObject && CompareTag("Glass"))
        {
            // Se non sta già tenendo l'oggetto, avvicinalo al player
            isHoldingObject = true;
            targetPosition = playerHead.position; // Imposta la posizione target come la testa del player
            risucchio.SetActive(true); // Mostra l'oggetto Risucchio
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
            }
        }
    }

    private IEnumerator ThrowObject(Vector3 direction)
    {
        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            transform.position += direction * throwSpeed * Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }



    void OnDrawGizmos()
    {
        // Disegna il range di interazione come una sfera rossa quando il player è nel range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, maxDistance);
    }
}
