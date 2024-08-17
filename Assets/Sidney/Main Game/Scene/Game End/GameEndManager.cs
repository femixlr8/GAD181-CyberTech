using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameEndManager : MonoBehaviour
{
    public Canvas winCanvas; // Assign in the Inspector
    public Canvas loseCanvas; // Assign in the Inspector
    public Button quitButton; // Assign in the Inspector
    public Button quitButton2; // Assign in the Inspector

    private void Start()
    {
        // Ensure canvases and button are properly set
        if (winCanvas == null || loseCanvas == null || quitButton == null)
        {
            Debug.LogError("Please assign all references in the Inspector.");
            return;
        }

        // Set canvases to inactive at the start
        winCanvas.gameObject.SetActive(false);
        loseCanvas.gameObject.SetActive(false);

        // Add listener to quit button
        quitButton.onClick.AddListener(QuitGame);
        quitButton2.onClick.AddListener(QuitGame);

        CheckGameEnd();
    }

    private void CheckGameEnd()
    {
        // Check the player score and activate the appropriate canvas
        if (MicroGameManager.Instance.playerScore >= 10)
        {
            winCanvas.gameObject.SetActive(true);
        }
        else
        {
            loseCanvas.gameObject.SetActive(true);
        }
    }

    private void QuitGame()
    {
        // Quit the game
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stop playing in editor
#endif
    }
}
