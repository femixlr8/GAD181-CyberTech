using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Coin : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip clip;
    [SerializeField] TextMeshProUGUI coinCounter;

    // Function called at the start of the script
    void Start()
    {
        //Get the AudioSource component attached to this object
        audioSource = GetComponent<AudioSource>();

    }
    // Coin.gameObject = Instantiate(new GameObject(coin));

    //Function to handle collision 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            //Destroy the collided coin
            Destroy(gameObject);

        }
    }
}

