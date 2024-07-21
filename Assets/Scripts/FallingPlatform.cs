using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public float fallSpeed = 100f; // Velocità con cui cade la piattaforma
    public AudioClip fallSound; // AudioClip per il suono della caduta

    private Rigidbody rb;
    private bool isFalling = false;
    private AudioSource audioSource; // AudioSource per riprodurre il suono

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; // Assicurati che inizi come oggetto statico

        // Aggiungi un AudioSource a questo GameObject e assegnalo
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = fallSound;
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

        // Riproduci il suono della caduta
        if (audioSource != null && fallSound != null)
        {
            audioSource.Play();
        }
    }
}
