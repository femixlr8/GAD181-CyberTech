using UnityEngine;
using TMPro;

public class UICounter : MonoBehaviour
{
    public TextMeshProUGUI normalMailText;
    public TextMeshProUGUI totalMailText;

    public int normalMailCount = 0;
    public int totalMailCount = 0;

    public void IncrementNormalMail()
    {
        normalMailCount++;
        normalMailText.text = "Normal Mail: " + normalMailCount;
    }

    public void IncrementTotalMail()
    {
        totalMailCount++;
        totalMailText.text = "Total Mail: " + totalMailCount;
    }
}
