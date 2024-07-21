using UnityEngine;

public class SpeedBoostPlatform : MonoBehaviour
{
    public float boostSpeed = 10.0f; // La velocità aumentata quando il player è sulla piattaforma
    public AudioClip boostSound; // Suono da riprodurre quando il boost viene attivato
    private float normalSpeed;
    private AudioSource audioSource;
    private bool hasPlayedSound = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.clip = boostSound;
        audioSource.loop = false;
        audioSource.playOnAwake = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                normalSpeed = playerMovement.maximumSpeed; // Salva la velocità normale
                playerMovement.maximumSpeed = boostSpeed; // Imposta la velocità aumentata

                // Riproduci il suono solo una volta quando il giocatore entra nella piattaforma
                if (!hasPlayedSound && boostSound != null)
                {
                    // Imposta il volume a metà (0.5f corrisponde al 50%)
                    audioSource.volume = 0.2f;
                    audioSource.PlayOneShot(boostSound);
                    hasPlayedSound = true;
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
                hasPlayedSound = false; // Resetta il flag per permettere di riprodurre il suono nuovamente
            }
        }
    }
}
