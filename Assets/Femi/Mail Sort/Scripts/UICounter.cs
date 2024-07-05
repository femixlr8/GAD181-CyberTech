using UnityEngine;
using TMPro;

public class UICounter : MonoBehaviour
{
    public TMP_Text normalMailCounter;
    public TMP_Text totalMailCounter;

    private int normalMailCount = 0;
    private int totalMailCount = 0;

    void Start()
    {
        normalMailCounter.text = "Normal Mail: " + normalMailCount;
        totalMailCounter.text = "Total Mail: " + totalMailCount;
    }

    public void IncrementNormalMail()
    {
        normalMailCount++;
        normalMailCounter.text = "Normal Mail: " + normalMailCount;
    }

    public void IncrementTotalMail()
    {
        totalMailCount++;
        totalMailCounter.text = "Total Mail: " + totalMailCount;
    }
}

