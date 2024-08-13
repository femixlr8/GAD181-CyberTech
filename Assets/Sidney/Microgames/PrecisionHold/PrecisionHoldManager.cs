using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PrecisionHoldManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Button holdButton;          // Reference to the button
    public Slider progressBar;         // Reference to the progress bar (Slider)
    public TextMeshProUGUI timerText;  // Reference to the timer text

    private float holdTime = 2f;       // Time to hold the button (seconds)
    private float timer = 10f;         // Countdown timer
    private bool isHolding = false;
    private bool gameActive = true;

    void Start()
    {
        progressBar.value = 0f;
        Transform handle = progressBar.transform.Find("Handle Slide Area/Handle");
        if (handle != null)
        {
            handle.gameObject.SetActive(false);
        }

        // Add EventTrigger manually
        EventTrigger trigger = holdButton.gameObject.AddComponent<EventTrigger>();

        // OnPointerDown
        EventTrigger.Entry pointerDownEntry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerDown
        };
        pointerDownEntry.callback.AddListener((data) => { OnPointerDown((PointerEventData)data); });
        trigger.triggers.Add(pointerDownEntry);

        // OnPointerUp
        EventTrigger.Entry pointerUpEntry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerUp
        };
        pointerUpEntry.callback.AddListener((data) => { OnPointerUp((PointerEventData)data); });
        trigger.triggers.Add(pointerUpEntry);
    }

    void Update()
    {
        if (gameActive)
        {
            timer -= Time.deltaTime;
            timerText.text = "Time: " + Mathf.Round(timer).ToString();

            if (timer <= 0)
            {
                gameActive = false;
                GameOver(false);
            }

            if (isHolding)
            {
                progressBar.value += Time.deltaTime / holdTime;
                if (progressBar.value >= 1f)
                {
                    gameActive = false;
                    GameOver(true);
                }
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isHolding = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isHolding = false;
    }

    void GameOver(bool won)
    {
        if (won)
        {
            Debug.Log("You won!");
        }
        else
        {
            Debug.Log("You lost!");
        }

        // Notify MicroGameManager to load the next scene
        MicroGameManager.Instance.LoadNextScene();
    }
}
