using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPlatform : MonoBehaviour
{
    // Lista delle posizioni e rotazioni per le piattaforme duplicate
    [System.Serializable]
    public struct PlatformTransform
    {
        public Vector3 position;
        public Vector3 rotation;
    }

    public List<PlatformTransform> platformTransforms = new List<PlatformTransform>();

    // Prefab della piattaforma da duplicare (può essere lo stesso oggetto in scena)
    public GameObject platformPrefab;

    // Intervallo di tempo per l'apparizione
    public float interval = 0.5f;

    // Tempo dopo il quale le piattaforme scompaiono
    public float disappearDelay = 0f;

    // Flag per determinare se questa è la piattaforma originale
    public bool isOriginal = true;

    // Lista delle piattaforme create
    private List<GameObject> spawnedPlatforms = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        // Controlliamo se è la piattaforma originale e se il player ha toccato la piattaforma
        if (isOriginal && other.CompareTag("Player"))
        {
            // Avvia la coroutine per creare le piattaforme
            StartCoroutine(SpawnPlatforms());
        }
    }

    // Coroutine che gestisce l'apparizione delle piattaforme una alla volta
    private IEnumerator SpawnPlatforms()
    {
        // Inizia l'apparizione delle piattaforme una alla volta
        for (int i = 0; i < platformTransforms.Count; i++)
        {
            // Crea la nuova piattaforma
            PlatformTransform platformTransform = platformTransforms[i];
            GameObject newPlatform = Instantiate(platformPrefab, platformTransform.position, Quaternion.Euler(platformTransform.rotation));

            // Assicura che la nuova piattaforma non attivi la duplicazione
            ChessPlatform platformScript = newPlatform.GetComponent<ChessPlatform>();
            if (platformScript != null)
            {
                platformScript.isOriginal = false;  // La nuova piattaforma non è l'originale
            }

            // Aggiungi la piattaforma creata alla lista
            spawnedPlatforms.Add(newPlatform);

            // Attendere l'intervallo prima di crearne un'altra
            yield return new WaitForSeconds(interval);
        }

        // Dopo aver creato tutte le piattaforme, iniziamo la Coroutine per farle scomparire
        StartCoroutine(DestroyPlatforms());
    }

    // Coroutine per distruggere le piattaforme duplicate con un ritardo
    private IEnumerator DestroyPlatforms()
    {
        // Distruggi ogni piattaforma duplicata con un ritardo
        foreach (GameObject platform in spawnedPlatforms)
        {
            // Aspetta il tempo di delay prima di distruggere ogni piattaforma
            yield return new WaitForSeconds(disappearDelay);

            // Distruggi la piattaforma
            Destroy(platform);
        }

        // Una volta che tutte le piattaforme duplicate sono state distrutte, puoi scegliere di far fare qualcosa alla piattaforma originale se necessario
    }
}
