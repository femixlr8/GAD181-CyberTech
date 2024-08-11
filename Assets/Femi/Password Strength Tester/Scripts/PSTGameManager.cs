using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PSTGameManager : MonoBehaviour
{
    public Text timerText;
    public Text scoreText;
    public GameObject gameOverPanel;
    public GameObject gameWonPanel;
    public PSTButton[] buttons;

    private float timer = 15f;
    private int score = 0;

    void Start()
    {
        UpdateTimer();
        UpdateScore();
        GeneratePasswords();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        UpdateTimer();

        if (timer <= 0)
        {
            GameOver();
        }
    }

    void UpdateTimer()
    {
        timerText.text = "Time: " + Mathf.Max(timer, 0).ToString("F1");
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public void CorrectPassword()
    {
        score++;
        UpdateScore();

        if (score == buttons.Length)
        {
            GameWon();
        }
    }

    public void WrongPassword()
    {
        GameOver();
    }

    void GameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0; // Pause the game
    }

    void GameWon()
    {
        gameWonPanel.SetActive(true);
        Time.timeScale = 0; // Pause the game
    }

    void GeneratePasswords()
    {
        foreach (var button in buttons)
        {
            button.GeneratePassword();
        }
    }
}
