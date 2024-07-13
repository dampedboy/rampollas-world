using UnityEngine;
using System.Collections;

public class Fragile : MonoBehaviour
{
    public GameObject newPlatformPrefab; // Il prefab della nuova piattaforma
    private bool isTransformed = false; // Per evitare trasformazioni multiple
    public float delay = 1.0f; // Delay prima della trasformazione

    void OnTriggerEnter(Collider other)
    {
        if (!isTransformed && other.gameObject.GetComponent<CharacterController>() != null)
        {
            StartCoroutine(TransformPlatformAfterDelay());
        }
    }

    IEnumerator TransformPlatformAfterDelay()
    {
        isTransformed = true;
        yield return new WaitForSeconds(delay); // Aspetta per il delay specificato
        TransformPlatform();
    }

    void TransformPlatform()
    {
        // Crea la nuova piattaforma nella stessa posizione e rotazione della piattaforma attuale
        Instantiate(newPlatformPrefab, transform.position, transform.rotation);
        // Distruggi la piattaforma attuale
        Destroy(gameObject);
    }
}