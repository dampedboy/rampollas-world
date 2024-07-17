using UnityEngine;

public class Dynamite : MonoBehaviour
{
    public float explosionForce = 500f;
    public float explosionRadius = 5f;

    public AudioClip esplosione;
    public AudioSource audioSource;

    void Start()
    {
        // Initialize audio source
        audioSource = GetComponent<AudioSource>();

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Explode(other.gameObject);


        }
        else if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject); // Distruggi il nemico
            Destroy(gameObject); // Distruggi la dinamite
                                 // Play  sound
            if (esplosione != null && audioSource != null)
            {
                audioSource.PlayOneShot(esplosione);
            }
        }
    }

    void Explode(GameObject player)
    {
        CharacterController controller = player.GetComponent<CharacterController>();

        if (controller != null)
        {
            Vector3 explosionDirection = (player.transform.position - transform.position).normalized;

            // Crea un movimento di respinta per il player
            Vector3 pushDirection = explosionDirection * explosionForce;
            StartCoroutine(ApplyExplosionForce(controller, pushDirection));
        }

        // Play  sound
        if (esplosione != null && audioSource != null)
        {
            audioSource.PlayOneShot(esplosione);
        }
        Destroy(gameObject); // Distruggi la dinamite dopo l'esplosione
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
