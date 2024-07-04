using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FemiGameManager : MonoBehaviour
{
    [SerializeField] private List<Virus> viruses;

    [Header("UI Objects")]
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject outOfTimeText;
    [SerializeField] private GameObject bombText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI scoreText;

    // Hardcoded variables 
    private float startingTime = 30f;

    // Global variables
    private float timeRemaining;
    private HashSet<Virus> currentVirus = new HashSet<Virus>();
    private int score;
    private bool playing = false;

    // Event for game over
    public delegate void OnGameEnd();
    public static event OnGameEnd GameEnded;

    public void StartGame()
    {
        Debug.Log("Game Started");
        // Hide/show the UI elements to show or not show
        outOfTimeText.SetActive(false);
        bombText.SetActive(false);
        gameUI.SetActive(true);

        // Hide all the visible viruses
        foreach (Virus virus in viruses)
        {
            virus.Hide();
            virus.SetIndex(viruses.IndexOf(virus));
        }

        // Remove any old game state
        currentVirus.Clear();

        // Start with 30 seconds
        timeRemaining = startingTime;
        UpdateTimeText(); // Update time text initially
        score = 0;
        UpdateScoreText(); // Update score text initially
        playing = true;
    }

    public void GameOver(int type)
    {
        // Show the message
        if (type == 0)
        {
            outOfTimeText.SetActive(true);
            Debug.Log("Out of time!");
        }
        else
        {
            bombText.SetActive(true);
            Debug.Log("Bomb exploded!");
        }

        // Hide all the viruses
        foreach (Virus virus in viruses)
        {
            virus.StopGame();
        }

        // Stop the game
        playing = false;
        Debug.Log("Game Over");

        // Trigger event that the game has ended
        GameEnded?.Invoke();
    }

    void Update()
    {
        if (playing)
        {
            // Update time
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0)
            {
                timeRemaining = 0;
                GameOver(0);
            }

            UpdateTimeText(); // Update time text every frame

            // Check if we need to add any more viruses 
            if (currentVirus.Count <= (score / 10))
            {
                // Choose a random virus
                int index = Random.Range(0, viruses.Count);
                // Doesn't matter if it's already doing something, just try again next frame
                if (!currentVirus.Contains(viruses[index]))
                {
                    currentVirus.Add(viruses[index]);
                    viruses[index].Activate(score / 10);
                }
            }
        }
    }

    void UpdateTimeText()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timeText.text = string.Format("{0}:{1:D2}", minutes, seconds);

        Debug.Log("Time text Updated: " + timeText.text);
        Debug.Log("Minutes: " + minutes + " Seconds: " + seconds);
    }

    void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }

    public void AddScore(int virusIndex)
    {
        // Add and update score
        score += 1;
        UpdateScoreText();

        // Increase time by a little bit
        timeRemaining += 1;

        // Remove from active viruses
        currentVirus.Remove(viruses[virusIndex]);

        Debug.Log("Score increased. New score: " + score);
    }

    public void Missed(int virusIndex, bool isVirus)
    {
        if (isVirus)
        {
            // Decrease time by a little bit
            timeRemaining -= 2;
            UpdateTimeText(); // Update time text when time decreases
        }

        // Remove from active viruses
        currentVirus.Remove(viruses[virusIndex]);

        Debug.Log("Missed virus. Current time remaining: " + timeRemaining);
    }
}
