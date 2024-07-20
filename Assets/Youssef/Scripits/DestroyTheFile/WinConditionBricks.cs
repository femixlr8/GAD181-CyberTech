using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinConditionBricks : MonoBehaviour
{
    private int totalBricks;
    private int destroyedBricks;

    void Start()
    {
        totalBricks = GameObject.FindGameObjectsWithTag("bricks").Length;

        destroyedBricks = 0;
    }

    public void BricksDestroyed()
    {
        destroyedBricks++;

        if (destroyedBricks >= totalBricks) 
        {
            // Notify MicroGameManager to load the next scene
            MicroGameManager.Instance.LoadNextScene();
        }
    }
}
