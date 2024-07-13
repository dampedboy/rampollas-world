using System.Collections.Generic;
using UnityEngine;

public class BlackButtonManager : MonoBehaviour
{
    public GameObject[] blackButtons; // Array di bottoni neri
    public Animator[] buttonAnimators; // Array di animator per i bottoni neri
    public GameObject portalObject; // Oggetto chiave per il YellowButtonController
    public GameObject keyObject; // Oggetto chiave per il BlueButtonController
    public GameObject[] redPlatforms; // Piattaforme per il RedButtonController
    public GameObject[] greenPlatforms; // Piattaforme per il GreenButtonController
    public float platformsVisibleDuration = 5f; // Durata per cui le piattaforme rimangono visibili/invisibili

    private System.Type[] buttonScripts = new System.Type[]
    {
        typeof(RedButtonController),
        typeof(GreenButtonController),
        typeof(BlueButtonController),
        typeof(YellowButtonController)
    };

    void Start()
    {
        // Nasconde inizialmente le piattaforme invisibili per RedButtonController
        foreach (GameObject platform in redPlatforms)
        {
            platform.SetActive(false);
        }

        // Mostra inizialmente le piattaforme visibili per GreenButtonController
        foreach (GameObject platform in greenPlatforms)
        {
            platform.SetActive(true);
        }

        // Assicurati che ci siano esattamente quattro bottoni neri
        if (blackButtons.Length != 4)
        {
            Debug.LogError("Ci devono essere esattamente quattro bottoni neri!");
            return;
        }

        // Randomizza l'ordine degli script
        Shuffle(buttonScripts);

        // Assegna casualmente uno script a ciascun bottone nero
        for (int i = 0; i < blackButtons.Length; i++)
        {
            GameObject button = blackButtons[i];
            Animator animator = buttonAnimators[i];
            System.Type scriptType = buttonScripts[i];

            // Aggiungi lo script al bottone
            var script = button.AddComponent(scriptType);

            // Configura lo script in base al tipo
            if (script is RedButtonController redButton)
            {
                redButton.buttonAnimator = animator;
                redButton.platforms = redPlatforms;
                redButton.platformsVisibleDuration = platformsVisibleDuration;
            }
            else if (script is GreenButtonController greenButton)
            {
                greenButton.buttonAnimator = animator;
                greenButton.platforms = greenPlatforms;
                greenButton.platformsVisibleDuration = platformsVisibleDuration;
            }
            else if (script is BlueButtonController blueButton)
            {
                blueButton.buttonAnimator = animator;
                blueButton.keyObject = keyObject;
            }
            else if (script is YellowButtonController yellowButton)
            {
                yellowButton.buttonAnimator = animator;
                yellowButton.portalObject = portalObject;
            }
        }
    }

    // Funzione per randomizzare l'ordine degli script
    void Shuffle(System.Type[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            System.Type temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }
}