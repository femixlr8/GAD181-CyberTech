using UnityEngine;
using TMPro;

public class VSGameManager : MonoBehaviour
{
    public static VSGameManager instance;

    public GameObject virusPrefab;
    public Transform spawnPoint;
    public float spawnInterval = 1.5f;
    public int scoreToWin = 10;
    public int maxViruses = 45;
    public TextMeshProUGUI scoreText;
    public GameObject gameOverScreen;
    public GameObject gameWonScreen;

    private int currentScore = 0;
    private int virusCount = 0;
    private int virusesDestroyed = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        gameOverScreen.SetActive(false); // Hide game over screen
        gameWonScreen.SetActive(false); // Hide game won screen
        InvokeRepeating(nameof(SpawnVirus), 1f, spawnInterval);
        UpdateScoreText();
    }

    void SpawnVirus()
    {
        if (virusCount < maxViruses)
        {
            Instantiate(virusPrefab, spawnPoint.position, Quaternion.identity);
            virusCount++;
        }
        else
        {
            CancelInvoke(nameof(SpawnVirus));
            CheckGameOver();
        }
    }

    public void AddScore(int amount)
    {
        currentScore += amount;
        virusesDestroyed++;
        Debug.Log("Score: " + currentScore);
        UpdateScoreText();
        if (currentScore >= scoreToWin)
        {
            GameWon();
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + currentScore;
    }

    void GameWon()
    {
        CancelInvoke(nameof(SpawnVirus));
        gameWonScreen.SetActive(true);
    }

    public void CheckGameOver()
    {
        if (virusesDestroyed + virusCount >= maxViruses && currentScore < scoreToWin)
        {
            gameOverScreen.SetActive(true);
        }
    }
}
