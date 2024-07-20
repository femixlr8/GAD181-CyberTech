using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class MashManiaManager : MonoBehaviour
{
    public Canvas gameCanvas;
    public Button mashButton;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public float gameDuration = 15f;

    private float timer;
    private int score;
    private bool isGameActive;

    void Start()
    {
        timer = gameDuration;
        score = 0;
        isGameActive = true; // Set to true to start the game immediately
        gameCanvas.gameObject.SetActive(isGameActive);

        mashButton.onClick.AddListener(OnMashButtonClick);

        UpdateScoreText();
        UpdateTimer();
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

    void OnMashButtonClick()
    {
        if (isGameActive)
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

        mashButton.interactable = false;

        // Notify MicroGameManager to load the next scene
        MicroGameManager.Instance.LoadNextScene();
    }
}
