using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BCGameManager : MonoBehaviour
{
    // UI Elements
    public TextMeshProUGUI timerText;      // Displays the countdown timer
    public TextMeshProUGUI binaryCodeText; // Displays the current binary code
    public GameObject gameWonPanel;        // UI panel shown when the player wins
    public GameObject gameOverPanel;       // UI panel shown when the player loses

    // Buttons
    public Button button1;                 // Reference to the "1" button
    public Button button0;                 // Reference to the "0" button

    // Audio
    public AudioSource audioSource;        // Audio source to play sounds
    public AudioClip buttonClickSound;     // Sound played on button click
    public AudioClip correctSound;         // Sound played when the player wins
    public AudioClip incorrectSound;       // Sound played when the player loses

    // Game State
    private string currentBinaryCode;      // The generated binary code to be guessed
    private string playerInput;            // Player's input
    private float timeRemaining = 20f;     // Countdown timer value
    private bool gameActive;               // Flag to track if the game is active

    void Start()
    {
        StartGame(); // Initialize the game when the scene starts
    }

    void Update()
    {
        if (gameActive)
        {
            // Decrease the time remaining and update the timer text
            timeRemaining -= Time.deltaTime;
            UpdateTimer();

            // Check if the time has run out
            if (timeRemaining <= 0)
            {
                GameOver(); // Trigger game over if time runs out
            }
        }
    }

    void StartGame()
    {
        // Generate a random binary code between 6 and 8 digits
        int codeLength = Random.Range(6, 9);
        currentBinaryCode = GenerateBinaryCode(codeLength);
        binaryCodeText.text = currentBinaryCode; // Display the binary code

        // Reset player input and game state
        playerInput = "";
        timeRemaining = 20f;
        gameActive = true;

        // Hide game won and game over panels
        gameWonPanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    string GenerateBinaryCode(int length)
    {
        // Create a binary string of the specified length
        string code = "";
        for (int i = 0; i < length; i++)
        {
            code += Random.Range(0, 2).ToString(); // Append a random 0 or 1
        }
        return code;
    }

    void UpdateTimer()
    {
        // Update the timer text with the remaining time
        timerText.text = "Time: " + Mathf.Ceil(timeRemaining).ToString();
    }

    public void OnButtonClick(int number)
    {
        if (!gameActive) return; // Ignore clicks if the game is not active

        // Play the button click sound
        audioSource.PlayOneShot(buttonClickSound);
        playerInput += number.ToString(); // Append the clicked number to the player's input

        // Check if the player's input matches the length of the binary code
        if (playerInput.Length == currentBinaryCode.Length)
        {
            CheckPlayerInput(); // Verify the player's input
        }
    }

    void CheckPlayerInput()
    {
        // Compare the player's input with the binary code
        if (playerInput == currentBinaryCode)
        {
            GameWon(); // Player wins if the input matches
        }
        else
        {
            GameOver(); // Player loses if the input does not match
        }
    }

    void GameWon()
    {
        // Handle game won condition
        gameActive = false; // Stop the game
        audioSource.PlayOneShot(correctSound); // Play the correct sound
        gameWonPanel.SetActive(true); // Show the game won panel
    }

    void GameOver()
    {
        // Handle game over condition
        gameActive = false; // Stop the game
        audioSource.PlayOneShot(incorrectSound); // Play the incorrect sound
        gameOverPanel.SetActive(true); // Show the game over panel
    }
}
