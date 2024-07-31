using System.Collections;
using UnityEngine;
using TMPro;

public class SCCGameManager : MonoBehaviour
{
    public TextMeshProUGUI codeDisplay;    // Text to display the code
    public TMP_InputField codeInputField;  // Input field for player input
    public TextMeshProUGUI timerText;      // Text to display the countdown timer
    public GameObject gameOverPanel;       // Game over panel
    public GameObject gameWonPanel;        // Game won panel
    public AudioSource correctAudioSource; // Audio source for correct input
    public AudioSource incorrectAudioSource; // Audio source for incorrect input

    private string securityCode;           // Randomly generated security code
    private float timer = 10f;             // Timer duration
    private bool gameActive = true;        // Game state

    private void Start()
    {
        GenerateSecurityCode();
        UpdateCodeDisplay();
        gameOverPanel.SetActive(false);
        gameWonPanel.SetActive(false);
    }

    private void Update()
    {
        if (gameActive)
        {
            UpdateTimer();
            CheckInput();
        }
    }

    private void GenerateSecurityCode()
    {
        securityCode = "";
        for (int i = 0; i < 5; i++)
        {
            securityCode += Random.Range(0, 10).ToString();
        }
    }

    private void UpdateCodeDisplay()
    {
        codeDisplay.text = securityCode;
    }

    private void UpdateTimer()
    {
        timer -= Time.deltaTime;
        timerText.text = "Time: " + Mathf.Ceil(timer).ToString();

        if (timer <= 0)
        {
            GameOver();
        }
    }

    private void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (codeInputField.text == securityCode)
            {
                PlayCorrectSound();
                GameWon();
            }
            else
            {
                PlayIncorrectSound();
                GameOver();
            }
        }
    }

    private void PlayCorrectSound()
    {
        if (correctAudioSource != null)
        {
            correctAudioSource.Play();
        }
    }

    private void PlayIncorrectSound()
    {
        if (incorrectAudioSource != null)
        {
            incorrectAudioSource.Play();
        }
    }

    private void GameOver()
    {
        gameActive = false;
        gameOverPanel.SetActive(true);
    }

    private void GameWon()
    {
        gameActive = false;
        gameWonPanel.SetActive(true);
    }
}
