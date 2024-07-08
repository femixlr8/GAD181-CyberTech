using UnityEngine;

public class VSBullet : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision Detected with: " + collision.gameObject.name); // Debug log
        if (collision.CompareTag("Virus"))
        {
            Debug.Log("Hit Virus"); // Debug log
            Destroy(collision.gameObject); // Destroy the virus
            Destroy(gameObject); // Destroy the bullet
            VSGameManager.instance.AddScore(1); // Add score
        }
    }
}
