using UnityEngine;

public class PlatformTargetController : MonoBehaviour
{
    // Oggetto da assegnare nel Inspector per il blocco
    public GameObject block;

    // Trasform del blocco (posizione di partenza)
    public Transform OriginalPosition;

    // Trasform della piattaforma (posizione di arrivo)
    public Transform PlatformPosition;

    // Variabile per determinare se il blocco è stato rilasciato
    private bool isTriggered = false;

    // Velocità di caduta del blocco
    public float dropSpeed = 7.0f;

    // Clip audio per il suono di caduta
    public AudioClip fallSoundClip;

    // Clip audio per il suono di inizio caduta
    public AudioClip startFallSoundClip;

    private AudioSource fallAudioSource; // AudioSource per riprodurre il suono di caduta
    private AudioSource startFallAudioSource; // AudioSource per riprodurre il suono di inizio caduta

    // Distanza minima considerata "arrivato" alla piattaforma
    public float arrivalThreshold = 0.1f;

    // Flag per controllare se il suono di caduta è stato già riprodotto
    private bool hasPlayedFallSound = false;

    void Start()
    {
        // Imposta la posizione iniziale del blocco
        block.transform.position = OriginalPosition.position;

        // Inizializza l'AudioSource e assegna l'AudioClip per il suono di caduta
        fallAudioSource = gameObject.AddComponent<AudioSource>();
        fallAudioSource.clip = fallSoundClip;

        // Inizializza un secondo AudioSource e assegna l'AudioClip per il suono di inizio caduta
        startFallAudioSource = gameObject.AddComponent<AudioSource>();
        startFallAudioSource.clip = startFallSoundClip;
    }

    void Update()
    {
        if (isTriggered)
        {
            // Se il blocco non è ancora partito, riproduci il suono di inizio caduta
            if (!startFallAudioSource.isPlaying && !hasPlayedFallSound)
            {
                startFallAudioSource.Play();
            }

            // Movimento del blocco verso la posizione della piattaforma
            block.transform.position = Vector3.MoveTowards(block.transform.position, PlatformPosition.position, dropSpeed * Time.deltaTime);

            // Verifica se il blocco è vicino abbastanza alla piattaforma per considerarsi "arrivato"
            if (Vector3.Distance(block.transform.position, PlatformPosition.position) <= arrivalThreshold)
            {
                // Blocca il movimento quando raggiunge la piattaforma
                isTriggered = false;

                // Riproduci il suono di caduta solo se non è stato già riprodotto
                if (!hasPlayedFallSound)
                {
                    fallAudioSource.Play();
                    hasPlayedFallSound = true;
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Verifica se il player entra nel collider
        if (other.CompareTag("Player"))
        {
            // Imposta il flag per iniziare il conto alla rovescia per la caduta
            isTriggered = true;
        }
    }
}


