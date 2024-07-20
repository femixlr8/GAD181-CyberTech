using UnityEngine;

public class ICBlueIcon : MonoBehaviour
{
    private ICGameManager gameManager;

    void Start()
    {
        gameManager = FindFirstObjectByType<ICGameManager>();
    }

    void OnMouseDown()
    {
        gameManager.GameOver();
        Destroy(gameObject);
    }
}
