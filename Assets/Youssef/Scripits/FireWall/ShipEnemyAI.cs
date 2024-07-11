using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipEnemyAI : MonoBehaviour
{
    public float speed = 3f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //same concept in horizontal line but without the input
        Vector2 movement = Vector2.right * speed * Time.deltaTime;

        transform.Translate(movement);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //since the ships will go right forever, they will hit the boundarie and then get * by -1 to chnage direction to left and so on
        if (collision.gameObject.tag ==("bary"))
        {
            speed *= -1;
        }
    }
}
