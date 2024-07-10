using UnityEngine;

public class Mail : MonoBehaviour
{
    private MailManager mailManager;
    private Vector3 startPosition;

    public void Initialize(MailManager manager)
    {
        mailManager = manager;
        startPosition = transform.position;
    }

    void OnMouseDown()
    {
        // Start dragging mail
        if (mailManager != null)
        {
            mailManager.SetDraggingMail(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Mail") && mailManager != null)
        {
            mailManager.SortMail(true); // Correct sorting for normal mail
        }
        else if (other.CompareTag("MailRed") && mailManager != null)
        {
            mailManager.SortMail(false); // Correct sorting for red mail
        }
        else
        {
            mailManager.WrongDrop(); // Incorrect sorting
        }
    }


    void OnMouseUp()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero);
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("NormalFolder"))
            {
                mailManager.IncrementNormalMailCounter();
                Destroy(gameObject);
            }
            else if (hit.collider.CompareTag("RedFolder"))
            {
                mailManager.IncrementTotalMailCounter();
                Destroy(gameObject);
            }
            else
            {
                mailManager.WrongDrop();
                transform.position = startPosition;
            }
        }
    }

    void Update()
    {
        // Handle mail movement if dragging
        if (Input.GetMouseButton(0) && !mailManager.IsGameOver)
        {
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            newPosition.z = 0f;
            transform.position = newPosition;
        }
    }
}

