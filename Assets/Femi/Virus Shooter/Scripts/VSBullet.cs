using UnityEngine;

public class VSBullet : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Virus"))
        {
            Destroy(collision.gameObject); // Destroy the virus
            Destroy(gameObject); // Destroy the bullet
            VSGameManager.instance.AddScore(1); // Add score
        }
    }
}
