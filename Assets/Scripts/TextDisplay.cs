using System.Collections;
using UnityEngine;
using TMPro;

public class TextDisplay : MonoBehaviour
{
    // Riferimento al componente TextMeshPro
    public TextMeshProUGUI textMeshPro;

    void Start()
    {
        
        

        // Assicurati che il testo sia visibile all'inizio
        textMeshPro.enabled = true;

        // Avvia la coroutine per nascondere il testo dopo 3 secondi
        StartCoroutine(HideTextAfterDelay(3f));
    }

    private IEnumerator HideTextAfterDelay(float delay)
    {
        // Attendi il ritardo specificato
        yield return new WaitForSeconds(delay);

        // Nascondi il testo
        textMeshPro.enabled = false;
    }
}