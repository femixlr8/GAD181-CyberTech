using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSystem : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioClip brickBreaking;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = brickBreaking;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.tag == ("ball"))
        {
            if(brickBreaking != null) 
            {
                audioSource.Play();
            }

            Destroy(gameObject, brickBreaking.length);
        }

    }
}
