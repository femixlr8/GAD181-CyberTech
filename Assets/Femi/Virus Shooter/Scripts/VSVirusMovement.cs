using UnityEngine;

public class VSVirusMovement : MonoBehaviour
{
    public float speed = 6f;

    void Update()
    {
        Debug.Log("Virus is moving");
        MoveVirus();
    }

    void MoveVirus()
    {
        // Move the virus to the right
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        // Destroy the virus if it moves off the screen
        if (transform.position.x > Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x)
        {
            Destroy(gameObject);
            VSGameManager.instance.CheckGameOver();
        }
    }

    void OnDestroy()
    {
        VSGameManager.instance.CheckGameOver();
    }
}
