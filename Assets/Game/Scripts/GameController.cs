using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public List<string> sceneNames; // List of scene names to load
    private int currentIndex = 0; // Index of the current scene being played

    private void Start()
    {
        ShuffleScenes(); // Shuffle the scene list to randomize play order
        LoadNextScene(); // Start with the first scene
    }

    private void ShuffleScenes()
    {
        // Fisher-Yates shuffle algorithm to shuffle sceneNames list
        for (int i = sceneNames.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            string temp = sceneNames[i];
            sceneNames[i] = sceneNames[randomIndex];
            sceneNames[randomIndex] = temp;
        }
    }

    private void LoadNextScene()
    {
        if (currentIndex < sceneNames.Count)
        {
            SceneManager.LoadScene(sceneNames[currentIndex]);
            currentIndex++;
        }
        else
        {
            Debug.Log("All scenes have been played.");
            // Optionally handle what happens when all scenes have been played
        }
    }

    // Method to handle game over from minigame managers
    public void GameOver(int type)
    {
        StartCoroutine(LoadNextSceneAsync(type));
    }

    private IEnumerator LoadNextSceneAsync(int type)
    {
        yield return new WaitForSecondsRealtime(2f); // Delay before loading next scene

        Time.timeScale = 1; // Ensure time scale is normal before loading next scene

        LoadNextScene();
    }

    private void Awake()
    {
        // Ensure this object persists across scene changes
        DontDestroyOnLoad(gameObject);
    }

}
