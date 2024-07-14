using UnityEngine;
using TMPro;

public class MailManager : MonoBehaviour
{
    public GameObject normalMailPrefab;
    public GameObject redMailPrefab;
    public Transform spawnPoint;
    public AudioSource wrongDropSound;
    public GameObject gameWonScreen;
    public GameObject gameOverScreen;
    public UICounter uiCounter;

    private GameObject draggingMail;
    private int wrongTurnCounter = 0;
    private bool isGameOver = false;

    public bool IsGameOver => isGameOver;

    void Start()
    {
        SpawnMail();
    }

    public void SetDraggingMail(GameObject mail)
    {
        draggingMail = mail;
    }

    public void SpawnMail()
    {
        if (!isGameOver)
        {
            GameObject mailPrefab = Random.value < 0.5f ? normalMailPrefab : redMailPrefab;
            GameObject mail = Instantiate(mailPrefab, spawnPoint.position, Quaternion.identity);
            mail.GetComponent<Mail>().Initialize(this);
        }
    }

    public void SortMail(bool isCorrect, GameObject mail)
    {
        if (isCorrect)
        {
            if (mail.CompareTag("Mail"))
            {
                uiCounter.IncrementNormalMail();
            }
            uiCounter.IncrementTotalMail();
            Destroy(mail);
            SpawnMail();

            if (uiCounter.normalMailCount >= 15)
            {
                isGameOver = true;
                gameWonScreen.SetActive(true);
            }
        }
        else
        {
            WrongDrop();
        }
    }

    public void WrongDrop()
    {
        wrongDropSound.Play();
        wrongTurnCounter++;
        if (wrongTurnCounter >= 3)
        {
            isGameOver = true;
            gameOverScreen.SetActive(true);
        }
    }
}
