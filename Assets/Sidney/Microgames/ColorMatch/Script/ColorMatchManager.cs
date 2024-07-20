using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ColorMatchManager : MonoBehaviour
{
    public Canvas gameCanvas;
    public bool isGameActive;
    public bool hasPlayedBefore;

    public TextMeshProUGUI targetColorText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public ColorButtons redButton;
    public ColorButtons yellowButton;
    public ColorButtons greenButton;
    public ColorButtons blueButton;

    private int score = 0;
    private float timeLeft = 30f;
    private string[] colorNames = { "Red", "Yellow", "Green", "Blue" };
    private string currentTargetColor;
    private Coroutine timerCoroutine;

    void Start()
    {
        isGameActive = true; // Set to true to start the game immediately

        if (isGameActive)
        {
            StartNewGame();
            gameCanvas.gameObject.SetActive(isGameActive);
        }
        else
        {
            gameCanvas.gameObject.SetActive(isGameActive);
        }
    }

    void StartNewGame()
    {
        score = 0;
        timeLeft = 30f;
        StartNewRound();
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
        }
        timerCoroutine = StartCoroutine(GameTimer());
    }

    void StartNewRound()
    {
        // Choose a random color name for the target
        currentTargetColor = colorNames[Random.Range(0, colorNames.Length)];
        targetColorText.text = currentTargetColor;

        // Update UI and reset buttons interactability
        scoreText.text = "Score: " + score.ToString();
        ResetButtonInteractability(true);
    }

    void ResetButtonInteractability(bool interactable)
    {
        redButton.SetInteractable(interactable);
        yellowButton.SetInteractable(interactable);
        greenButton.SetInteractable(interactable);
        blueButton.SetInteractable(interactable);
    }

    public void OnButtonClicked(string buttonColor)
    {
        if (buttonColor.ToLower() == currentTargetColor.ToLower())
        {
            score++;
            Debug.Log("Correct! Score: " + score);
        }
        else
        {
            score--;
            Debug.Log("Incorrect! Score: " + score);
        }

        StartNewRound();
    }

    IEnumerator GameTimer()
    {
        while (timeLeft > 0)
        {
            timerText.text = "Time: " + Mathf.Round(timeLeft).ToString();
            yield return new WaitForSeconds(1f);
            timeLeft--;
        }

        // Disable all buttons
        redButton.SetInteractable(false);
        yellowButton.SetInteractable(false);
        greenButton.SetInteractable(false);
        blueButton.SetInteractable(false);

        // Display Game Over in the timer text field
        timerText.text = "Game Over";

        // Game over logic (e.g., show game over screen, reset game)
        Debug.Log("Game Over! Final Score: " + score);

        isGameActive = false;
        gameCanvas.gameObject.SetActive(isGameActive);

        // Notify MicroGameManager to load the next scene
        MicroGameManager.Instance.LoadNextScene();
    }
}
