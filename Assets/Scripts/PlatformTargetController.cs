using UnityEngine;

public class PlatformTargetController : MonoBehaviour
{
    public GameObject block;
    public Transform OriginalPosition;
    public Transform PlatformPosition;
    public float dropSpeed = 7.0f;
    public AudioClip fallSoundClip;
    public AudioClip startFallSoundClip;
    public float arrivalThreshold = 0.1f;

    private bool isTriggered = false;
    private bool hasPlayedFallSound = false;
    private AudioSource fallAudioSource;
    private AudioSource startFallAudioSource;
    private Rigidbody blockRigidbody;

    void Start()
    {
        block.transform.position = OriginalPosition.position;

        // Aggiungi un Rigidbody al blocco
        blockRigidbody = block.GetComponent<Rigidbody>();
        if (blockRigidbody == null)
        {
            blockRigidbody = block.AddComponent<Rigidbody>();
        }
        blockRigidbody.isKinematic = true; // Assicurati che il blocco non si muova prima che sia stato attivato

        fallAudioSource = gameObject.AddComponent<AudioSource>();
        fallAudioSource.clip = fallSoundClip;

        startFallAudioSource = gameObject.AddComponent<AudioSource>();
        startFallAudioSource.clip = startFallSoundClip;
    }

    void Update()
    {
        if (isTriggered)
        {
            if (!startFallAudioSource.isPlaying && !hasPlayedFallSound)
            {
                startFallAudioSource.Play();
            }

            // Cambia il blocco in modo che sia gestito dalla fisica
            blockRigidbody.isKinematic = false;
            blockRigidbody.velocity = (PlatformPosition.position - block.transform.position).normalized * dropSpeed;

            if (Vector3.Distance(block.transform.position, PlatformPosition.position) <= arrivalThreshold + 5)
            {
                if (!hasPlayedFallSound)
                {
                    fallAudioSource.Play();
                    hasPlayedFallSound = true;
                }
            }

            if (Vector3.Distance(block.transform.position, PlatformPosition.position) <= arrivalThreshold)
            {
                isTriggered = false;
                blockRigidbody.isKinematic = true; // Ferma il blocco
                blockRigidbody.velocity = Vector3.zero;
                block.transform.position = PlatformPosition.position;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isTriggered = true;
        }
    }
}
