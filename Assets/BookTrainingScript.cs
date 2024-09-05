using UnityEngine;
using UnityEngine.UI;

public class BookTrainingScript : MonoBehaviour
{
    public GameObject player; // Riferimento al personaggio principale
    public Image displayImage; // Riferimento all'oggetto UI Image
    public Sprite[] images; // Array di immagini da mostrare
    public float interactionDistance = 2.0f; // Distanza di interazione
    private int currentImageIndex = -1; // Indice dell'immagine corrente
    private bool isViewingImages = false; // Flag per controllare se il giocatore sta visualizzando immagini

    void Update()
    {
        // Controlla la distanza tra il player e l'oggetto
        if (Vector3.Distance(player.transform.position, transform.position) < interactionDistance)
        {
            // Se il giocatore preme il tasto T o il pulsante in alto del controller (Jump) e non sta già visualizzando immagini
            if ((Input.GetKeyDown(KeyCode.I) || Input.GetButtonDown("Jump")) && !isViewingImages)
            {
                StartViewingImages();
            }
            // Se il giocatore preme il tasto T o il pulsante in alto del controller (Jump) e sta visualizzando immagini
            else if ((Input.GetKeyDown(KeyCode.I) || Input.GetButtonDown("Jump")) && isViewingImages)
            {
                ShowNextImage();
            }

        }
    }

    void StartViewingImages()
    {
        currentImageIndex = 0;
        displayImage.enabled = true;  // Per mostrare l'immagine
        isViewingImages = true;
        player.GetComponent<PlayerMovement>().enabled = false; // Disabilita il movimento del player
        ShowImage();
    }

    void ShowNextImage()
    {
        currentImageIndex++;
        if (currentImageIndex < images.Length)
        {
            ShowImage();
        }
        else
        {
            ExitViewingImages();
        }
    }

    void ShowImage()
    {
        displayImage.sprite = images[currentImageIndex];
        displayImage.enabled = true; // Assicurati che l'immagine sia visibile
    }

    void ExitViewingImages()
    {
        displayImage.enabled = false;
        isViewingImages = false;
        player.GetComponent<PlayerMovement>().enabled = true; // Riabilita il movimento del player
        currentImageIndex = -1;
    }
}