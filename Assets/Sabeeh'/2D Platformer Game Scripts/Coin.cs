using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Coin : MonoBehaviour
{
    AudioSource audioSource;
    //[SerializeField] AudioClip clip;
   // PlatformGameManager scoreManager;

    // Function called at the start of the script
    void Start()
    {
     //   scoreManager = FindObjectOfType<PlatformGameManager>();

        //Get the AudioSource component attached to this object
        audioSource = GetComponent<AudioSource>();

    }

    //Function to handle collision 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // scoreManager.AddScore();
           // audioSource.PlayOneShot(clip);
            //Destroy the collided coin
            Destroy(gameObject);

        }
    }

}

