using UnityEngine;
using TMPro;
using System.Text;

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

        if (score == buttons.Length - 1) // Only win when all correct buttons are pressed
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
        int weakPasswordIndex = Random.Range(0, buttons.Length); // Pick a random index for the weak password

        for (int i = 0; i < buttons.Length; i++)
        {
            if (i == weakPasswordIndex)
            {
                buttons[i].password = GenerateWeakPassword(8);
            }
            else
            {
                buttons[i].GeneratePassword();
            }
            buttons[i].GetComponentInChildren<TMP_Text>().text = buttons[i].password;
        }
    }

    private string GenerateWeakPassword(int length)
    {
        const string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string lower = "abcdefghijklmnopqrstuvwxyz";
        const string digits = "0123456789";
        const string special = "!@#$%^&*()_+-=[]{}|;:,.<>?";

        System.Random random = new System.Random();
        StringBuilder passwordBuilder = new StringBuilder();

        // Randomly decide which character type to omit
        int omitType = random.Next(0, 4); // 0 = uppercase, 1 = lowercase, 2 = digits, 3 = special

        // Fill the password with characters, omitting one type
        for (int i = 0; i < length; i++)
        {
            if (omitType != 0) passwordBuilder.Append(upper[random.Next(upper.Length)]);
            if (omitType != 1) passwordBuilder.Append(lower[random.Next(lower.Length)]);
            if (omitType != 2) passwordBuilder.Append(digits[random.Next(digits.Length)]);
            if (omitType != 3) passwordBuilder.Append(special[random.Next(special.Length)]);
        }

        // Truncate to the required length
        return passwordBuilder.ToString().Substring(0, length);
    }
}
