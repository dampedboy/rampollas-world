using UnityEngine;

public class DestroyAfterDelay : MonoBehaviour
{
    void Start()
    {
        // Avvia il metodo che distrugge l'oggetto dopo 1 secondo
        Destroy(gameObject, 1.5f);
    }
}