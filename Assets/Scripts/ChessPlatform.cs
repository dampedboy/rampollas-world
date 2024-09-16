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

    // Intervallo di tempo prima che una piattaforma scompaia
    public float disappearInterval = 0.5f;

    // Lista per tracciare le piattaforme duplicate
    private List<GameObject> createdPlatforms = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Duplica la piattaforma nelle posizioni e rotazioni specificate
            DuplicatePlatforms();
            // Avvia la coroutine per farle scomparire
            StartCoroutine(DisappearPlatforms());
        }
    }

    // Duplica le piattaforme alle posizioni e rotazioni specificate
    private void DuplicatePlatforms()
    {
        foreach (PlatformTransform platformTransform in platformTransforms)
        {
            GameObject newPlatform = Instantiate(platformPrefab, platformTransform.position, Quaternion.Euler(platformTransform.rotation));
            createdPlatforms.Add(newPlatform);
        }
    }

    // Coroutine che fa scomparire le piattaforme a intervalli
    private IEnumerator DisappearPlatforms()
    {
        // Aggiungi la piattaforma originale alla lista per farla scomparire per prima
        createdPlatforms.Insert(0, gameObject);

        foreach (GameObject platform in createdPlatforms)
        {
            yield return new WaitForSeconds(disappearInterval);
            platform.SetActive(false);  // Disattiva la piattaforma (equivalente a farla scomparire)
        }
    }
}
