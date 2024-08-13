using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public Button startGameButton;
    public Button quitGameButton;

    void Start()
    {
        // Add listeners to the buttons
        startGameButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OnStartGameClicked);
        quitGameButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OnQuitGameClicked);
    }

    // Method to be called when the "Start Game" button is clicked
    void OnStartGameClicked()
    {
        
        SceneManager.LoadScene("Scene_01");
    }

    // Method to be called when the "Quit Game" button is clicked
    void OnQuitGameClicked()
    {
        // Quit the application
        Application.Quit();

        // If running in the Unity Editor, stop play mode
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
    