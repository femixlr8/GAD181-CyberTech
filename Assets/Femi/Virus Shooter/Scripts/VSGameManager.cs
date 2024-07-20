using UnityEngine;
using TMPro;

public class VSGameManager : MonoBehaviour
{
    public static VSGameManager instance;

    public GameObject virusPrefab;
    public Transform spawnPoint;
    public float spawnInterval = 2f;
    public int scoreToWin = 10;
    public int maxViruses = 25;
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
        if (spawnPoint != null)
        {
            InvokeRepeating(nameof(SpawnVirus), 1f, spawnInterval);
        }
        else
        {
            Debug.LogError("Spawn point is missing.");
        }
        UpdateScoreText();
    }

    void SpawnVirus()
    {
        if (virusCount < maxViruses)
        {
            if (spawnPoint != null)
            {
                Instantiate(virusPrefab, spawnPoint.position, Quaternion.identity);
                virusCount++;
                Debug.Log("Virus spawned. Total count: " + virusCount); // Debug log
            }
            else
            {
                CancelInvoke(nameof(SpawnVirus));
                Debug.LogError("Spawn point is missing.");
            }
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
        virusCount--; // Reduce virus count when one is destroyed
        Debug.Log("Score: " + currentScore + ", Viruses Destroyed: " + virusesDestroyed); // Debug log
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

        // Notify MicroGameManager to load the next scene
        MicroGameManager.Instance.LoadNextScene();
    }

    public void CheckGameOver()
    {
        if (virusesDestroyed + virusCount >= maxViruses && currentScore < scoreToWin)
        {
            CancelInvoke(nameof(SpawnVirus));
            gameOverScreen.SetActive(true);

            // Notify MicroGameManager to load the next scene
            MicroGameManager.Instance.LoadNextScene();
        }
    }
}
