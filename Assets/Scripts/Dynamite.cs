using UnityEngine;

public class Dynamite : MonoBehaviour
{
    public float explosionForce = 500f;
    public float explosionRadius = 5f;
    public AudioClip esplosione; // AudioClip per l'esplosione
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = esplosione;
    }

    void OnTriggerEnter(Collider other)
    {

        
        if (other.CompareTag("Enemy"))
        {
            audioSource.Play();
            Destroy(other.gameObject); // Distruggi il nemico
            float delay = Mathf.Max(audioSource.clip.length - 1.55f, 0f); // Calcola il tempo di ritardo, assicurandoti che non sia negativo
            audioSource.Play();

            Destroy(gameObject, delay); // Distruggi la dinamite dopo il tempo di ritardo calcolato

        }
    }
}


