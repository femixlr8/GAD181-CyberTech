using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpeedControl : MonoBehaviour
{
    private Rigidbody2D ballRb;

    public float minVel;
    public float maxVel;
    public float speedYaxis;

    void Start()
    {
        ballRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //claming the speed so the balls speed doesnt exceed to the point of the game becoming impossible to play
        if (ballRb.velocity.magnitude > maxVel)
        {
            ballRb.velocity = Vector2.ClampMagnitude(ballRb.velocity, maxVel);
        }

        //but if the ball is too slow , we set it to the min speed
        if (ballRb.velocity.magnitude < minVel)
        {
            if (ballRb.velocity.y < 0) // The ball is moving downwards
            {
                ballRb.velocity = new Vector2(ballRb.velocity.x, -minVel);
            }
            else if (ballRb.velocity.y > 0) // The ball is moving upwards
            {
                ballRb.velocity = new Vector2(ballRb.velocity.x, minVel);
            }
        }

        /// if ball goes out of frame , it spawns in the new vector which is manual input
        /// ps. need to add live lost when it goes out of frame
        if (transform.position.y < speedYaxis)
        {
            transform.position = new Vector2(0, -2);

            //resest the position of the ball when it goes out of boundaries
            ballRb.velocity = new Vector2(0, -minVel);


        }



    }
}
