using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class FWWinningCondition : MonoBehaviour
{
    public int points;

    public TMP_Text pointsNumber;

    public int winningPoint = 20;

    void Start()
    {
        // Always starts with 0 no matter what is written in the inspector
        points = 0;
        pointsNumber.text = "Points: " + points;
    }

    // Method to update the points
    public void UpdatePoint(int additionalPoints)
    {
        points += additionalPoints;

        pointsNumber.text = "Points: " + points;

        if (points >= winningPoint) 
        {
            WinGame();
        }

    }

    void WinGame()
    {
        SceneManager.LoadScene("");
    }
}
