using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private AudioClip nextDialogueClip; // Campo per l'audio clip
    [SerializeField] private AudioSource audioSource; // Campo per l'AudioSource
    public GameObject malocchio; // Riferimento a Malocchio
    public GameObject portal; // Riferimento all'oggetto Portal
    public GameObject canvaTalk; // Riferimento all'oggetto canvaTalk

    public bool IsOpen { get; private set; }

    // per le opzioni
    private ResponseHandler responseHandler;
    private TypewriterEffect typewriterEffect;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Malocchio")
        {
            portal.SetActive(false); // Rende il portale invisibile all'inizio
        }
            
        typewriterEffect = GetComponent<TypewriterEffect>();

        // per le opzioni:
        responseHandler = GetComponent<ResponseHandler>();

        // Aggiungi il componente AudioSource se non � gi� assegnato
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Chiude dialogo
        CloseDialogueBox();
    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        IsOpen = true;
        // mostra dialogo all'inizio mettendolo attivo
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    public void AddResponseEvents(ResponseEvent[] responseEvents)
    {
        responseHandler.AddResponseEvents(responseEvents);
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        // per le opzioni, non fa premere spazio se ci sono:
        for (int i = 0; i < dialogueObject.Dialogue.Length; i++)
        {
            string dialogue = dialogueObject.Dialogue[i];

            yield return RunTypingEffect(dialogue);

            textLabel.text = dialogue;

            // per scrivere passo passo:
            if (i == dialogueObject.Dialogue.Length - 1 && dialogueObject.HasResponses) break;

            yield return null;

            // aspetta che premo spazio per passare al prossimo dialogo
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

            // Riproduce il suono next_dialogue
            PlayNextDialogueSound();
        }

        if (dialogueObject.HasResponses)
        {
            responseHandler.ShowResponses(dialogueObject.Responses);
        }
        else
        {
            CloseDialogueBoxFinal();
        }
    }

    private IEnumerator RunTypingEffect(string dialogue)
    {
        typewriterEffect.Run(dialogue, textLabel);

        while (typewriterEffect.IsRunning)
        {
            yield return null;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                typewriterEffect.Stop();
            }
        }
    }

    // per chiudere dialogo quando finisce
    public void CloseDialogueBox()
    {
        IsOpen = false;
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
    }

    public void CloseDialogueBoxFinal()
    {
        IsOpen = false;
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
        if (SceneManager.GetActiveScene().name == "Malocchio")
        {
            Destroy(gameObject); // Distruggi l'oggetto chiave
            Destroy(malocchio); // Distruggi l'oggetto Malocchio
            Destroy(canvaTalk); // Distruggi l'oggetto canvaTalk

            portal.SetActive(true); // Rendi il portale visibile
        }
        
    }


    // Funzione per riprodurre il suono next_dialogue
    private void PlayNextDialogueSound()
    {
        if (nextDialogueClip != null)
        {
            audioSource.PlayOneShot(nextDialogueClip);
        }
    }
}
