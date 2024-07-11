using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipHealthPoints : MonoBehaviour
{
    private int healthPoint = 3;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == ("ship"))
        {
            Destroy(collision.gameObject);

            healthPoint -= 1;

            if (healthPoint <= 0)
            {
                Destroy(gameObject);
            }
            
        }
    }


}
