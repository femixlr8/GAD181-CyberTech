using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 15f;
    public float jumpStrength = 500f;
    public bool isGrounded;
    public float jumpHeight = 4f;
    public float timeToJump = 0.4f;
    private float gravity = 1f;


    private Rigidbody2D rb;

   


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJump, 2);
        jumpStrength = Mathf.Abs(gravity) * timeToJump; 
        rb.gravityScale = gravity / Physics2D.gravity.y;
    }

    private void Update()
    {
        Jump();
        Move();
    }

    private void Move()
    {
        float x = Input.GetAxis("Horizontal");
        Vector2 position = new Vector2(x * speed,0);
        rb.velocity = new Vector2(position.x, rb.velocity.y);
        
    }




    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
           // rb.AddForce(transform.up * jumpStrength, ForceMode2D.Force);
           rb.velocity = new Vector2 (rb.velocity.x, jumpStrength);
        }
        if (Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0 )
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }
    
}
