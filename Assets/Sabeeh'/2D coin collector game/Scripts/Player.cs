using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 40f;
    public float jumpStrength = 500f;
    public bool isGrounded;


    private Rigidbody2D rb;




    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Jump();
        Move();
    }

    private void Move()
    {
        float x = Input.GetAxis("Horizontal");
        Vector2 position = new Vector2(x * speed * Time.deltaTime, 0);
        rb.velocity = new Vector2(position.x, rb.velocity.y);

    }




    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            rb.AddForce(transform.up * jumpStrength, ForceMode2D.Force);

        }

    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }
}