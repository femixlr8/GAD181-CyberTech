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
        InvokeRepeating("SpawnMail", 0f, spawnInterval);
    }

    void SpawnMail()
    {
        if (isGameOver || draggingMail != null)
        {
            return; // Wait until current mail is sorted or discarded
        }

        GameObject mailPrefab = Random.value > 0.5f ? normalMailPrefab : redMailPrefab;
        GameObject mail = Instantiate(mailPrefab, spawnPoint.position, Quaternion.identity);
        mail.GetComponent<Mail>().Initialize(this);

        // Set draggingMail to the newly spawned mail object
        draggingMail = mail;
    }

    public void SetDraggingMail(GameObject mail)
    {
        draggingMail = mail;
    }

    public void SortMail(bool isNormalMail)
    {
        if (draggingMail != null)
        {
            if (isNormalMail)
            {
                IncrementNormalMailCounter();
            }
            IncrementTotalMailCounter();

            Destroy(draggingMail); // Destroy the sorted mail object
            draggingMail = null; // Reset draggingMail reference
        }
    }

    public void WrongDrop()
    {
        if (draggingMail != null)
        {
            wrongTurns++;
            wrongDropSound.Play();
            Debug.Log($"Wrong Turns: {wrongTurns}");

            if (wrongTurns >= 3)
            {
                GameOver();
            }
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
        CancelInvoke("SpawnMail");
    }

    void WinGame()
    {
        isGameOver = true;
        gameWonScreen.SetActive(true);
        CancelInvoke("SpawnMail");
    }
}
