using UnityEngine;

public class Mail : MonoBehaviour
{
    private MailManager mailManager;
    private Vector3 startPosition;
    private bool isDragging = false;

    void Start()
    {
        mailManager = FindObjectOfType<MailManager>();
        startPosition = transform.position;
    }

    public void Initialize(MailManager manager)
    {
        mailManager = manager;
        startPosition = transform.position;
    }

    void OnMouseDown()
    {
        if (mailManager != null)
        {
            isDragging = true;
            mailManager.SetDraggingMail(this.gameObject);
        }
    }

    void OnMouseUp()
    {
        isDragging = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (mailManager != null)
        {
            if ((gameObject.CompareTag("Mail") && other.CompareTag("NormalFolder")) ||
                (gameObject.CompareTag("MailRed") && other.CompareTag("RedFolder")))
            {
                mailManager.SortMail(true, this.gameObject);
            }
            else if ((gameObject.CompareTag("Mail") && other.CompareTag("RedFolder")) ||
                     (gameObject.CompareTag("MailRed") && other.CompareTag("NormalFolder")))
            {
                mailManager.SortMail(false, this.gameObject);
            }
        }
    }

    void Update()
    {
        if (isDragging && !mailManager.IsGameOver)
        {
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            newPosition.z = 0f;
            transform.position = newPosition;
        }
    }
}
