using UnityEngine;

public class ICRedIcon : MonoBehaviour
{
    private ICGameManager gameManager;

    void Start()
    {
        gameManager = FindFirstObjectByType<ICGameManager>();
    }

    void OnMouseDown()
    {
        gameManager.AddScore();
        gameManager.CollectRedIcon();
        Destroy(gameObject);
    }
}
