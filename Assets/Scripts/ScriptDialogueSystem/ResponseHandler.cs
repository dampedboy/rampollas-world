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
    [SerializeField] private AudioSource responseAudioSource;
    [SerializeField] private AudioClip responseAudioClip;
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color highlightedColor = Color.yellow;

    private DialogueUI dialogueUI;
    private ResponseEvent[] responseEvents;

    private List<GameObject> tempResponseButtons = new List<GameObject>();
    private int currentResponseIndex = 0;

    private float inputCooldown = 0.15f; // Cooldown per prevenire input multipli
    private float lastInputTime;
    private float axisInputCooldown = 0.15f; // Cooldown per l'asse
    private float lastAxisInputTime;
    private const float deadZone = 0.5f; // Dead zone per evitare piccoli movimenti accidentali

    private void Start()
    {
        dialogueUI = GetComponent<DialogueUI>();
    }

    private void Update()
    {
        if (tempResponseButtons.Count == 0) return;

        // Verifica se è trascorso abbastanza tempo per accettare nuovi input
        if (Time.time - lastInputTime >= inputCooldown)
        {
            HandleInput();
        }
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
        bool inputReceived = false;
        float verticalAxis = Input.GetAxisRaw("Vertical");

        // Gestione dell'asse verticale con dead zone e cooldown
        if (verticalAxis > deadZone && Time.time - lastAxisInputTime >= axisInputCooldown)
        {
            currentResponseIndex = (currentResponseIndex - 1 + tempResponseButtons.Count) % tempResponseButtons.Count;
            inputReceived = true;
            lastAxisInputTime = Time.time;
        }
        else if (verticalAxis < -deadZone && Time.time - lastAxisInputTime >= axisInputCooldown)
        {
            currentResponseIndex = (currentResponseIndex + 1) % tempResponseButtons.Count;
            inputReceived = true;
            lastAxisInputTime = Time.time;
        }

        // Gestione delle frecce su/giù
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentResponseIndex = (currentResponseIndex - 1 + tempResponseButtons.Count) % tempResponseButtons.Count;
            inputReceived = true;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentResponseIndex = (currentResponseIndex + 1) % tempResponseButtons.Count;
            inputReceived = true;
        }

        // Selezione della risposta
        if (inputReceived)
        {
            lastInputTime = Time.time; // Resetta il timer dell'input
            UpdateButtonSelection();
        }

        // Seleziona la risposta con il tasto "P" o il pulsante "Submit"
        if Input.GetKeyDown(KeyCode.O) || Input.GetButtonDown("Fire3"))
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
        // Riproduce il suono della risposta
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
