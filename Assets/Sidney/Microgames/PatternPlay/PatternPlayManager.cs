using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PatternPlayManager : MonoBehaviour
{
    public TextMeshProUGUI[] patternDisplayTexts;  // Text objects displaying the pattern
    public Button[] inputButtons;                  // Buttons the player can press
    public string[] symbols = { "<", "!", "?", "{" }; // The symbols used in the pattern
    public float timeRemaining = 20f;                   // Countdown timer
    public TextMeshProUGUI timerText;  // Reference to the Timer Text (Optional)

    private List<string> pattern = new List<string>();   // The current pattern to follow
    private int currentStep = 0;                         // Current step in the pattern
    private bool gameActive = true;

    void Start()
    {
        GeneratePattern();
        UpdatePatternDisplay();
        foreach (Button btn in inputButtons)
        {
            btn.onClick.AddListener(() => OnButtonClick(btn.GetComponentInChildren<TextMeshProUGUI>().text));
        }
    }

    void Update()
    {
        if (gameActive)
        {
            timeRemaining -= Time.deltaTime;
            timerText.text = "Time: " + Mathf.Round(timeRemaining).ToString();

            if (timeRemaining <= 0)
            {
                gameActive = false;
                GameOver(false);
            }
        }
    }

    void GeneratePattern()
    {
        for (int i = 0; i < patternDisplayTexts.Length; i++)
        {
            string symbol = symbols[Random.Range(0, symbols.Length)];
            pattern.Add(symbol);
        }
    }

    void UpdatePatternDisplay()
    {
        for (int i = 0; i < pattern.Count; i++)
        {
            patternDisplayTexts[i].text = pattern[i];
        }
    }

    void OnButtonClick(string clickedSymbol)
    {
        if (!gameActive) return;

        clickedSymbol = clickedSymbol.Trim();
        string expectedSymbol = pattern[currentStep].Trim();

        Debug.Log("Clicked Symbol: " + clickedSymbol + " (Length: " + clickedSymbol.Length + ")");
        Debug.Log("Expected Symbol: " + expectedSymbol + " (Length: " + expectedSymbol.Length + ")");

        if (clickedSymbol == expectedSymbol)
        {
            currentStep++;
            if (currentStep >= pattern.Count)
            {
                gameActive = false;
                GameOver(true);
            }
        }
        else
        {
            gameActive = false;
            GameOver(false);
        }
    }

    void GameOver(bool won)
    {
        if (won)
        {
            Debug.Log("You won!");
            // Additional logic for winning can be added here
            MicroGameManager.Instance.IncreaseScore();
        }
        else
        {
            Debug.Log("You lost!");
            // Additional logic for losing can be added here
        }

        // Notify MicroGameManager to load the next scene
        MicroGameManager.Instance.LoadNextScene();
    }
}
