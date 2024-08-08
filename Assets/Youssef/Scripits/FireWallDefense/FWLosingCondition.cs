using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FWLosingCondition : MonoBehaviour
{
    
    private int healthPoint = 4;

    //UI and Audio
    public AudioSource explosionSFX;

    public Image[] livesUI;

    void Start()
    {
        explosionSFX = GetComponent<AudioSource>();
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
                if (explosionSFX != null)
                {
                    explosionSFX.Play();
                }
                Destroy(gameObject, explosionSFX.clip.length);

                // Notify MicroGameManager to load the next scene
                MicroGameManager.Instance.LoadNextScene();
            }

        }
    }


}
