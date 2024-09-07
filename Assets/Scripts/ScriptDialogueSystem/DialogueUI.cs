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
    public GameObject chatBubble;
    public Chat_Bubble_Spawn chatBubbleSpawn; // Reference to Chat_Bubble_Spawn script

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
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.O) || Input.GetButtonDown("Fire3"));
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
            if (Input.GetKeyDown(KeyCode.O) || Input.GetButtonDown("Fire3"))
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
            // Destroy objects as before
            Destroy(gameObject);
            Destroy(malocchio);
            Destroy(canvaTalk);
            Destroy(chatBubble);

            // Destroy chat bubble via Chat_Bubble_Spawn
            if (chatBubbleSpawn != null)
            {
                chatBubbleSpawn.DestroySpawnedObject();
            }

            portal.SetActive(true);

            // Play background music for the next scene
            PlayBackgroundMusic();
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
            MusicPlayer.Instance.PlayNewBackgroundMusic(backgroundMusicClip);
        }
    }
}
