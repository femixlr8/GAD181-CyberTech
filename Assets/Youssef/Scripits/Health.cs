using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    
    public int startingHealth;
    public int maxHealth = 50;
    public GameController gameController;

    
    // Start is called before the first frame update
    void Start()
    {
        startingHealth = maxHealth; 
    }

    // Update is called once per frame
    public void DamageApplied(int damage)
    {
        startingHealth -= damage;

        if (startingHealth <= 0)
        {
            if (gameController != null)
            {
                gameController.MoveToNextScene();
            }
        }
    }
}
