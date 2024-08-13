using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public Button startGameButton;

    void Start()
    {
        // Add listeners to the buttons
        startGameButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OnStartGameClicked);
    }

    // Method to be called when the "Start Game" button is clicked
    void OnStartGameClicked()
    {
        
        SceneManager.LoadScene("Scene_01");
    }
}
    