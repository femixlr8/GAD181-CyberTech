using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera mCamera;
    private Rigidbody2D rb;
    public float force;

    
    void Start()
    {
        // following the cross hair through camera

        mCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        mousePos = mCamera.ScreenToWorldPoint(Input.mousePosition);

        //direction for the bullet to travel in

        Vector3 direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;

        //To mantain the cursor position the magnitude and speed for ht ebullet will be the same

        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

        //Atan 2  figures the angle of the bullet line it follows but in radians so mathf.radians to degrees is used, since quantorion works on degres

        float deg = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, deg);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
