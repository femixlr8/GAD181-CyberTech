using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipHealthPoints : MonoBehaviour
{
    private int healthPoint = 1;
    private AudioSource audioExplosion;

    void Start()
    {
        audioExplosion = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // added collider since the ships are children of another gameobject
        if (collision.collider.gameObject.tag == ("ship"))
        {
            Destroy(collision.collider.gameObject);

            healthPoint -= 1;

            if (healthPoint <= 0)
            {
                if (audioExplosion != null)
                {
                    audioExplosion.Play();
                }
                Destroy(gameObject, audioExplosion.clip.length);
            }

        }
    }


}
