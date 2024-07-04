using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDetector : MonoBehaviour
{
    public FemiGameManager gameManager;
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
            Virus virus = collision.gameObject.GetComponent<Virus>();
            if (virus != null)
            {
                int virusIndex = virus.GetIndex();
                gameManager.AddScore(virusIndex);
                Destroy(collision.gameObject);
            }
        }
    }
}
