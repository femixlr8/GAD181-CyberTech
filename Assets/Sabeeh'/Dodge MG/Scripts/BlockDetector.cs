using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CyberTech.Dodge
{
    public class BlockDetector : MonoBehaviour
    {
        public DodgingGameManager gameManager;
        AudioSource audioSource;
        public AudioClip clip;
        
        // Start is called before the first frame update
        void Start()
        {
            audioSource = FindObjectOfType<AudioSource>();

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Hazard")
            {
                gameManager.AddScore();
                audioSource.PlayOneShot(clip,0.2f);
                Destroy(collision.gameObject);

            }
        }
    }
}