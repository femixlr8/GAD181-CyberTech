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
        if (mailManager != null)
        {
            if (gameObject.CompareTag("Mail") && other.CompareTag("NormalFolder"))
            {
                mailManager.SortMail(true, this.gameObject);
            }
            else if (gameObject.CompareTag("MailRed") && other.CompareTag("RedFolder"))
            {
                mailManager.SortMail(true, this.gameObject);
            }
            else
            {
                mailManager.SortMail(false, this.gameObject);
            }
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
        if (mailManager == null)
        {
            mailManager = FindObjectOfType<MailManager>();
        }

        // Handle mail movement if dragging
        if (Input.GetMouseButton(0) && !mailManager.IsGameOver)
        {
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            newPosition.z = 0f;
            transform.position = newPosition;
        }
    }
}

