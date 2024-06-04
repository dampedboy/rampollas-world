using UnityEngine;

public class Projectile : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1);
            }
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject, 2f); // Distrugge il proiettile dopo 2 secondi se non colpisce il player
        }
    }
}