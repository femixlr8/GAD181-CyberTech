using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FWDHowToPlay : MonoBehaviour
{
    public GameObject instructionsFWDPannel;

    public float displayTime = 5f;

    void Start()
    {

        StartCoroutine(HidePanelAfterTime());
    }

    IEnumerator HidePanelAfterTime()
    {

        instructionsFWDPannel.SetActive(true);


        yield return new WaitForSeconds(displayTime);


        instructionsFWDPannel.SetActive(false);
    }
}
