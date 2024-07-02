using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    //Gizmos and range for enmey to walk
    [SerializeField] public GameObject leftPoint;
    [SerializeField] public GameObject rightPoint;

    private Rigidbody2D rb;
    private Transform startingPoint;

    public float enemySpeed;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(leftPoint.transform.position, 0.5f);
        Gizmos.DrawWireSphere(rightPoint.transform.position, 0.5f);
        Gizmos.DrawLine(leftPoint.transform.position, rightPoint.transform.position);
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startingPoint = rightPoint.transform;
    }

    // sets up the enmey walk range
    void Update()
    {
        Vector2 direction = startingPoint.position - transform.position;

        Debug.Log("Starting Point: " + startingPoint.position);
        Debug.Log("Current Position: " + transform.position);

        if (startingPoint == rightPoint.transform)
        {
            rb.velocity = new Vector2(enemySpeed, rb.velocity.y);
            Debug.Log("Moving right");
        }
        else
        {
            rb.velocity = new Vector2(-enemySpeed, rb.velocity.y);
            Debug.Log("Moving left");
        }

        if (Vector2.Distance(transform.position, startingPoint.position) < 0.1f)
        {
            Debug.Log("Switching direction");
            startingPoint = (startingPoint == rightPoint.transform) ? leftPoint.transform : rightPoint.transform;
        }
    }
}
