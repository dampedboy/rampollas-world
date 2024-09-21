using UnityEngine;

public class MetalObject : MonoBehaviour
{
    private int metalCollisionCount = 0;

    void OnCollisionEnter(Collision collision)
    {
        GameObject otherObject = collision.gameObject;

        if ((otherObject.CompareTag("WoodBlock") || otherObject.CompareTag("MetalBlock") || otherObject.CompareTag("GlassBlock") || otherObject.CompareTag("SteelBlock")) && IsAbsorbedMetal())
        {
            metalCollisionCount++;
            Debug.Log($"Metal collision count: {metalCollisionCount}");

            if (metalCollisionCount >= 2)
            {
                Debug.Log("Metal object destroyed after two collisions.");
                Destroy(gameObject);
            }
        }
        else if (otherObject.CompareTag("PlasmaBlock")){
            Destroy(gameObject);
        }
    }

    private bool IsAbsorbedMetal()
    {
        ObjAbsorber metalScript = GetComponent<ObjAbsorber>();
        return metalScript != null && metalScript.isThrown;
    }
}