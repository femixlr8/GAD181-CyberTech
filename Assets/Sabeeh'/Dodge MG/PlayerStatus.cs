using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public TextMeshProUGUI healthValue;
  

    public float health;
   

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hazard")
        {
            health--;
            
        }
    }
    public void Update()
    {
        UpdateTextHealth();
    }

    private void UpdateTextHealth()
    {
       healthValue.text = health.ToString();
    }
}

