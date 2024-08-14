using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public TextMeshProUGUI healthValue;
  

    public float health;
    private SpriteRenderer colorChange;
    AudioSource audioSource;
    [SerializeField] AudioClip hitSFX;

    private void Start()
    {
        colorChange =  GetComponent<SpriteRenderer>();
        audioSource = FindObjectOfType<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hazard")
        {
            health--;
            audioSource.PlayOneShot(hitSFX,0.7f);
            StartCoroutine(PlayerHitFeedback());
            
        }
    }
    public void Update()
    {
        UpdateTextHealth();
    }

    private void UpdateTextHealth()
    {
       healthValue.text = health.ToString();
    }

    IEnumerator PlayerHitFeedback()
    {
        colorChange.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        colorChange.color = Color.white;
    }
}

