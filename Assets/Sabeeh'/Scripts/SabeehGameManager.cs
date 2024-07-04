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
        if (player.health <= 0 || currentTime <= 0)
        {
           gameOverPanel.SetActive(true);
            Time.timeScale = 0;
            gameOverBackground.color = Color.red;
            gameOverText.text = "Game Over";

            if (gameController != null)
            {
                gameController.MoveToNextScene();
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
            gameOverPanel.SetActive(true );
            gameOverBackground.color = Color.green;
            gameOverText.text = "You Win!";
            Time.timeScale = 0;
        }
        int timerINT = (int)currentTime;

        timerValue.text = timerINT.ToString();
    }
    public void AddScore ()
    {
        currentScore++;
    }
}
