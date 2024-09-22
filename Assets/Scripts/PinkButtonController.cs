using System.Collections.Generic;
using UnityEngine;

public class PinkButtonController : MonoBehaviour
{
    public Animator buttonAnimator; // Riferimento all'animator del bottone
    public float additionalDuration = 5f; // Durata extra da aggiungere agli altri bottoni
    public AudioClip buttonPressClip; // Riferimento all'AudioClip
    public List<MonoBehaviour> targetButtons; // Lista di bottoni temporanei assegnati dall'Inspector

    private bool isButtonPressed = false; // Flag per controllare se il bottone è stato premuto

    void OnTriggerEnter(Collider other)
    {
        // Controlla se il player ha toccato il bottone
        if (other.CompareTag("Player") && !isButtonPressed)
        {
            isButtonPressed = true;
            buttonAnimator.SetTrigger("Press"); // Attiva l'animazione del bottone
            PlayButtonPressSound(); // Riproduce il suono del bottone

            // Riavvia i bottoni presenti nella lista
            RestartTargetButtons();
        }
    }

    private void PlayButtonPressSound()
    {
        // Crea un nuovo AudioSource, assegna la clip e riproduce il suono
        AudioSource.PlayClipAtPoint(buttonPressClip, transform.position);
    }

    private void RestartTargetButtons()
    {
        // Scorre ogni bottone nella lista e chiama la funzione di riavvio se implementa ITemporarilyActivatableButton
        foreach (var button in targetButtons)
        {
            ITemporarilyActivatableButton activatableButton = button as ITemporarilyActivatableButton;
            if (activatableButton != null)
            {
                activatableButton.ReactivateEffect(additionalDuration); // Riattiva l'effetto per una durata extra
            }
        }
    }
}
