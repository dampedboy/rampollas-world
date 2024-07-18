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

        if (other.CompareTag("Player"))
        {
            Explode(other.gameObject);
        }
        else if (other.CompareTag("Enemy"))
        {
            audioSource.Play();
            Destroy(other.gameObject); // Distruggi il nemico
            float delay = Mathf.Max(audioSource.clip.length - 1.55f, 0f); // Calcola il tempo di ritardo, assicurandoti che non sia negativo
            audioSource.Play();

            Destroy(gameObject, delay); // Distruggi la dinamite dopo il tempo di ritardo calcolato

        }
    }

    void Explode(GameObject player)
    {
        // Riproduci il suono dell'esplosione
        audioSource.Play();

        CharacterController controller = player.GetComponent<CharacterController>();

        if (controller != null)
        {
            Vector3 explosionDirection = (player.transform.position - transform.position).normalized;

            // Crea un movimento di respinta per il player
            Vector3 pushDirection = explosionDirection * explosionForce;
            StartCoroutine(ApplyExplosionForce(controller, pushDirection));
        }

        float delay = Mathf.Max(audioSource.clip.length - 1.4f, 0f); // Calcola il tempo di ritardo, assicurandoti che non sia negativo
        Destroy(gameObject, delay); // Distruggi la dinamite dopo il tempo di ritardo calcolato
    }

    System.Collections.IEnumerator ApplyExplosionForce(CharacterController controller, Vector3 force)
    {
        float time = 0.2f; // Durata della respinta
        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            controller.Move(force * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
