using UnityEngine;

public class Dynamite : MonoBehaviour
{
    public float explosionForce = 500f;
    public float explosionRadius = 5f;
    public AudioClip esplosione; // AudioClip per l'esplosione
    private AudioSource audioSource;
    private MeshRenderer meshRenderer;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = esplosione;
        meshRenderer = GetComponent<MeshRenderer>(); // Ottieni il MeshRenderer del gameObject
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            audioSource.Play();
            Destroy(other.gameObject); // Distruggi il nemico

            // Disattiva il MeshRenderer della dinamite
            if (meshRenderer != null)
            {
                meshRenderer.enabled = false;
            }

            float delay = Mathf.Max(audioSource.clip.length, 0f);
            Destroy(gameObject, delay); // Distruggi la dinamite dopo il tempo di ritardo calcolato
        }
    }
}


