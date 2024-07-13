using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSystem : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioClip brickBreaking;

    private AudioSource audioSource;

    private LosingConditionBricks losingCond;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;

        if(brickBreaking != null )
        {
            audioSource.clip = brickBreaking;
        }
        else
        {
            Debug.LogWarning("ask sumone");
        }

        losingCond = FindObjectOfType<LosingConditionBricks>(); 

    }

  
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.tag == ("ball"))
        {
            if(brickBreaking != null) 
            {
                audioSource.Play();

                //destory brick after audio is finished
                Destroy(gameObject, brickBreaking.length);
            }

            //update from losingcondition
            if (losingCond != null)
            {
                losingCond.BricksDestroyed();
            }
            
        }

    }
}
