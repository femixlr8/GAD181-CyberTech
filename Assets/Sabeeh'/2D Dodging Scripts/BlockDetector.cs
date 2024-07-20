using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CyberTech.Dodge
{
    public class BlockDetector : MonoBehaviour
    {
        public GameManager gameManager;
        // Start is called before the first frame update
        void Start()
        {


        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "block")
            {
                gameManager.AddScore();
                Destroy(collision.gameObject);
            }
        }
    }
}