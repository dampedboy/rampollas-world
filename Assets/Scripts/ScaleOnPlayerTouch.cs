using UnityEngine;

public class ScaleOnPlayerTouch : MonoBehaviour
{
    // Velocità di riduzione della scala
    public float scalingSpeed = 1f;

    // Flag per sapere se il player sta toccando la piattaforma
    private bool playerTouched = false;

    // Memorizza la scala originale della piattaforma
    private Vector3 targetScale = Vector3.zero; // Scala target (scomparire)

    // Funzione chiamata quando un Collider entra nel trigger della piattaforma
    private void OnTriggerEnter(Collider other)
    {
        if (!playerTouched && other.CompareTag("Player") && other.GetComponent<CharacterController>())
        {
            playerTouched = true;  // Imposta il flag solo al primo tocco
        }
    }

    // Funzione che viene chiamata ad ogni frame
    private void Update()
    {
        if (playerTouched)
        {
            // Riduce la scala della piattaforma verso zero (scomparire)
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, scalingSpeed * Time.deltaTime);

            // Se la piattaforma è quasi scomparsa, disabilitala
            if (transform.localScale.magnitude < 0.01f)
            {
                // Puoi disabilitare l'oggetto o distruggerlo
                gameObject.SetActive(false); // Disattiva la piattaforma
            }
        }
    }
}

