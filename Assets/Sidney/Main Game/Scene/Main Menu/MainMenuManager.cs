using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public Button startGameButton;
    public Button guideMenuButton;

    void Start()
    {
        // Add listeners to the buttons
        startGameButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OnStartGameClicked);
        guideMenuButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OnGuideButtonClicked);
    }

    // Method to be called when the "Start Game" button is clicked
    void OnStartGameClicked()
    {
        
        SceneManager.LoadScene("Scene_01");
    }

    void OnGuideButtonClicked()
    {
        SceneManager.LoadScene("GuideScene");
    }
}
    