using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DTFWinningCondition : MonoBehaviour
{
    private int totalBricks;
    private int destroyedBricks;

    void Start()
    {
        totalBricks = GameObject.FindGameObjectsWithTag("bricks").Length;

        destroyedBricks = 0;

        Debug.Log("total bricks=" + totalBricks);

    }

    public void BricksDestroyed()
    {
        destroyedBricks++;
        Debug.Log("bricks gone=" + destroyedBricks + "/" + totalBricks);

        if (destroyedBricks >= totalBricks) 
        {
            // Notify MicroGameManager to load the next scene
            MicroGameManager.Instance.LoadNextScene();
        }
    }
}
