using TMPro;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResponseHandler : MonoBehaviour
{
    [SerializeField] private RectTransform responseBox;
    [SerializeField] private RectTransform responseButtonTemplate;
    [SerializeField] private RectTransform responseContainer;
    [SerializeField] private AudioSource responseAudioSource; // Add this line
    [SerializeField] private AudioClip responseAudioClip; // Add this line
    [SerializeField] private Color normalColor = Color.white; // Colore normale per i bottoni
    [SerializeField] private Color highlightedColor = Color.yellow; // Colore evidenziato per i bottoni

    private DialogueUI dialogueUI;
    private ResponseEvent[] responseEvents;

    private List<GameObject> tempResponseButtons = new List<GameObject>();
    private int currentResponseIndex = 0;

    private void Start()
    {
        dialogueUI = GetComponent<DialogueUI>();
    }

    private void Update()
    {
        if (tempResponseButtons.Count == 0) return;

        HandleInput();
    }

    public void AddResponseEvents(ResponseEvent[] responseEvents)
    {
        this.responseEvents = responseEvents;
    }

    public void ShowResponses(Response[] responses)
    {
        float responseBoxHeight = 0;
        currentResponseIndex = 0;

        for (int i = 0; i < responses.Length; i++)
        {
            Response response = responses[i];
            int responseIndex = i;

            GameObject responseButton = Instantiate(responseButtonTemplate.gameObject, responseContainer);
            responseButton.gameObject.SetActive(true);
            responseButton.GetComponent<TMP_Text>().text = response.ResponseText;
            responseButton.GetComponent<Button>().onClick.AddListener(() => OnPickedResponse(response, responseIndex));

            tempResponseButtons.Add(responseButton);

            responseBoxHeight += responseButtonTemplate.sizeDelta.y;
        }

        responseBox.sizeDelta = new Vector2(responseBox.sizeDelta.x, responseBoxHeight);
        responseBox.gameObject.SetActive(true);

        UpdateButtonSelection();
    }

    private void HandleInput()
    {
        // Navigazione su/giù
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetAxisRaw("Vertical") > 0)
        {
            currentResponseIndex = (currentResponseIndex - 1 + tempResponseButtons.Count) % tempResponseButtons.Count;
            UpdateButtonSelection();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetAxisRaw("Vertical") < 0)
        {
            currentResponseIndex = (currentResponseIndex + 1) % tempResponseButtons.Count;
            UpdateButtonSelection();
        }

        // Seleziona con il tasto "P" o con il pulsante "Sopra" del controller
        if (Input.GetKeyDown(KeyCode.P) || Input.GetButtonDown("Submit"))
        {
            tempResponseButtons[currentResponseIndex].GetComponent<Button>().onClick.Invoke();
        }
    }

    private void UpdateButtonSelection()
    {
        for (int i = 0; i < tempResponseButtons.Count; i++)
        {
            TMP_Text buttonText = tempResponseButtons[i].GetComponent<TMP_Text>();
            buttonText.color = (i == currentResponseIndex) ? highlightedColor : normalColor;
        }
    }

    private void OnPickedResponse(Response response, int responseIndex)
    {
        // Play the response sound
        if (responseAudioSource != null && responseAudioClip != null)
        {
            responseAudioSource.PlayOneShot(responseAudioClip);
        }

        responseBox.gameObject.SetActive(false);

        foreach (GameObject button in tempResponseButtons)
        {
            Destroy(button);
        }
        tempResponseButtons.Clear();

        if (responseEvents != null && responseIndex < responseEvents.Length)
        {
            responseEvents[responseIndex].OnPickedResponse?.Invoke();
        }

        responseEvents = null;

        if (response.DialogueObject)
        {
            dialogueUI.ShowDialogue(response.DialogueObject);
        }
        else
        {
            dialogueUI.CloseDialogueBox();
        }
    }
}
