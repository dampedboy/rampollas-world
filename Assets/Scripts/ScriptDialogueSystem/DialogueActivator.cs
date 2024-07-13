
using UnityEngine;

public class DialogueActivator: MonoBehaviour, IInteractable
{
    //parte aggiunta per mostrare icona talk
    [SerializeField] GameObject uiPanelTalk;


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
    }


    //parte aggiunta per mostrare icona talk
    void Start()
    {
        if (uiPanelTalk != null)
        {
            uiPanelTalk.SetActive(false);
        }
    }






    private void OnTriggerEnter(Collider other)
    {


        if (other.CompareTag("Player") && other.TryGetComponent(out Player player))
        {
            player.Interactable = this;
            //parte aggiunta per mostrare icona talk
            uiPanelTalk.SetActive(true);

        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out Player player))
        {
            if(player.Interactable is DialogueActivator dialogueActivator && dialogueActivator == this)
            {
                player.Interactable = null;
                //parte aggiunta per mostrare icona talk
                uiPanelTalk.SetActive(false);

            }
        }

    }

}
