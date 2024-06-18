using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    // Prefabs for beams and blocks
    public GameObject woodBeamPrefab;
    public GameObject woodBlockPrefab;
    public GameObject metalBlockPrefab;

    private int metalToWoodHitCount = 0;

    void OnCollisionEnter(Collision collision)
    {
        GameObject otherObject = collision.gameObject;

        // Check the tags of the colliding objects
        if (gameObject.CompareTag("Glass"))
        {
            if (otherObject.CompareTag("WoodBlock") || otherObject.CompareTag("MetalBlock"))
            {
                // Destroy glass object
                Destroy(gameObject);
            }
        }
        else if (gameObject.CompareTag("Wood"))
        {
            if (otherObject.CompareTag("WoodBlock"))
            {
                // Destroy wood object and replace wood block with beams
                Destroy(gameObject);
                ReplaceWithBeams(otherObject.transform.position, otherObject.transform.rotation);
                Destroy(otherObject);
            }
        }
        else if (gameObject.CompareTag("Metal"))
        {
            if (otherObject.CompareTag("WoodBlock"))
            {
                // Replace wood block with beams
                ReplaceWithBeams(otherObject.transform.position, otherObject.transform.rotation);
                Destroy(otherObject);
            }
            else if (otherObject.CompareTag("MetalBlock"))
            {
                metalToWoodHitCount++;

                if (metalToWoodHitCount == 1)
                {
                    // Replace metal block with wood block
                    Instantiate(woodBlockPrefab, otherObject.transform.position, otherObject.transform.rotation);
                    Destroy(otherObject);
                }
                else if (metalToWoodHitCount == 2)
                {
                    // Destroy metal object and replace wood block with beams
                    ReplaceWithBeams(otherObject.transform.position, otherObject.transform.rotation);
                    Destroy(gameObject);
                    Destroy(otherObject);
                }
            }
        }
    }

    private void ReplaceWithBeams(Vector3 position, Quaternion rotation)
    {
        int beamCount = Random.Range(4, 8);
        for (int i = 0; i < beamCount; i++)
        {
            Instantiate(woodBeamPrefab, position, rotation);
        }
    }
}