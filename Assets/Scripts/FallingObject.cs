using UnityEngine;

public class FallingObject : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlatformTrigger platformTrigger = collision.collider.GetComponent<PlatformTrigger>();

            if (platformTrigger != null)
            {
                platformTrigger.PlayerHit(); // Riduce i cuori del player
            }

            Destroy(gameObject); // Distrugge l'oggetto che cade
        }
    }
}