using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public static bool IsPaused = false;

    public Button[] menuButtons; // Assign buttons for each menu option in the Inspector
    public Color normalColor = Color.white;
    public Color highlightedColor = Color.yellow;

    private int currentButtonIndex = 0;
    private float inputCooldown = 0.15f;
    private float lastInputTime;
    private float axisInputCooldown = 0.15f;
    private float lastAxisInputTime;
    private const float deadZone = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        UpdateButtonSelection();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown("joystick button 7"))
        {
            if (IsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        if (IsPaused)
        {
            if (Time.time - lastInputTime >= inputCooldown)
            {
                HandleInput();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
        currentButtonIndex = 0; // Start with the first button highlighted
        UpdateButtonSelection();
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }

    public void Hub()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Hub");
    }

    private void HandleInput()
    {
        bool inputReceived = false;
        float verticalAxis = Input.GetAxisRaw("Vertical");

        // Handling controller's vertical axis input with a dead zone and cooldown
        if (verticalAxis > deadZone && Time.time - lastAxisInputTime >= axisInputCooldown)
        {
            currentButtonIndex = (currentButtonIndex - 1 + menuButtons.Length) % menuButtons.Length;
            inputReceived = true;
            lastAxisInputTime = Time.time;
        }
        else if (verticalAxis < -deadZone && Time.time - lastAxisInputTime >= axisInputCooldown)
        {
            currentButtonIndex = (currentButtonIndex + 1) % menuButtons.Length;
            inputReceived = true;
            lastAxisInputTime = Time.time;
        }

        // Handling keyboard arrow inputs
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentButtonIndex = (currentButtonIndex - 1 + menuButtons.Length) % menuButtons.Length;
            inputReceived = true;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentButtonIndex = (currentButtonIndex + 1) % menuButtons.Length;
            inputReceived = true;
        }

        // Select the option with "O" key or "Fire3" button
        if (Input.GetKeyDown(KeyCode.O) || Input.GetButtonDown("Fire3"))
        {
            menuButtons[currentButtonIndex].onClick.Invoke();
        }

        if (inputReceived)
        {
            lastInputTime = Time.time;
            UpdateButtonSelection();
        }
    }

    private void UpdateButtonSelection()
    {
        for (int i = 0; i < menuButtons.Length; i++)
        {
            Text buttonText = menuButtons[i].GetComponentInChildren<Text>();
            buttonText.color = (i == currentButtonIndex) ? highlightedColor : normalColor;
        }
    }
}
