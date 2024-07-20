using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MemoryBlitzManager : MonoBehaviour
{
    public Canvas gameCanvas;
    public Button[] sequenceButtons;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI turnText;
    public float gameDuration = 30f;
    public float buttonLightDuration = 1f;

    private float timer;
    private int score;
    private bool isGameActive;
    public bool hasPlayedBefore;
    private List<int> sequence;
    private int currentStep;
    private bool playerTurn;

    // Start is called before the first frame update
    void Start()
    {
        timer = gameDuration;
        score = 0;
        isGameActive = true;
        gameCanvas.gameObject.SetActive(isGameActive);

        foreach (Button btn in sequenceButtons)
        {
            btn.onClick.AddListener(() => OnSequenceButtonClick(btn));
        }

        UpdateScoreText();
        UpdateTimer();

        sequence = new List<int>();
        playerTurn = false;
        StartCoroutine(ShowSequence());
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameActive)
        {
            timer -= Time.deltaTime;
            UpdateTimer();

            if (timer <= 0)
            {
                EndGame();
            }
        }
    }

    IEnumerator ShowSequence()
    {
        turnText.text = "Watch the sequence...";
        playerTurn = false;

        // Add a new random step to the sequence
        sequence.Add(Random.Range(0, sequenceButtons.Length));

        // Show the sequence to the player
        for (int i = 0; i < sequence.Count; i++)
        {
            sequenceButtons[sequence[i]].GetComponent<Image>().color = Color.yellow;
            yield return new WaitForSeconds(buttonLightDuration);
            sequenceButtons[sequence[i]].GetComponent<Image>().color = Color.white;
            yield return new WaitForSeconds(0.5f);
        }

        turnText.text = "Your turn!";
        playerTurn = true;
        currentStep = 0;
    }

    void OnSequenceButtonClick(Button button)
    {
        if (isGameActive && playerTurn)
        {
            StartCoroutine(ButtonClickFeedback(button));

            int buttonIndex = System.Array.IndexOf(sequenceButtons, button);
            if (buttonIndex == sequence[currentStep])
            {
                currentStep++;
                if (currentStep >= sequence.Count)
                {
                    score++;
                    UpdateScoreText();
                    StartCoroutine(ShowSequence());
                }
            }
            else
            {
                playerTurn = false;
                StartCoroutine(ShowSequence());
            }
        }
    }

    IEnumerator ButtonClickFeedback(Button button)
    {
        button.GetComponent<Image>().color = Color.green;
        yield return new WaitForSeconds(0.2f);
        button.GetComponent<Image>().color = Color.white;
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }

    void UpdateTimer()
    {
        timerText.text = "Time: " + Mathf.Clamp(timer, 0, gameDuration).ToString("F2");
    }

    void EndGame()
    {
        isGameActive = false;
        timerText.text = "Game Over!";
        gameCanvas.gameObject.SetActive(isGameActive);

        foreach (Button btn in sequenceButtons)
        {
            btn.interactable = false;
        }
    }
}
