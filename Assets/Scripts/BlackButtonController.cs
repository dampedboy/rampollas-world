using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackButtonController : MonoBehaviour
{
    public Animator buttonAnimator; // Riferimento all'animator del bottone
    public GameObject[] objectsToMove; // Array di oggetti da spostare
    public Vector3[] targetPositions; // Array di nuove posizioni
    public float moveDuration = 2f; // Durata per muovere gli oggetti verso la nuova posizione
    public float objectsVisibleDuration = 4f; // Tempo in cui gli oggetti restano nella nuova posizione
    public AudioClip buttonPressClip; // Riferimento all'AudioClip

    private bool isButtonPressed = false; // Flag per controllare se il bottone è stato premuto
    private Vector3[] initialPositions; // Array per salvare le posizioni iniziali degli oggetti

    void Start()
    {
        // Controllo per assicurarsi che la lunghezza di objectsToMove e targetPositions sia uguale
        if (objectsToMove.Length != targetPositions.Length)
        {
            Debug.LogError("Il numero di oggetti e di posizioni non corrisponde!");
            return;
        }

        // Inizializza l'array delle posizioni iniziali
        initialPositions = new Vector3[objectsToMove.Length];

        // Salva le posizioni iniziali
        for (int i = 0; i < objectsToMove.Length; i++)
        {
            initialPositions[i] = objectsToMove[i].transform.position; // Salva la posizione iniziale
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Controlla se il player ha toccato il bottone
        if (other.CompareTag("Player") && !isButtonPressed)
        {
            isButtonPressed = true;
            buttonAnimator.SetTrigger("Press"); // Attiva l'animazione del bottone
            PlayButtonPressSound(); // Riproduce il suono del bottone
            StartCoroutine(MoveObjectsWithLerp());
        }
    }

    private void PlayButtonPressSound()
    {
        // Crea un nuovo AudioSource, assegna la clip e riproduce il suono
        AudioSource.PlayClipAtPoint(buttonPressClip, transform.position);
    }

    private IEnumerator MoveObjectsWithLerp()
    {
        // Muove gli oggetti verso la nuova posizione
        yield return StartCoroutine(LerpObjectsToPosition(targetPositions));

        // Mantiene gli oggetti nella nuova posizione per il tempo specificato
        yield return new WaitForSeconds(objectsVisibleDuration);

        // Riporta gli oggetti alla posizione iniziale
        yield return StartCoroutine(LerpObjectsToPosition(initialPositions));
    }

    private IEnumerator LerpObjectsToPosition(Vector3[] targetPositions)
    {
        float timeElapsed = 0f;

        // Salva la posizione iniziale degli oggetti al momento del movimento
        Vector3[] startPositions = new Vector3[objectsToMove.Length];
        for (int i = 0; i < objectsToMove.Length; i++)
        {
            startPositions[i] = objectsToMove[i].transform.position;
        }

        // Esegue il Lerp per ogni oggetto
        while (timeElapsed < moveDuration)
        {
            for (int i = 0; i < objectsToMove.Length; i++)
            {
                objectsToMove[i].transform.position = Vector3.Lerp(startPositions[i], targetPositions[i], timeElapsed / moveDuration);
            }

            timeElapsed += Time.deltaTime;
            yield return null; // Attende il frame successivo
        }

        // Assicura che gli oggetti arrivino esattamente alla posizione target
        for (int i = 0; i < objectsToMove.Length; i++)
        {
            objectsToMove[i].transform.position = targetPositions[i];
        }
    }
}
