using UnityEngine;

public class SlowPlatform : MonoBehaviour
{
    public float slowSpeed = 1.0f; // La velocità ridotta quando il player è sulla piattaforma
    private float normalSpeed;
    private AudioSource platformAudio; // Riferimento all'AudioSource
    public AudioClip slowPlatformSound; // AudioClip per il suono della piattaforma rallentante

    void Start()
    {
        // Ottieni il riferimento all'AudioSource
        platformAudio = GetComponent<AudioSource>();
        if (platformAudio == null)
        {
            // Se non c'è un AudioSource, aggiungilo e configuralo
            platformAudio = gameObject.AddComponent<AudioSource>();
        }

        // Assegna l'AudioClip al campo dell'AudioSource
        platformAudio.clip = slowPlatformSound;
        // Imposta il volume del suono (opzionale)
        platformAudio.volume = 0.5f; // Esempio di volume a metà
        // Imposta il loop del suono a true per farlo ripetere
        platformAudio.loop = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                normalSpeed = playerMovement.maximumSpeed; // Salva la velocità normale
                playerMovement.maximumSpeed = slowSpeed; // Imposta la velocità ridotta

                // Avvia la riproduzione del suono della piattaforma rallentante se non è già in riproduzione
                if (!platformAudio.isPlaying)
                {
                    platformAudio.Play();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.maximumSpeed = normalSpeed; // Ripristina la velocità normale

                // Interrompi la riproduzione del suono della piattaforma rallentante
                if (platformAudio.isPlaying)
                {
                    platformAudio.Stop();
                }
            }
        }
    }
}
