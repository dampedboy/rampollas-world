using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public float fallDelay = 1.0f; // Ritardo prima che la piattaforma inizi a cadere

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; // Assicurati che la piattaforma non si muova fino a quando non viene toccata
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Invoke("DropPlatform", fallDelay); // Inizia il conto alla rovescia per la caduta
        }
    }

    private void DropPlatform()
    {
        rb.isKinematic = false; // Permette alla piattaforma di cadere
    }
}
