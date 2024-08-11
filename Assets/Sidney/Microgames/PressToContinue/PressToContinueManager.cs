using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PressToContinueManager : MonoBehaviour
{
    public Button pressButton;
    public Button hereButton;
    public Button toButton;
    public Button continueButton;
    public TextMeshProUGUI timerText;

    private int currentStep = 0;
    private float timer = 10f; // Set the timer duration here
    private bool gameActive = true;

    void Start()
    {
        pressButton.onClick.AddListener(() => CheckOrder(pressButton, "Press"));
        hereButton.onClick.AddListener(() => CheckOrder(hereButton, "Here"));
        toButton.onClick.AddListener(() => CheckOrder(toButton, "To"));
        continueButton.onClick.AddListener(() => CheckOrder(continueButton, "Continue"));
    }

    void Update()
    {
        if (gameActive)
        {
            timer -= Time.deltaTime;
            timerText.text = "Time: " + Mathf.Round(timer).ToString();

            if (timer <= 0)
            {
                gameActive = false;
                GameOver(false);
            }
        }
    }

    void CheckOrder(Button clickedButton, string buttonText)
    {
        if (!gameActive) return;

        string[] correctOrder = { "Press", "Here", "To", "Continue" };

        if (buttonText == correctOrder[currentStep])
        {
            currentStep++;
            clickedButton.interactable = false; // Disable the button after it's clicked

            if (currentStep >= correctOrder.Length)
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
        }
        else
        {
            Debug.Log("You lost!");
        }
    }
}
