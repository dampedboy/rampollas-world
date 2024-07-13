using UnityEngine;
using System.Collections;

public class CollisionManager : MonoBehaviour
{
    public GameObject woodBeamPrefab;
    public GameObject woodBlockPrefab;
    public GameObject metalBlockPrefab;

    private int metalCollisionCount = 0;

    void OnCollisionEnter(Collision collision)
    {
        GameObject otherObject = collision.gameObject;
        ObjAbsorbeGlass glassScript = otherObject.GetComponent<ObjAbsorbeGlass>();
        ObjAbsorbeMetal metalScript = otherObject.GetComponent<ObjAbsorbeMetal>();
        ObjAbsorbeWood woodScript = otherObject.GetComponent<ObjAbsorbeWood>();

        bool isThrown = (glassScript != null && glassScript.isThrown) ||
                        (metalScript != null && metalScript.isThrown) ||
                        (woodScript != null && woodScript.isThrown);

        Debug.Log($"Collision detected between {gameObject.tag} and {otherObject.tag}. IsThrown: {isThrown}");

        if (!isThrown)
        {
            Debug.Log("Object was not thrown, ignoring collision.");
            return;
        }

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
        else if (gameObject.CompareTag("WoodBlock"))
        {
            HandleWoodBlockCollision(otherObject);
        }
        else if (gameObject.CompareTag("MetalBlock"))
        {
            HandleMetalBlockCollision(otherObject);
        }
    }

    private void HandleGlassCollision(GameObject otherObject)
    {
        if (otherObject.CompareTag("WoodBlock") || otherObject.CompareTag("MetalBlock"))
        {
            Debug.Log("Glass object destroyed.");
            Destroy(gameObject);
        }
    }

    private void HandleWoodCollision(GameObject otherObject)
    {
        if (otherObject.CompareTag("WoodBlock"))
        {
            ReplaceWithBeams(otherObject.transform.position, otherObject.transform.rotation);
            Destroy(otherObject);
            Destroy(gameObject);
        }
        else if (otherObject.CompareTag("MetalBlock"))
        {
            Destroy(gameObject);
        }
    }

    private void HandleMetalCollision(GameObject otherObject)
    {
        if (otherObject.CompareTag("WoodBlock") || otherObject.CompareTag("MetalBlock"))
        {
            metalCollisionCount++;
            Debug.Log($"Metal collision count: {metalCollisionCount}");

            if (metalCollisionCount == 2)
            {
                Debug.Log("Metal object destroyed after two collisions.");
                Destroy(gameObject);
            }
        }
    }

    private void HandleWoodBlockCollision(GameObject otherObject)
    {
        if (otherObject.CompareTag("Glass"))
        {
            Debug.Log("WoodBlock hit by Glass.");
            Destroy(otherObject);        
        }
        if (otherObject.CompareTag("Wood"))
        {
            Debug.Log("WoodBlock hit by Wood. Replacing with beams.");
            ReplaceWithBeams(otherObject.transform.position, otherObject.transform.rotation);
            Destroy(otherObject);
            Destroy(gameObject);
        }
        if(otherObject.CompareTag("Metal"))
        {
            Debug.Log("WoodBlock hit by Metal. Replacing with beams.");
            ReplaceWithBeams(otherObject.transform.position, otherObject.transform.rotation);
            Destroy(gameObject);
        }
    }

    private void HandleMetalBlockCollision(GameObject otherObject)
    {
        if (otherObject.CompareTag("Glass"))
        {
            Destroy(otherObject);
        }
        if (otherObject.CompareTag("Wood"))
        {
            Destroy(otherObject);
        }
        if (otherObject.CompareTag("Metal"))
        {
            ReplaceWithWood(otherObject.transform.position, otherObject.transform.rotation);
            Destroy(gameObject);
            if (metalCollisionCount == 2)
            {
                Destroy(otherObject);
            }
        }
    }

    private void ReplaceWithWood(Vector3 position, Quaternion rotation)
    {
        Instantiate(woodBlockPrefab, position, rotation);
  
    }

    private void ReplaceWithBeams(Vector3 position, Quaternion rotation)
    {
        int beamCount = Random.Range(5, 9);
        for (int i = 0; i < beamCount; i++)
        {
            Instantiate(woodBeamPrefab, position, Quaternion.Euler(90, 0, 90));  // Ruota la trave in orizzontale
        }
        Debug.Log($"Spawned {beamCount} wood beams.");
    }
}