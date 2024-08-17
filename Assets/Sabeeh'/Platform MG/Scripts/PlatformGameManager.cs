using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class PlatfromGameManager : MonoBehaviour
{
    public int currentScore;
    public int maxScore = 7;
    public TextMeshProUGUI scoreValue;
    public PlayerStatus player;
    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverScoreValue;
    public float currentTime;
    public float initTime = 30;
    public TextMeshProUGUI gameOverText;
    public Image gameOverBackground;
    public TextMeshProUGUI timerValue;
    private AudioSource audioSource;
    [SerializeField] AudioClip audioClip;
    bool victoryMusicPlayed;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = initTime;
        audioSource = FindObjectOfType<AudioSource>();
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
        if (player.health <= 0)
        {
            gameOverPanel.SetActive(true);
            gameOverBackground.color = Color.red;
            gameOverText.text = "Game Over";

            MicroGameManager.Instance.LoadNextScene();
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
            gameOverPanel.SetActive(true);
            gameOverBackground.color = Color.green;
            gameOverText.text = "You Win!";
            MicroGameManager.Instance.IncreaseScore();
            if (victoryMusicPlayed == false)
            {
                victoryMusicPlayed = true;
                audioSource.PlayOneShot(audioClip, 0.1f);
            }

            MicroGameManager.Instance.LoadNextScene();

        }
        int timerINT = (int)currentTime;

        timerValue.text = timerINT.ToString();
    }
    public void AddScore()
    {
        currentScore++;
    }
}
