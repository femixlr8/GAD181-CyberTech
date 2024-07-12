using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarMovementHorizontal : MonoBehaviour
{
    private Rigidbody2D barRb;

    public float speed;

    void Start()
    {
        barRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horMov = Input.GetAxis(("Horizontal"));

        barRb.velocity = Vector2.right * speed * horMov;
    }
}
