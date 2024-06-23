using UnityEngine;

public class WoodBlock : MonoBehaviour
{
    public GameObject woodBeamPrefab;

    void OnCollisionEnter(Collision collision)
    {
        GameObject otherObject = collision.gameObject;

        if (otherObject.CompareTag("Wood") || otherObject.CompareTag("Metal"))
        {
            ReplaceWithBeams(transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    private void ReplaceWithBeams(Vector3 position, Quaternion rotation)
    {
        int beamCount = Random.Range(4, 8);
        for (int i = 0; i < beamCount; i++)
        {
            Instantiate(woodBeamPrefab, position, Quaternion.Euler(90, 0, 90));
        }
    }
}