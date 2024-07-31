using CyberTech.Dodge;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinCollectorGameManager : MonoBehaviour
{
    public int currentScore;
    public TextMeshProUGUI scoreValue;
    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverScoreValue;
    public float currentTime;
    public float initTime = 30;
    public TextMeshProUGUI gameOverText;
    public Image gameOverBackground;
    public TextMeshProUGUI timerValue;
    public AudioClip audioClip;
    AudioSource audioSource;
    bool victoryMusicPlayed;
    [SerializeField] int numberOfCoinsToCollect;

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
        Timer();
        CheckCoins();
    }

    

    public void Timer()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
        }
        else if (currentScore <= 0)
        {
            currentTime = 0;
            gameOverPanel.SetActive(true);
            gameOverBackground.color = Color.red;
            gameOverText.text = "You Lose!";
            Time.timeScale = 0;
        }
        else if (currentScore > 0)
        {
            currentTime = 0;
            gameOverPanel.SetActive(true);
            gameOverBackground.color = Color.green;
            gameOverText.text = "You Win!";
            Time.timeScale = 0;
            if (victoryMusicPlayed == false)
            {
                victoryMusicPlayed = true;
                audioSource.PlayOneShot(audioClip, 0.1f);
            }
        }
        int timerINT = (int)currentTime;

        timerValue.text = timerINT.ToString();
    }
    public void AddScore()
    {
        currentScore++;
    }
    void CheckCoins()
    {
        if (currentScore >= numberOfCoinsToCollect)
        {
            gameOverPanel.SetActive(true);
            gameOverBackground.color = Color.green;
            gameOverText.text = "You Win!";
            Time.timeScale = 0;
            if (victoryMusicPlayed == false)
            {
                victoryMusicPlayed = true;
                audioSource.PlayOneShot(audioClip, 0.1f);
            }
        }
    }
}

