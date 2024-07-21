using UnityEngine;

public class MetalObject : MonoBehaviour
{
    private int metalCollisionCount = 0;

    void OnCollisionEnter(Collision collision)
    {
        GameObject otherObject = collision.gameObject;

        if ((otherObject.CompareTag("WoodBlock") || otherObject.CompareTag("MetalBlock")) && IsAbsorbedMetal())
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

    private bool IsAbsorbedMetal()
    {
        ObjAbsorbeMetal metalScript = GetComponent<ObjAbsorbeMetal>();
        return metalScript != null && metalScript.isThrown;
    }
}