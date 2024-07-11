using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float laserSpeed = 3f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //same concept as shipenemy but upward
        Vector2 movement = Vector2.up * laserSpeed * Time.deltaTime;

        transform.Translate(movement);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag ==("ship")) 
        {
            Destroy(collision.gameObject);

            Destroy(gameObject);           
        }

        if(collision.gameObject.tag == ("bary")) 
        {
            Destroy(gameObject);
        }
    }
}
