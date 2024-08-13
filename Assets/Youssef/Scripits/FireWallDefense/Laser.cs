using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float laserSpeed = 3f;

    private FWWinningCondition pointsManager;

    public GameObject explosionPrefab;

    public AudioClip enemyExplosionSFX;
    private AudioSource audioSource;

    void Start()
    {
        // finds the componet since its private in a gameobject
        pointsManager = GameObject.Find("ScoreManager").GetComponent<FWWinningCondition>();

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //same concept as shipenemy but upward
        Vector2 movement = Vector2.up * laserSpeed * Time.deltaTime;

        transform.Translate(movement);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("ship"))
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            audioSource.PlayOneShot(enemyExplosionSFX);

            Destroy(collision.gameObject);

            pointsManager.UpdatePoint(1);

            Destroy(gameObject);

        }

        if (collision.gameObject.tag == ("bary"))
        {
            Destroy(gameObject);
        }
    }
}
