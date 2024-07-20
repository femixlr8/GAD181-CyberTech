using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LightningReactionManager : MonoBehaviour
{
    public Canvas gameCanvas;
    public Button[] reflexButtons;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public float gameDuration = 15f;
    public float buttonLightDuration = 1f;

    private float timer;
    private int score;
    private bool isGameActive;
    private Button activeButton;

    void Start()
    {
        timer = gameDuration;
        score = 0;
        isGameActive = true; // Set to true to start the game immediately
        gameCanvas.gameObject.SetActive(isGameActive);

        foreach (Button btn in reflexButtons)
        {
            btn.onClick.AddListener(() => OnReflexButtonClick(btn));
        }

        UpdateScoreText();
        UpdateTimer();

        // Start the random button flash coroutine
        StartCoroutine(RandomButtonFlash());
    }

    void Update()
    {
        if (isGameActive)
        {
            timer -= Time.deltaTime;
            UpdateTimer();

            if (timer <= 0)
            {
                EndGame();
            }
        }
    }

    IEnumerator RandomButtonFlash()
    {
        while (isGameActive)
        {
            // Select a random button to light up
            int randomIndex = Random.Range(0, reflexButtons.Length);
            activeButton = reflexButtons[randomIndex];

            // Light up the button
            activeButton.GetComponent<Image>().color = Color.yellow;

            // Wait for the button light duration
            yield return new WaitForSeconds(buttonLightDuration);

            // Turn off the button light
            activeButton.GetComponent<Image>().color = Color.white;

            // Wait for a short random duration before lighting up the next button
            yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
        }
    }

    void OnReflexButtonClick(Button button)
    {
        if (isGameActive && button == activeButton)
        {
            score += 1;
            UpdateScoreText();
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }

    void UpdateTimer()
    {
        timerText.text = "Time: " + Mathf.Clamp(timer, 0, gameDuration).ToString("F2");
    }

    void EndGame()
    {
        isGameActive = false;
        timerText.text = "Game Over!";
        gameCanvas.gameObject.SetActive(isGameActive);

        foreach (Button btn in reflexButtons)
        {
            btn.interactable = false;
        }

        StopCoroutine(RandomButtonFlash());

        // Notify MicroGameManager to load the next scene
        MicroGameManager.Instance.LoadNextScene();
    }
}
