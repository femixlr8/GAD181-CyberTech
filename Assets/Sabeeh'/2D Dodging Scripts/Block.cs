using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public float speed = 10f;
    Rigidbody2D rb;
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        rb.velocity = -Vector3.up * speed;
    }
}
