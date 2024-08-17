using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PuzzleTapManager : MonoBehaviour
{
    public List<Button> puzzleButtons = new List<Button>();  // List of all puzzle buttons
    public TextMeshProUGUI timerText;                        // Timer text reference
    public TextMeshProUGUI sequenceText;                     // Text field to display the sequence
    public TextMeshProUGUI textPutIn;                        // Text field to display the button presses

    private List<int> buttonSequence = new List<int>();      // To store the randomized sequence
    private int currentStep = 0;                             // Current step in the puzzle sequence
    private float timer = 15f;                               // Countdown timer (set duration as needed)
    private bool gameActive = true;

    void Start()
    {
        // Setup buttons and generate sequence
        SetupButtons();
        GenerateCode();
        DisplayButtonSequence();

        // Assign listeners to each button
        foreach (var button in puzzleButtons)
        {
            TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
            if (int.TryParse(buttonText.text, out int number))
            {
                button.onClick.AddListener(() => CheckOrder(number));
            }
            else
            {
                Debug.LogError($"Button text is not a valid number: {buttonText.text}");
            }
        }
    }

    void Update()
    {
        if (gameActive)
        {
            timer -= Time.deltaTime;
            timerText.text = "Time: " + Mathf.Round(timer).ToString();

            if (timer <= 0)
            {
                gameActive = false;
                GameOver(false);
            }
        }
    }

    void SetupButtons()
    {
        // Ensure that all buttons are labeled from 1 to 9
        for (int i = 0; i < puzzleButtons.Count; i++)
        {
            TextMeshProUGUI buttonText = puzzleButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText != null)
            {
                buttonText.text = (i + 1).ToString();
            }
        }
    }

    void GenerateCode()
    {
        // Generate a random code (sequence) up to 9 numbers long
        buttonSequence.Clear();
        int codeLength = Random.Range(4, 10); // Length of code between 1 and 9
        for (int i = 0; i < codeLength; i++)
        {
            buttonSequence.Add(Random.Range(4, 10)); // Numbers between 1 and 9
        }
    }

    void DisplayButtonSequence()
    {
        if (sequenceText != null)
        {
            string sequence = "Code: ";
            foreach (var number in buttonSequence)
            {
                sequence += number + " ";
            }
            sequenceText.text = sequence; // Display the sequence on the text field
        }
        else
        {
            Debug.LogError("SequenceText is not assigned.");
        }
        Debug.Log("Code Sequence: " + string.Join(" ", buttonSequence)); // Log the sequence for verification
    }

    void CheckOrder(int buttonNumber)
    {
        if (!gameActive) return;

        // Append the pressed number to the textPutIn field
        if (textPutIn != null)
        {
            textPutIn.text += buttonNumber + " ";
        }
        else
        {
            Debug.LogError("TextPutIn is not assigned.");
        }

        if (buttonNumber == buttonSequence[currentStep])
        {
            currentStep++;

            // If the whole sequence is completed
            if (currentStep >= buttonSequence.Count)
            {
                gameActive = false;
                GameOver(true);
            }
        }
        else
        {
            gameActive = false;
            GameOver(false);
        }
    }

    void GameOver(bool won)
    {
        if (won)
        {
            Debug.Log("You won!");
            MicroGameManager.Instance.IncreaseScore();
        }
        else
        {
            Debug.Log("You lost!");
        }

        // Disable all buttons when the game is over
        foreach (var button in puzzleButtons)
        {
            button.interactable = false;
        }

        // Notify MicroGameManager to load the next scene
        MicroGameManager.Instance.LoadNextScene();
    }
}
