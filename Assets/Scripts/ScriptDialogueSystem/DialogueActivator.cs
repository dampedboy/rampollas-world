using UnityEngine;

public class DialogueActivator : MonoBehaviour, IInteractable
{
    // Parte aggiunta per mostrare icona talk
    [SerializeField] GameObject uiPanelTalk;

    // Variabili per il suono
    [SerializeField] private AudioClip dialogueSound;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private DialogueObject dialogueObject;

    public void UpdateDialogueObject(DialogueObject dialogueObject)
    {
        this.dialogueObject = dialogueObject;
    }

    public void Interact(Player player)
    {
        foreach (DialogueResponseEvents responseEvents in GetComponents<DialogueResponseEvents>())
        {
            if (responseEvents.DialogueObject == dialogueObject)
            {
                player.DialogueUI.AddResponseEvents(responseEvents.Events);
                break;
            }
        }

        player.DialogueUI.ShowDialogue(dialogueObject);

        // Riproduci il suono quando il dialogo è attivato
        if (audioSource != null && dialogueSound != null)
        {
            audioSource.PlayOneShot(dialogueSound);
        }
    }

    // Parte aggiunta per mostrare icona talk
    void Start()
    {
        if (uiPanelTalk != null)
        {
            uiPanelTalk.SetActive(false);
        }

        // Inizializza l'AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out Player player))
        {
            player.Interactable = this;
            // Parte aggiunta per mostrare icona talk
            uiPanelTalk.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out Player player))
        {
            if (player.Interactable is DialogueActivator dialogueActivator && dialogueActivator == this)
            {
                player.Interactable = null;
                // Parte aggiunta per mostrare icona talk
                uiPanelTalk.SetActive(false);
            }
        }
    }
}
