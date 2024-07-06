using UnityEngine;

public class Antivirus : MonoBehaviour
{
    private bool isClicked = false; // Flag to track if antivirus has been clicked

    public bool IsClicked
    {
        get { return isClicked; }
    }

    void OnMouseDown()
    {
        // When clicked, set isClicked flag to true
        isClicked = true;

        // Perform other actions as needed (e.g., increase score)
        RTGameManager.Instance.IncreaseScore();

        // Destroy the antivirus GameObject when clicked
        Destroy(gameObject);
    }
}
