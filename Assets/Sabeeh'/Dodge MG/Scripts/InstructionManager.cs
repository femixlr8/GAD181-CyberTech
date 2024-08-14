using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionManager : MonoBehaviour
{
    [SerializeField] GameObject instructionPanel;
    void Start()
    {
        Time.timeScale = 0f;
        instructionPanel.SetActive(true);
    }

    public void StartButton()
    {
        Time.timeScale = 1f;
        instructionPanel.SetActive(false);
    }
}
