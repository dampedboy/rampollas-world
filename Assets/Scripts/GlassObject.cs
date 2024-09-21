using UnityEngine;
using System.Collections;

public class GlassObject : MonoBehaviour
{
    public AudioClip glassBreakSound; // Riferimento al suono di rottura del vetro

    void OnCollisionEnter(Collision collision)
    {
        GameObject otherObject = collision.gameObject;

        if (otherObject.CompareTag("WoodBlock") || otherObject.CompareTag("MetalBlock") || otherObject.CompareTag("SteelBlock")||otherObject.CompareTag("PlasmaBlock"))
        {
            PlayGlassBreakSound();
            Debug.Log("Glass object destroyed.");
            Destroy(gameObject);
        }
        else if (otherObject.CompareTag("GlassBlock"))
        {
            Debug.Log("Wood object destroyed.");
            Destroy(gameObject);
        }
    }

    private void PlayGlassBreakSound()
    {
        if (glassBreakSound != null)
        {
            AudioSource.PlayClipAtPoint(glassBreakSound, transform.position, 10f);
        }
        
    }
}
