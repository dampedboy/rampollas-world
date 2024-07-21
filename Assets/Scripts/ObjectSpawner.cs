using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    // Riferimento al prefab dell'oggetto da istanziare
    public GameObject objectPrefab;

    // Posizione in cui l'oggetto deve essere creato
    public Vector3 spawnPosition;
    public Quaternion spawnRotation = Quaternion.identity; // Rotazione predefinita se non specificata

    // Metodo per istanziare l'oggetto nella posizione specificata
    public void SpawnObject()
    {
        if (objectPrefab != null)
        {
            Instantiate(objectPrefab, spawnPosition, spawnRotation);
        }
        else
        {
            Debug.LogError("Object prefab is not assigned.");
        }
    }
}