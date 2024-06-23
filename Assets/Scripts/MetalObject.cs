using UnityEngine;

public class MetalObject : MonoBehaviour
{
    private int metalCollisionCount = 0;

    void OnCollisionEnter(Collision collision)
    {
        GameObject otherObject = collision.gameObject;

        if (otherObject.CompareTag("WoodBlock") || otherObject.CompareTag("MetalBlock"))
        {
            metalCollisionCount++;
            Debug.Log($"Metal collision count: {metalCollisionCount}");

            if (metalCollisionCount >= 2)
            {
                Debug.Log("Metal object destroyed after two collisions.");
                Destroy(gameObject);
            }
        }
    }
}