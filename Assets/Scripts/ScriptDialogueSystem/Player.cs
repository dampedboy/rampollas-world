using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private DialogueUI dialogueUI;
    public DialogueUI DialogueUI => dialogueUI;

    public IInteractable Interactable { get; set; }

    private PlayerMovement playerMovementScript;

    void Start()
    {
        playerMovementScript = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        // Disabilita il movimento del giocatore solo quando il dialogo � aperto
        if (dialogueUI.IsOpen)
        {
            playerMovementScript.enabled = false;
        }
        else
        {
            playerMovementScript.enabled = true;

            if ((Input.GetKeyDown(KeyCode.I) || Input.GetButtonDown("Fire1")) && Interactable != null)
            {
                Interactable.Interact(this);
            }
        }
    }
}
