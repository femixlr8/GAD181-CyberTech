using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorButtons : MonoBehaviour
{
    public ColorMatchManager gameManager; // Reference to GameManager
    public string buttonColorName; // Assign in Inspector ("Red", "Yellow", "Green", "Blue")

    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        // Notify GameManager which button was clicked
        gameManager.OnButtonClicked(buttonColorName);
    }

    public void SetInteractable(bool interactable)
    {
        button.interactable = interactable;
    }
}
