using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerCoin : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip clip;
    PlatfromGameManager scoreManager;

    // Function called at the start of the script
    void Start()
    {
        scoreManager = FindObjectOfType<PlatfromGameManager>();

        //Get the AudioSource component attached to this object
        audioSource = GetComponent<AudioSource>();

    }
    

    //Function to handle collision 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            scoreManager.AddScore();
            //audioSource.PlayOneShot(clip);
            //Destroy the collided coin
            Destroy(gameObject);

        }
    }
}
