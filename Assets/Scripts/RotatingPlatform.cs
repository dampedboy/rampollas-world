using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    // Velocità di rotazione in gradi al secondo
    public float rotationSpeed = 50.0f;

    void Update()
    {
        // Calcola la rotazione per questo frame
        float rotation = rotationSpeed * Time.deltaTime;

        // Applica la rotazione alla piattaforma lungo l'asse Y
        transform.Rotate(0, 0, rotation);
    }
}