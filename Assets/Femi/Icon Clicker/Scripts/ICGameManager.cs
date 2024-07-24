using UnityEngine;
using TMPro;

public class ICGameManager : MonoBehaviour
{
    public int score = 0;
    public float timeLimit = 30f;
    private float timer;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public GameObject gameWonScreen;
    public GameObject gameOverScreen;

    private int remainingRedIcons;

    void Start()
    {
        timer = timeLimit;
        UpdateScoreText();
        UpdateTimerText();

        ICGridManager gridManager = FindFirstObjectByType<ICGridManager>();
        remainingRedIcons = gridManager.redIconCount;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        UpdateTimerText();

        if (timer <= 0)
        {
            GameOver();
        }
    }

    public void AddScore()
    {
        score++;
        UpdateScoreText();
    }

    public void CollectRedIcon()
    {
        remainingRedIcons--;
        CheckWinCondition();
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }

    void UpdateTimerText()
    {
        timerText.text = "Time: " + Mathf.Round(timer);
    }

    void CheckWinCondition()
    {
        if (remainingRedIcons <= 0)
        {
            GameWon();
        }
    }

    void GameWon()
    {
        gameWonScreen.SetActive(true);

        // Notify MicroGameManager to load the next scene
        MicroGameManager.Instance.LoadNextScene();
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);

        // Notify MicroGameManager to load the next scene
        MicroGameManager.Instance.LoadNextScene();
    }
}
