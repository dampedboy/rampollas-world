using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaBlock : MonoBehaviour
{
    public GameObject steelBlockPrefab;
    public AudioClip breakSound; // AudioClip per il suono di rottura

    private bool canCollide = true; // Variabile per controllare se il blocco può collidere
    private AudioSource audioSource; // AudioSource per riprodurre il suono

    void Start()
    {
        // Ottieni o aggiungi un componente AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.clip = breakSound;
        audioSource.playOnAwake = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject otherObject = collision.gameObject;

        // Controlla se il blocco può collidere e se l'oggetto che ha causato la collisione è di metallo
        if (canCollide && otherObject.CompareTag("Plasma"))
        {
            // Ottieni lo script ObjAbsorbeMetal dall'oggetto che ha causato la collisione
            ObjAbsorber metalScript = otherObject.GetComponent<ObjAbsorber>();

            // Controlla se l'oggetto di metallo è stato avvicinato al player
            if (metalScript != null && metalScript.isThrown)
            {
                ReplaceWithWood();
            }
        }
        
    }

    private void ReplaceWithWood()
    {
        // Disabilita ulteriori collisioni per evitare che altre istanze di MetalBlock siano sostituite immediatamente
        canCollide = false;

        // Riproduci il suono di rottura
        audioSource.volume *= 1.5f; // Aumenta il volume di 1.5 volte
        audioSource.Play();

        // Ottieni la posizione e la rotazione del blocco di metallo
        Vector3 position = transform.position;
        Quaternion rotation = transform.rotation;

        // Sostituisci con un nuovo blocco di legno dopo un ritardo
        StartCoroutine(ReplaceAfterDelay(position, rotation));
    }

    private IEnumerator ReplaceAfterDelay(Vector3 position, Quaternion rotation)
    {
        // Attendi un certo periodo prima di sostituire con il blocco di legno
        yield return new WaitForSeconds(0.8f); // Modifica il valore 1f a seconda del ritardo desiderato

        // Sostituisci con un nuovo blocco di legno
        Instantiate(steelBlockPrefab, position, rotation);

        // Distruggi questo blocco di metallo
        Destroy(gameObject);
    }
}
