using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class DTFlosingCondition : MonoBehaviour
{
   
    public float timer = 2f;
   
    [SerializeField] TextMeshProUGUI timerText;
    

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        timerText.text = timer.ToString("F2");

        if (timer <= 0) 
        {
            MicroGameManager.Instance.LoadNextScene();
        }
    }
}
