using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipHealthPoints : MonoBehaviour
{
    
    private int healthPoint = 3;

    //UI and Audio
    private AudioSource audioExplosion;
    public Image[] livesUI;

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
            
           for (int i = 0; i < livesUI.Length; i++)
                if(i < healthPoint)
                {
                    livesUI[i].enabled = true;
                }
                else
                {
                    livesUI[i].enabled = false;
                }


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
