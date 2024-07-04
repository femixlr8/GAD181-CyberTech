using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int startingHealth;
    public int maxHealth = 50;
    public GameController gameController; // Reference to GameController

    void Start()
    {
        startingHealth = maxHealth;
    }

    public void DamageApplied(int damage)
    {
        startingHealth -= damage;

        if (startingHealth <= 0)
        {
            Debug.LogWarning("GameController reference not assigned to Health script.");
            
        }
    }
}
