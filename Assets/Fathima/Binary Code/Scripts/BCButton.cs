using UnityEngine;
using UnityEngine.UI;

public class BCButton : MonoBehaviour
{
    public int number; // 0 or 1
    private Button button;
    private BCGameManager gameManager;

    void Start()
    {
        button = GetComponent<Button>();
        gameManager = FindObjectOfType<BCGameManager>();
        button.onClick.AddListener(OnButtonClicked);
    }

    void OnButtonClicked()
    {
        gameManager.OnButtonClick(number);
    }
}
