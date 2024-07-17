using UnityEngine;

public class GlassObject : MonoBehaviour
{
    public AudioClip glassBreakSound; // Riferimento al suono di rottura del vetro

    void OnCollisionEnter(Collision collision)
    {
        GameObject otherObject = collision.gameObject;

        if (otherObject.CompareTag("WoodBlock") || otherObject.CompareTag("MetalBlock"))
        {
            PlayGlassBreakSound();
            Debug.Log("Glass object destroyed.");
            Destroy(gameObject);
        }
    }

    private void PlayGlassBreakSound()
    {
        if (glassBreakSound != null)
        {
            AudioSource.PlayClipAtPoint(glassBreakSound, transform.position);
        }
    }
}
