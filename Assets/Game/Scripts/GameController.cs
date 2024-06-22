using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject startPromptPanel;
    public GameObject miniGame1Panel;
    public GameObject miniGame2Panel;
    public GameObject winScreenPanel;

    public Button continueButton;
    public Button miniGame1Button;
    public Button miniGame2Button1;
    public Button miniGame2Button2;



    // Start is called before the first frame update
    void Start()
    {
        ShowStartPrompt();

        continueButton.onClick.AddListener(StartMiniGame1);
        miniGame1Button.onClick.AddListener(CompleteMiniGame1);
        miniGame2Button1.onClick.AddListener(PressMiniGame2Button1);
        miniGame2Button2.onClick.AddListener(PressMiniGame2Button2);
        
    }

    private void ShowStartPrompt()
    {
        startPromptPanel.SetActive(true);
        miniGame1Panel.SetActive(false);
        miniGame2Panel.SetActive(false);
        winScreenPanel.SetActive(false);

    }

    private void StartMiniGame1()
    {
        startPromptPanel.SetActive(false);
        miniGame1Panel.SetActive(true);

    }

    private void CompleteMiniGame1()
    {
        miniGame1Panel.SetActive(false);
        miniGame2Panel.SetActive(true);

    }

    private void PressMiniGame2Button1()
    {
        miniGame2Button1.interactable = false;
        CheckMiniGame2Completion();

    }

    private void PressMiniGame2Button2()
    {
        miniGame2Button2.interactable = false;
        CheckMiniGame2Completion();

    }

    private void CheckMiniGame2Completion()
    {
        if (!miniGame2Button1.interactable && !miniGame2Button2.interactable)
        {
            miniGame2Panel.SetActive(false);
            winScreenPanel.SetActive(true);

        }
    }
}
