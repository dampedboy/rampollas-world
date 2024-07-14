using UnityEngine;
using System.Collections;

public class FallingPlatform : MonoBehaviour
{
    public float fallDelay = 3.0f;  // Tempo di attesa prima che la piattaforma inizi a cadere
    public float disappearDelay = 2.0f;  // Tempo di attesa prima che la piattaforma scompaia dopo la caduta

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;  // Disabilita la gravità all'inizio
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(FallAndDisappear());
        }
    }

    IEnumerator FallAndDisappear()
    {
        yield return new WaitForSeconds(fallDelay);

        rb.useGravity = true;  // Abilita la gravità dopo il delay
        rb.isKinematic = false;  // Assicurati che il RigidBody non sia kinematic

        yield return new WaitForSeconds(disappearDelay);

        Destroy(gameObject);  // Distrugge la piattaforma dopo il tempo di attesa
    }
}