using UnityEngine;

public class VerticalPlatform : MonoBehaviour
{
    // Posizione iniziale della piattaforma
    private Vector3 startPosition;

    // Posizione target finale della piattaforma
    public Vector3 targetPosition;

    // Velocità di movimento della piattaforma
    public float speed = 2.0f;

    // Timer per il movimento
    private float time;

    void Start()
    {
        // Salva la posizione iniziale della piattaforma
        startPosition = transform.position;
    }

    void Update()
    {
        // Aggiorna il timer basato sul tempo reale e sulla velocità
        time += Time.deltaTime * speed;

        // Movimento della piattaforma avanti e indietro tra startPosition e targetPosition
        transform.position = Vector3.Lerp(startPosition, targetPosition, Mathf.PingPong(time, 1.0f));
    }
}