using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomPlatformDisabler : MonoBehaviour
{
    public GameObject[] platforms; // Array di tutte le piattaforme
    public float switchInterval = 3f; // Intervallo di tempo tra le disattivazioni
    public int minPlatformsToDisable = 1; // Numero minimo di piattaforme da disattivare ogni intervallo
    public int maxPlatformsToDisable = 5; // Numero massimo di piattaforme da disattivare ogni intervallo

    private void Start()
    {
        // Inizia la coroutine per disattivare le piattaforme in modo casuale
        StartCoroutine(DisableRandomPlatforms());
    }

    IEnumerator DisableRandomPlatforms()
    {
        while (true)
        {
            yield return new WaitForSeconds(switchInterval);

            // Numero casuale di piattaforme da disattivare
            int numPlatformsToDisable = Random.Range(minPlatformsToDisable, maxPlatformsToDisable + 1);

            // Lista delle piattaforme disponibili per essere disattivate
            List<GameObject> availablePlatforms = new List<GameObject>(platforms);

            for (int i = 0; i < numPlatformsToDisable; i++)
            {
                if (availablePlatforms.Count == 0)
                    break;

                // Scegli una piattaforma casuale dall'elenco delle piattaforme disponibili
                int randomIndex = Random.Range(0, availablePlatforms.Count);
                GameObject platformToDisable = availablePlatforms[randomIndex];

                // Disattiva la piattaforma
                platformToDisable.SetActive(false);

                // Rimuovi la piattaforma dall'elenco delle piattaforme disponibili
                availablePlatforms.RemoveAt(randomIndex);
            }

            // Riattiva tutte le piattaforme dopo il ciclo di disattivazione
            yield return new WaitForSeconds(switchInterval);
            foreach (GameObject platform in platforms)
            {
                platform.SetActive(true);
            }
        }
    }
}
