using UnityEngine;

public class MailManager : MonoBehaviour
{
    public GameObject normalMailPrefab;
    public GameObject redMailPrefab;
    public Transform spawnPoint;
    public AudioSource wrongDropSound;
    public GameObject gameWonScreen;
    public GameObject gameOverScreen;
    public float spawnInterval = 1.5f;
    public bool IsGameOver { get { return isGameOver; } }

    private int normalMailCounter = 0;
    private int totalMailCounter = 0;
    private int wrongTurns = 0;
    private bool isGameOver = false;

    private GameObject draggingMail; // Reference to currently dragged mail object

    void Start()
    {
        SpawnMail();
    }

    public void SetDraggingMail(GameObject mail)
    {
        draggingMail = mail;
    }

    public void SortMail(bool isCorrect, GameObject mail)
    {
        if (!isGameOver)
        {
            if (isCorrect)
            {
                if (mail.CompareTag("Mail"))
                {
                    IncrementNormalMailCounter();
                }
                IncrementTotalMailCounter();
            }
            else
            {
                WrongDrop();
            }

            Destroy(mail); // Destroy the sorted mail object
            draggingMail = null; // Reset draggingMail reference
            Invoke("SpawnMail", 1.5f); // Spawn the next mail after 1.5 seconds
        }
    }

    public void WrongDrop()
    {
        wrongTurns++;
        wrongDropSound.Play();
        Debug.Log($"Wrong Turns: {wrongTurns}");

        if (wrongTurns >= 3)
        {
            GameOver();
        }
    }

    public void IncrementNormalMailCounter()
    {
        normalMailCounter++;
        totalMailCounter++;
        Debug.Log($"Normal Mail Collected: {normalMailCounter}, Total Mail Collected: {totalMailCounter}");

        if (normalMailCounter >= 15)
        {
            WinGame();
        }
    }

    public void IncrementTotalMailCounter()
    {
        totalMailCounter++;
        Debug.Log($"Total Mail Collected: {totalMailCounter}");
    }

    void GameOver()
    {
        isGameOver = true;
        gameOverScreen.SetActive(true);
    }

    void WinGame()
    {
        isGameOver = true;
        gameWonScreen.SetActive(true);
    }

    void SpawnMail()
    {
        if (!isGameOver)
        {
            GameObject mailPrefab = Random.Range(0, 2) == 0 ? normalMailPrefab : redMailPrefab;
            Instantiate(mailPrefab, spawnPoint.position, Quaternion.identity);
        }
    }
}
