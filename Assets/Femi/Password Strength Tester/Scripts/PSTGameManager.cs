using UnityEngine;
using TMPro;

public class PSTGameManager : MonoBehaviour
{
    public TMP_Text timerText;
    public TMP_Text scoreText;
    public GameObject gameOverPanel;
    public GameObject gameWonPanel;
    public PSTButton[] buttons;

    private float timer = 15f;
    private int score = 0;

    void Start()
    {
        Time.timeScale = 1; // Ensure game is running
        UpdateTimer();
        UpdateScore();
        GeneratePasswords();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        UpdateTimer();

        if (timer <= 0 && !gameOverPanel.activeSelf && !gameWonPanel.activeSelf) // Check for game over only if the panels are not already active
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
        Debug.Log("Game Over");
        gameOverPanel.SetActive(true);
        Time.timeScale = 0; // Pause the game
    }

    void GameWon()
    {
        Debug.Log("Game Won");
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
