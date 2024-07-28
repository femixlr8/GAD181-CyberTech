using UnityEngine;
using TMPro;

public class SCCControls : MonoBehaviour
{
    public TMP_InputField codeInputField; // Reference to the input field

    private void Start()
    {
        codeInputField.onValueChanged.AddListener(OnInputChanged);
    }

    private void OnInputChanged(string input)
    {
        // Additional input handling logic can be added here if needed
    }
}
