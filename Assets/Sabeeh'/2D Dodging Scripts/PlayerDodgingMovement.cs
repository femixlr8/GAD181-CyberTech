using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgingMovement : MonoBehaviour
{
    public float speed = 15f;

    private Rigidbody2D rb;



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal") * Time.fixedDeltaTime * speed;
        rb.MovePosition(rb.position + Vector2.right * x);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hazard")
        {
           
            Destroy(collision.gameObject);
        }
    }
}
