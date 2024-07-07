using UnityEngine;

public class VSVirusMovement : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        if (transform.position.x > Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x)
        {
            Destroy(gameObject);
            VSGameManager.instance.CheckGameOver(); // Check game over when a virus escapes
        }
    }
}
