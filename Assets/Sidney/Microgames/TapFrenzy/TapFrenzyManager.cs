using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TapFrenzyManager : MonoBehaviour
{
    public List<Button> buttonPool; // List of all buttons
    public float displayDuration = 2f; // Time each button stays visible
    public float buttonInterval = 0.5f; // Interval between button appearances
    public TextMeshProUGUI timerText;  // Timer text reference
    public TextMeshProUGUI scoreText;  // Score text reference
    public float gameDuration = 15f;   // Total game time

    private int currentIndex = 0;
    private bool gameActive = true;
    private float timer;
    private int score = 0;

    void Start()
    {
        timer = gameDuration;
        UpdateScoreText();
        UpdateTimerText();

        // Set up button click listeners
        foreach (Button button in buttonPool)
        {
            button.onClick.AddListener(() => OnButtonClick(button));
        }

        StartCoroutine(ButtonCycle());
    }

    void Update()
    {
        if (gameActive)
        {
            timer -= Time.deltaTime;
            UpdateTimerText();

            if (timer <= 0)
            {
                StopGame();
            }
        }
    }

    IEnumerator ButtonCycle()
    {
        while (gameActive)
        {
            // Deactivate all buttons
            foreach (Button button in buttonPool)
            {
                SetButtonVisibility(button, false);
            }

            // Activate the next button in the pool
            SetButtonVisibility(buttonPool[currentIndex], true);

            // Wait for the display duration
            yield return new WaitForSeconds(displayDuration);

            // Deactivate the current button
            SetButtonVisibility(buttonPool[currentIndex], false);

            // Move to the next button
            currentIndex = (currentIndex + 1) % buttonPool.Count;

            // Optional: Wait for a short interval before the next button appears
            yield return new WaitForSeconds(buttonInterval);
        }
    }

    void SetButtonVisibility(Button button, bool isVisible)
    {
        button.gameObject.SetActive(isVisible);
        var image = button.GetComponent<Image>();
        var text = button.GetComponentInChildren<TextMeshProUGUI>();

        if (image != null)
        {
            image.enabled = isVisible;
        }
        if (text != null)
        {
            text.enabled = isVisible;
        }
    }

    public void OnButtonClick(Button clickedButton)
    {
        if (gameActive)
        {
            // Increase score and update text
            score++;
            UpdateScoreText();
            Debug.Log("Button clicked: " + clickedButton.name + " - Score: " + score);
        }
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    void UpdateTimerText()
    {
        if (timerText != null)
        {
            timerText.text = "Time: " + Mathf.Round(timer);
        }
    }

    void StopGame()
    {
        gameActive = false;
        // Optionally, disable all buttons
        foreach (Button button in buttonPool)
        {
            SetButtonVisibility(button, false);
        }

        // Check win condition
        if (score >= 20)
        {
            Debug.Log("You win!");
        }
        else
        {
            Debug.Log("You lose!");
        }

        // Notify MicroGameManager to load the next scene
        MicroGameManager.Instance.LoadNextScene();
    }
}
