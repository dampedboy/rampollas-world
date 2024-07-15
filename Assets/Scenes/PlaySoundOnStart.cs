using UnityEngine;

public class PlaySoundOnStart : MonoBehaviour
{
    // Riferimento al file audio
    public AudioClip clip;

    // Componente AudioSource per riprodurre il suono
    private AudioSource audioSource;

    void Start()
    {
        // Aggiungi il componente AudioSource al GameObject
        audioSource = gameObject.AddComponent<AudioSource>();

        // Imposta il clip audio nel AudioSource
        audioSource.clip = clip;

        // Riproduci il suono
        audioSource.Play();
    }
}

