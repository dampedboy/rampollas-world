using System.Collections;
using UnityEngine;

public class Chat_Bubble_Spawn : MonoBehaviour
{
    [SerializeField] private GameObject objectToSpawn; // L'oggetto prefab da spawnare
    [SerializeField] private float spawnHeight = 3.0f; // Altezza a cui spawnare l'oggetto
    [SerializeField] private float animationDuration = 1.0f; // Durata dell'animazione di ingrandimento
    private GameObject spawnedObject; // Riferimento all'oggetto istanziato

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SpawnObject();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (spawnedObject != null)
            {
                StartCoroutine(AnimateScaleDownAndDestroy(spawnedObject));
            }
        }
    }
private void SpawnObject()
{
    if (objectToSpawn == null)
    {
        Debug.LogError("Object to spawn is not assigned.");
        return;
    }

    Vector3 spawnPosition = transform.position + new Vector3(0, spawnHeight, 0);
    Quaternion spawnRotation = Quaternion.Euler(0, 270, 0); // Rotazione di 180 gradi sull'asse Y
    spawnedObject = Instantiate(objectToSpawn, spawnPosition, spawnRotation);
    StartCoroutine(AnimateScale(spawnedObject));
}

    // Coroutine per animare l'ingrandimento dell'oggetto
    private IEnumerator AnimateScale(GameObject obj)
    {
        Vector3 initialScale = Vector3.zero;
        Vector3 finalScale = obj.transform.localScale;
        obj.transform.localScale = initialScale;

        float elapsedTime = 0;
        while (elapsedTime < animationDuration)
        {
            obj.transform.localScale = Vector3.Lerp(initialScale, finalScale, elapsedTime / animationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        obj.transform.localScale = finalScale;
    }

    // Coroutine per animare il rimpicciolimento e distruggere l'oggetto
    private IEnumerator AnimateScaleDownAndDestroy(GameObject obj)
    {
        Vector3 initialScale = obj.transform.localScale;
        Vector3 finalScale = Vector3.zero;

        float elapsedTime = 0;
        while (elapsedTime < animationDuration)
        {
            obj.transform.localScale = Vector3.Lerp(initialScale, finalScale, elapsedTime / animationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(obj);
    }

    void Update(){
        if(spawnedObject!=null){
        Vector3 updatedPosition = transform.position + new Vector3(0, spawnHeight, 0);
        spawnedObject.transform.position = updatedPosition;
        }
    }
}