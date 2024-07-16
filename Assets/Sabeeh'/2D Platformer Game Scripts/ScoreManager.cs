using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreValue;
    public int currentScore;

    public void SetScore()
    {
        currentScore++;
        scoreValue.text = currentScore.ToString();
    }
    
}
