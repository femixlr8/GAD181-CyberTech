using UnityEngine;
using TMPro;

public class PUPGameManager : MonoBehaviour
{
    public GameObject popUpPrefab;
    public Transform canvasTransform;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;

    public GameObject gameOverPanel;
    public GameObject gameWonPanel;

    private int score;
    private float timer = 30f;

    private bool gameActive = true;

    void Start()
    {
        score = 0;
        UpdateScoreText();
        UpdateTimerText();
        InvokeRepeating("SpawnPopUp", 1f, 1f); // Spawns a pop-up every second
    }

    void Update()
    {
        if (gameActive)
        {
            timer -= Time.deltaTime;
            UpdateTimerText();

            if (timer <= 0)
            {
                EndGame(false); // Ends the game when the timer reaches zero
            }
        }
    }

    public void IncreaseScore()
    {
        if (gameActive)
        {
            score++;
            UpdateScoreText();

            if (score >= 15)
            {
                EndGame(true); // Ends the game when the player reaches 15 points
            }
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }

    void UpdateTimerText()
    {
        timerText.text = "Time: " + Mathf.Ceil(timer);
    }

    void EndGame(bool won)
    {
        gameActive = false;
        CancelInvoke("SpawnPopUp");

        if (won)
        {
            gameWonPanel.SetActive(true); // Display "You Won!" screen
        }
        else
        {
            gameOverPanel.SetActive(true); // Display "Game Over!" screen
        }
    }

    void SpawnPopUp()
    {
        if (gameActive)
        {
            GameObject newPopUp = Instantiate(popUpPrefab, canvasTransform);
            RectTransform rectTransform = newPopUp.GetComponent<RectTransform>();

            // Random position within the canvas
            rectTransform.anchoredPosition = new Vector2(
                Random.Range(-canvasTransform.GetComponent<RectTransform>().rect.width / 2 + rectTransform.rect.width / 2,
                             canvasTransform.GetComponent<RectTransform>().rect.width / 2 - rectTransform.rect.width / 2),
                Random.Range(-canvasTransform.GetComponent<RectTransform>().rect.height / 2 + rectTransform.rect.height / 2,
                             canvasTransform.GetComponent<RectTransform>().rect.height / 2 - rectTransform.rect.height / 2)
            );

            // Add the pop-up behavior script only if not already present
            if (newPopUp.GetComponent<PUPPopUp>() == null)
            {
                newPopUp.AddComponent<PUPPopUp>();
            }
        }
    }
}
