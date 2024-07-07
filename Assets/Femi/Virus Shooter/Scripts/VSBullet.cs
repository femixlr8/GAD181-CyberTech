using UnityEngine;

public class VSBullet : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision Detected");
        if (collision.CompareTag("Virus"))
        {
            Debug.Log("Hit Virus");
            Destroy(collision.gameObject); // Destroy the virus
            Destroy(gameObject); // Destroy the bullet
            VSGameManager.instance.AddScore(1); // Add score
        }
    }
}
