using UnityEngine;

public class Cracked : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<CharacterController>() != null)
        {
            Disappear();
        }
    }

    void Disappear()
    {
        // Distruggi la piattaforma
        Destroy(gameObject);
    }
}