using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1.0f;
    [SerializeField] float distance = 1.0f;
    [SerializeField] bool moveRight = false;
    Vector2 startPosition;
    void Start()
    {
        startPosition = transform.position;

    }

    void Update()
    {
        MovePlatform();
    }
   void MovePlatform()
    {
        Vector2 newPosition = startPosition;
        if (moveRight == true)
        {
            newPosition.x += Mathf.PingPong(Time.time * movementSpeed, distance);
        }
        else
        {
            newPosition.y += Mathf.PingPong(Time.time * movementSpeed, distance);
        }
        transform.position = newPosition;
    }    
}
