using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DTFHowToPlay : MonoBehaviour
{
    public GameObject instructionsPannel;  

    public float displayTime = 5f; 

    void Start()
    {
        
        StartCoroutine(HidePanelAfterTime());
    }

    IEnumerator HidePanelAfterTime()
    {
        
        instructionsPannel.SetActive(true);

        
        yield return new WaitForSeconds(displayTime);

        
        instructionsPannel.SetActive(false);
    }
}
