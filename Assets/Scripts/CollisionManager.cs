using UnityEngine;
using System.Collections;

public class CollisionManager : MonoBehaviour
{
    // Prefabs for beams and blocks
    public GameObject woodBeamPrefab;
    public GameObject woodBlockPrefab;
    public GameObject metalBlockPrefab;

    private int metalToWoodHitCount = 0;
    private GameObject lastHitWoodBlock;
    private GameObject lastHitMetalObject;
    private bool metalObjectDestroyed = false;

    void OnCollisionEnter(Collision collision)
    {
        GameObject otherObject = collision.gameObject;
        ObjAbsorbeGlass glassScript = GetComponent<ObjAbsorbeGlass>();
        ObjAbsorbeMetal metalScript = GetComponent<ObjAbsorbeMetal>();
        ObjAbsorbeWood woodScript = GetComponent<ObjAbsorbeWood>();

        bool isThrown = (glassScript != null && glassScript.isThrown) ||
                        (metalScript != null && metalScript.isThrown) ||
                        (woodScript != null && woodScript.isThrown);

        if (!isThrown)
        {
            // If the object has not been thrown, do nothing
            return;
        }

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
        if (otherObject.CompareTag("WoodBlock") || otherObject.CompareTag("MetalBlock"))
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
        if (otherObject.CompareTag("MetalBlock"))
        {
            HandleMetalBlockCollision(otherObject);
        }
        else if (otherObject.CompareTag("WoodBlock"))
        {
            HandleWoodBlockCollision(otherObject);
        }
    }

    private void HandleMetalBlockCollision(GameObject metalBlock)
    {
        // Replace metal block with wood block
        lastHitWoodBlock = Instantiate(woodBlockPrefab, metalBlock.transform.position, metalBlock.transform.rotation);
        Destroy(metalBlock);
    }

    private void HandleWoodBlockCollision(GameObject woodBlock)
    {
        if (lastHitWoodBlock == woodBlock)
        {
            // Check if metal object was previously destroyed
            if (!metalObjectDestroyed)
            {
                // Mark metal object as destroyed
                metalObjectDestroyed = true;

                // Store the metal object
                lastHitMetalObject = woodBlock;

                // Replace wood block with beams after a delay
                StartCoroutine(ReplaceWithBeamsAfterDelay(woodBlock.transform.position, woodBlock.transform.rotation));
            }
            else
            {
                // Destroy the wood block immediately
                Destroy(woodBlock);
            }

            // Destroy this game object
            Destroy(gameObject);
        }
    }

    private IEnumerator ReplaceWithBeamsAfterDelay(Vector3 position, Quaternion rotation)
    {
        // Wait for a few seconds
        yield return new WaitForSeconds(2f);

        // Replace wood block with beams
        ReplaceWithBeams(position, rotation);

        // Destroy the wood block
        Destroy(lastHitWoodBlock);

        // Wait for another few seconds
        yield return new WaitForSeconds(2f);

        // Destroy the metal object
        Destroy(lastHitMetalObject);
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