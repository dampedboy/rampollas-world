using UnityEngine;

public class WoodBlock : MonoBehaviour
{
    public GameObject woodBeamPrefab;

    void OnCollisionEnter(Collision collision)
    {
        GameObject otherObject = collision.gameObject;

        // Controlla se l'oggetto che ha causato la collisione è di legno o metallo
        if (otherObject.CompareTag("Wood") || (otherObject.CompareTag("Metal") && IsAbsorbedMetal(otherObject)))
        {
            ReplaceWithBeams(transform.position, transform.rotation);
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
        for (int i = 0; i < beamCount; i++)
        {
            Instantiate(woodBeamPrefab, position, Quaternion.Euler(0, 0, 0));
        }
    }
}