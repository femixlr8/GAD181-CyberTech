using UnityEngine;
using UnityEngine.EventSystems;

public class Mail : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public Vector3 startPosition;
    private bool isDragging = false;

    public event System.Action<GameObject> OnMailDropped;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; // Ensure the z position is zero
            transform.position = mousePosition;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        isDragging = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
        OnMailDropped?.Invoke(gameObject);
        transform.position = startPosition;
    }

    void OnMouseDown()
    {
        isDragging = true;
    }

    void OnMouseUp()
    {
        isDragging = false;
        OnMailDropped?.Invoke(gameObject);
        transform.position = startPosition;
    }
}

