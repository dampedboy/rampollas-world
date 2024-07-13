using UnityEngine;

public class GlassObject : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        GameObject otherObject = collision.gameObject;

        if (otherObject.CompareTag("WoodBlock") || otherObject.CompareTag("MetalBlock"))
        {
            Debug.Log("Glass object destroyed.");
            Destroy(gameObject);
        }
    }
}
