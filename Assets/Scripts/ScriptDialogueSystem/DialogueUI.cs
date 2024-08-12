using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private AudioClip nextDialogueClip;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip backgroundMusicClip;

    public GameObject malocchio;
    public GameObject portal;
    public GameObject canvaTalk;

    public bool IsOpen { get; private set; }

    private ResponseHandler responseHandler;
    private TypewriterEffect typewriterEffect;
    private static bool musicPlaying = false;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Malocchio")
        {
            portal.SetActive(false);
        }

        typewriterEffect = GetComponent<TypewriterEffect>();
        responseHandler = GetComponent<ResponseHandler>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        CloseDialogueBox();
    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        IsOpen = true;
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    public void AddResponseEvents(ResponseEvent[] responseEvents)
    {
        responseHandler.AddResponseEvents(responseEvents);
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        for (int i = 0; i < dialogueObject.Dialogue.Length; i++)
        {
            string dialogue = dialogueObject.Dialogue[i];

            yield return RunTypingEffect(dialogue);

            textLabel.text = dialogue;

            if (i == dialogueObject.Dialogue.Length - 1 && dialogueObject.HasResponses) break;

            yield return null;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
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

        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "Malocchio")
        {
            Destroy(gameObject);
            Destroy(malocchio);
            Destroy(canvaTalk);
            portal.SetActive(true);

            if (sceneName == "Malocchio" && sceneName == "Prima Prova" &&
                sceneName == "Seconda Prova" && sceneName == "Terza Prova" &&
                sceneName == "Quarta Prova" && sceneName == "Quinta Prova")
            {
                PlayBackgroundMusic();
                Debug.Log("playing music");
                musicPlaying = true;
                DontDestroyOnLoad(audioSource.gameObject);
            }
        }


    }

    private void PlayNextDialogueSound()
    {
        if (nextDialogueClip != null)
        {
            audioSource.PlayOneShot(nextDialogueClip);
        }
    }

    private void PlayBackgroundMusic()
    {
        if (backgroundMusicClip != null)
        {
            audioSource.clip = backgroundMusicClip;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    private void Update()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (musicPlaying && (sceneName != "Malocchio" || sceneName != "Prima Prova" ||
            sceneName != "Seconda Prova" || sceneName != "Terza Prova" ||
            sceneName != "Quarta Prova" || sceneName != "Quinta Prova"))
        {
            StopBackgroundMusic();
        }
    }

    private void StopBackgroundMusic()
    {
        audioSource.Stop();
        Destroy(audioSource.gameObject);
        musicPlaying = false;
    }
}
