using UnityEngine;
using TMPro;
using System.Collections;

public class RTGameManager : MonoBehaviour
{
    public static RTGameManager Instance { get; private set; }

    public GameObject antivirusPrefab; // Drag  Antivirus GameObject prefab here
    public Transform spawnPoint; // Drag an empty GameObject as spawn point here
    public TextMeshProUGUI scoreText; // Drag TextMeshPro Text element here for score display
    public GameObject gameOverScreen; // Drag UI Panel GameObject for Game Over screen here
    public GameObject gameWonScreen; // Drag UI Panel GameObject for Game Won screen here

    private int score = 0;
    private int missCount = 0;
    private bool gameEnded = false;

    void Awake()
    {
        // Singleton pattern implementation
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Initialize UI and game state
        scoreText.text = "Score: " + score.ToString();
        gameOverScreen.SetActive(false);
        gameWonScreen.SetActive(false);

        // Start spawning antivirus after a delay (if needed)
        StartSpawningAntivirus();
    }

    void Update()
    {
        // Check for game end conditions
        if (!gameEnded)
        {
            if (score >= 10)
            {
                GameWon();
            }
            else if (missCount >= 5)
            {
                GameOver();
            }
        }
    }

    void StartSpawningAntivirus()
    {
        StartCoroutine(SpawnAntivirusRoutine());
    }

    IEnumerator SpawnAntivirusRoutine()
    {
        yield return new WaitForSeconds(2f); // Initial delay before spawning starts

        while (!gameEnded)
        {
            // Instantiate antivirus GameObject at spawn point
            GameObject antivirus = Instantiate(antivirusPrefab, spawnPoint.position, Quaternion.identity);

            yield return new WaitForSeconds(2f); // Delay between spawns

            // Check if antivirus still exists and hasn't been clicked
            if (antivirus != null && !antivirus.GetComponent<Antivirus>().IsClicked)
            {
                // Destroy antivirus if it hasn't been clicked
                Destroy(antivirus);
                missCount++; // Increment miss count
            }
        }
    }

    public void IncreaseScore()
    {
        if (!gameEnded)
        {
            // Increment score and update scoreText
            score++;
            scoreText.text = "Score: " + score.ToString();
        }
    }

    public void GameOver()
    {
        // Set game over state and activate game over screen
        gameEnded = true;
        gameOverScreen.SetActive(true);
    }

    public void GameWon()
    {
        // Set game won state and activate game won screen
        gameEnded = true;
        gameWonScreen.SetActive(true);
    }
}