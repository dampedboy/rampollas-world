using UnityEngine;

public class WoodBlock : MonoBehaviour
{
    public GameObject woodBeamPrefab;
    public Transform emptyTransform; // Riferimento all'oggetto Empty

    void OnCollisionEnter(Collision collision)
    {
        GameObject otherObject = collision.gameObject;

        // Controlla se l'oggetto che ha causato la collisione è di legno o metallo
        if (otherObject.CompareTag("Wood") || (otherObject.CompareTag("Metal") && IsAbsorbedMetal(otherObject)))
        {
            ReplaceWithBeams(emptyTransform.position, emptyTransform.rotation); // Usa la posizione dell'Empty
            Destroy(gameObject);
        }
    }

    private bool IsAbsorbedMetal(GameObject otherObject)
    {
        ObjAbsorbeMetal metalScript = otherObject.GetComponent<ObjAbsorbeMetal>();
        return metalScript != null && metalScript.isThrown;
    }

    private void ReplaceWithBeams(Vector3 position, Quaternion rotation)
    {
        int beamCount = Random.Range(4, 8);
        float radius = 2.5f;  // Raggio del cerchio in cui spargere le travi
        for (int i = 0; i < beamCount; i++)
        {
            Vector3 randomPosition = position + new Vector3(Random.Range(-radius, radius), 0, Random.Range(-radius, radius));
            Instantiate(woodBeamPrefab, randomPosition, Quaternion.Euler(0, Random.Range(0, 360), 0));
        }
    }
}