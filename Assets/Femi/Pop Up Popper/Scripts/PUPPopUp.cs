using UnityEngine;
using UnityEngine.EventSystems;

public class PUPPopUp : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        // Increase score and destroy pop-up
        FindObjectOfType<PUPGameManager>().IncreaseScore();
        Destroy(gameObject);
    }
}
