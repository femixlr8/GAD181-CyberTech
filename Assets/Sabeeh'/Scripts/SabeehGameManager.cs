using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SabeehGameManager : MonoBehaviour
{
    public int currentScore;
    public int maxScore = 7;
    public TextMeshProUGUI scoreValue;
    public Player player;
    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverScoreValue;
    public float currentTime;
    public float initTime = 30;
    public TextMeshProUGUI gameOverText;
    public Image gameOverBackground;
    public TextMeshProUGUI timerValue;
    public GameController gameController;

    private bool gameEnded = false; // Flag to prevent multiple scene loads

    // Start is called before the first frame update
    void Start()
    {
        currentTime = initTime;
    }

    // Update is called once per frame
    void Update()
    {
        scoreValue.text = currentScore.ToString();
        gameOverScoreValue.text = currentScore.ToString();
        GameOver();
        Timer();
    }

    void GameOver()
    {
        if (!gameEnded && (player.health <= 0 || currentTime <= 0))
        {
            gameEnded = true; // Prevent multiple calls

            gameOverPanel.SetActive(true);
            gameOverBackground.color = (currentTime <= 0) ? Color.green : Color.red;
            gameOverText.text = (currentTime <= 0) ? "You Win!" : "Game Over";

            

            if (gameController != null)
            {
                gameController.GameOver(currentTime <= 0 ? 1 : 0); // Inform GameController about game over type
            }
        }
    }

    public void Timer()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
        }
        else
        {
            currentTime = 0;
            GameOver(); // Check for game over condition when time runs out
        }

        int timerINT = (int)currentTime;
        timerValue.text = timerINT.ToString();
    }

    public void AddScore()
    {
        currentScore++;
    }
}
