using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D player;

    [SerializeField]private float playerSpeed = 10;

    private bool onGround;

    void Start()
    {
        player = GetComponent<Rigidbody2D>();
    }


    void Update()
    {

        //added Input.getaxis Y to avoid using if statements
        player.velocity = new Vector2(Input.GetAxis("Horizontal") * playerSpeed, player.velocity.y);


     

        //INput for player to jump on space key command
        if (Input.GetKey(KeyCode.W) && onGround)
        {
            Jump();
        }

    }

    private void Jump()
    {
        player.velocity = new Vector2(player.velocity.x, playerSpeed);

        onGround = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Ground"))
        {
            onGround = true;
        }
    }

}
