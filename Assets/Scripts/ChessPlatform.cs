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

    // Intervallo di tempo per l'apparizione e la scomparsa
    public float interval = 0.5f;

    // Lista per tracciare le piattaforme duplicate
    private List<GameObject> createdPlatforms = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Avvia la coroutine per creare e far scomparire le piattaforme
            StartCoroutine(HandlePlatforms());
        }
    }

    // Coroutine che gestisce sia l'apparizione che la scomparsa delle piattaforme
    private IEnumerator HandlePlatforms()
    {
        // Aggiungi la piattaforma originale alla lista
        createdPlatforms.Add(gameObject);

        // Inizia l'apparizione delle piattaforme una alla volta
        for (int i = 0; i < platformTransforms.Count; i++)
        {
            // Crea la nuova piattaforma
            PlatformTransform platformTransform = platformTransforms[i];
            GameObject newPlatform = Instantiate(platformPrefab, platformTransform.position, Quaternion.Euler(platformTransform.rotation));
            createdPlatforms.Add(newPlatform);

            // Attendere l'intervallo prima di crearne un'altra
            yield return new WaitForSeconds(interval);
        }

        // Dopo aver creato tutte le piattaforme, avvia la scomparsa parallela
        StartCoroutine(DisappearPlatforms());
    }

    // Coroutine per far scomparire le piattaforme una alla volta
    private IEnumerator DisappearPlatforms()
    {
        foreach (GameObject platform in createdPlatforms)
        {
            // Attende l'intervallo per la scomparsa di ogni piattaforma
            yield return new WaitForSeconds(interval);
            platform.SetActive(false);  // Disattiva la piattaforma (equivalente a farla scomparire)
        }
    }
}
