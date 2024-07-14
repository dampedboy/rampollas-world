using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public float fallSpeed = 100f; // Velocità con cui cade la piattaforma

    private Rigidbody rb;
    private bool isFalling = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; // Assicurati che inizi come oggetto statico
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isFalling)
        {
            Fall(); // Avvia immediatamente la caduta della piattaforma
        }
    }

    void Fall()
    {
        isFalling = true;
        rb.isKinematic = false; // Ora la gravità influenzerà la piattaforma
        rb.velocity = new Vector3(0, -fallSpeed, 0); // Fai cadere la piattaforma verso il basso
    }
}
