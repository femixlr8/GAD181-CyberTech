using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GuideSceneManager : MonoBehaviour
{
    [SerializeField] private string MainMenu = "Main Menu";

    public Button backButton;

    private void Start()
    {
        backButton.GetComponentInParent<UnityEngine.UI.Button>().onClick.AddListener(GoToMainMenu);
    }

    void GoToMainMenu()
    {
        // Load the main menu scene.
        SceneManager.LoadScene(MainMenu);
    }
}
