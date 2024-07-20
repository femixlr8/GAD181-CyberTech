using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MicroGameManager : MonoBehaviour
{
    public static MicroGameManager Instance { get; private set; }

    [Header("Scene Transition Settings")]
    public string[] sceneNames; // Public array to specify scene names

    private int currentSceneIndex = -1; // Start at -1 to load the first scene on start

    private void Awake()
    {
        // Singleton pattern to ensure only one instance
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Preserve across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    private void Start()
    {
        LoadNextScene(); // Load the first scene on start
    }

    public void OnMicroGameEnd()
    {
        StartCoroutine(HandleGameEnd());
    }

    private IEnumerator HandleGameEnd()
    {
        // Optional: Add any end-of-game logic here (e.g., animations, score saving)
        yield return new WaitForSeconds(2f); // Optional delay

        // Load the next scene
        LoadNextScene();
    }

    public void LoadNextScene()
    {
        if (sceneNames.Length == 0)
        {
            Debug.LogError("Scene list is empty. Please add scenes to the list.");
            return;
        }

        // Increment scene index
        currentSceneIndex++;
        if (currentSceneIndex >= sceneNames.Length)
        {
            Debug.Log("No more scenes in the list.");
            return; // Or loop back to the first scene if desired
        }

        // Load the next scene
        SceneManager.LoadScene(sceneNames[currentSceneIndex]);
    }
}
