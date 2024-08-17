using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCAnimation : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if( Input.GetKeyDown(KeyCode.A) )
        {
            animator.SetBool("isWalkingLeft", true);
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            animator.SetBool("isWalkingLeft", false);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            animator.SetBool("isWalkingRight", true);
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            animator.SetBool("isWalkingRight", false);
        }
    }
}
