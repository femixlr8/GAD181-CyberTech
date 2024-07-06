using UnityEngine;
using UnityEngine.EventSystems;

public class Mail : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public Vector3 startPosition;
    private bool isDragging = false;
    private int originalSortingOrder;
    private SpriteRenderer spriteRenderer;

    public event System.Action<GameObject> OnMailDropped;

    void Start()
    {
        startPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalSortingOrder = spriteRenderer.sortingOrder;
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
        spriteRenderer.sortingOrder = 10; // Bring to front
        Debug.Log("Dragging mail...");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
        spriteRenderer.sortingOrder = originalSortingOrder; // Reset to original order
        OnMailDropped?.Invoke(gameObject);
        Debug.Log("Ended dragging mail.");
    }

    void OnMouseDown()
    {
        isDragging = true;
        spriteRenderer.sortingOrder = 10; // Bring to front
        Debug.Log("Mouse down on mail.");
    }

    void OnMouseUp()
    {
        isDragging = false;
        spriteRenderer.sortingOrder = originalSortingOrder; // Reset to original order
        OnMailDropped?.Invoke(gameObject);
        Debug.Log("Mouse up on mail.");
    }
}
