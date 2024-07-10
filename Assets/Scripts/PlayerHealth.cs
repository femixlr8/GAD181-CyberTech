using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
   
    AudioSource audioSource;
    public int health;
    [SerializeField] TextMeshProUGUI playerhealth;
    [SerializeField] AudioClip clip;
    private void OnTriggerEnter(Collider other)
    {
        //if (other.TryGetComponent(out Hazard hazard))
        {
            //// Reduce player's health by the damage caused by the hazard
           // health -= hazard.damage;
            if (audioSource != null)
            {
                audioSource.PlayOneShot(clip);
            }

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        // Initialize player health to a random value between 1 and the specified maximum health

        health = Random.Range(1, health);
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        //Update the text to display the player's current health
        playerhealth.text = " Player Health " + health.ToString();
    }
}

