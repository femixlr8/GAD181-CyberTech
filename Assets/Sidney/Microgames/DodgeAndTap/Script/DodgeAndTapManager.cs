using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DodgeAndTapManager : MonoBehaviour
{
    public Canvas gameCanvas;
    public Button[] safeButtons;
    public Button[] dangerButtons;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public float gameDuration = 15f;

    private float timer;
    private int score;
    private bool isGameActive;
    public bool hasPlayedBefore;

    // Start is called before the first frame update
    void Start()
    {
        timer = gameDuration;
        score = 0;
        isGameActive = true;
        gameCanvas.gameObject.SetActive(isGameActive);

        foreach (Button btn in safeButtons)
        {
           btn.onClick.AddListener(() => OnSafeButtonClick(btn));
        }


        foreach (Button btn in dangerButtons)
        {
            btn.onClick.AddListener(() => OnDangerButtonClick(btn));
        }

        UpdateScoreText();
        UpdateTimer();
        
    }

    // Update is called once per frame
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

    void OnSafeButtonClick(Button button)
    {
        if (isGameActive)
        {
            score += 10;
            UpdateScoreText();
            button.interactable = false;
        }

    }

    void OnDangerButtonClick(Button button)
    {
        if (isGameActive)
        {
            score -= 20;
            UpdateScoreText();
            button.interactable = false;
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

        foreach (Button btn in safeButtons)
        {
            btn.interactable = false;
        }


        foreach (Button btn in dangerButtons)
        {
            btn.interactable = false;
        }

    }
}
