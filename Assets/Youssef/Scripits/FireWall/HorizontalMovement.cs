using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovement : MonoBehaviour
{
    public float playerSpeed = 5f;
    private float horizontalInput;

    // when moving rigth , the player will move right and then multiplying it by negative will move left
    void Update()
    {
        // gets horiznatl keys  in input manager
        horizontalInput = Input.GetAxis("Horizontal");

        // delta time to not move 60 frames per sec
        Vector2 movement =Vector2.right * horizontalInput * playerSpeed * Time.deltaTime;

        transform.Translate(movement);
     
    }
}
