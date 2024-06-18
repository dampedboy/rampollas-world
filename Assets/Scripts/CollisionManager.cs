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
            HandleGlassCollision(otherObject);
        }
        else if (gameObject.CompareTag("Wood"))
        {
            HandleWoodCollision(otherObject);
        }
        else if (gameObject.CompareTag("Metal"))
        {
            HandleMetalCollision(otherObject);
        }
    }

    private void HandleGlassCollision(GameObject otherObject)
    {
        if (otherObject.CompareTag("WoodBlock"))
        {
            // Destroy glass object
            Destroy(gameObject);
        }
        else if (otherObject.CompareTag("MetalBlock"))
        {
            // Destroy glass object
            Destroy(gameObject);
        }
    }

    private void HandleWoodCollision(GameObject otherObject)
    {
        if (otherObject.CompareTag("WoodBlock"))
        {
            // Replace wood block with beams
            ReplaceWithBeams(otherObject.transform.position, otherObject.transform.rotation);
            Destroy(otherObject);
            Destroy(gameObject);
        }
        else if (otherObject.CompareTag("MetalBlock"))
        {
            // Destroy wood object
            Destroy(gameObject);
        }
    }

    private void HandleMetalCollision(GameObject otherObject)
    {
        if (otherObject.CompareTag("WoodBlock"))
        {
            // Replace wood block with beams
            ReplaceWithBeams(otherObject.transform.position, otherObject.transform.rotation);
            Destroy(otherObject);
            Destroy(gameObject);
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
                // Replace wood block with beams
                ReplaceWithBeams(otherObject.transform.position, otherObject.transform.rotation);
                Destroy(otherObject);
                // Destroy metal object
                Destroy(gameObject);
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
