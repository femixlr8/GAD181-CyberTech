using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDetector : MonoBehaviour
{
    bool inRange = false;
    [SerializeField] GameObject interactable;
    [SerializeField] GameObject interactionUI;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>() != null)
        {
            Debug.Log("Player has been hit");
            inRange = true;
            interactionUI.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerMovement>() != null)
        {
            inRange = false;
            interactionUI.SetActive(false); void Start()
            {

            }
        }
    }
}