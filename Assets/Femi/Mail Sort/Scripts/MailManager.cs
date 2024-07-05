using UnityEngine;

public class MailManager : MonoBehaviour
{
    public GameObject mailPrefab;
    public GameObject mailRedPrefab;
    public Transform mailSpawnPoint;
    public Transform folder;
    public Transform folderRed;
    public UICounter uiCounter;

    void Start()
    {
        SpawnMail();
    }

    void SpawnMail()
    {
        GameObject mail;
        if (Random.value < 0.5f)
        {
            mail = Instantiate(mailPrefab, mailSpawnPoint.position, Quaternion.identity);
        }
        else
        {
            mail = Instantiate(mailRedPrefab, mailSpawnPoint.position, Quaternion.identity);
        }

        Mail mailComponent = mail.GetComponent<Mail>();
        mailComponent.OnMailDropped += HandleMailSorting;
    }

    void HandleMailSorting(GameObject mail)
    {
        Collider2D mailCollider = mail.GetComponent<Collider2D>();

        if (mailCollider.bounds.Intersects(folder.GetComponent<Collider2D>().bounds))
        {
            if (mail.CompareTag("Mail"))
            {
                uiCounter.IncrementNormalMail();
            }
            uiCounter.IncrementTotalMail();
            Destroy(mail);
            SpawnMail();
        }
        else if (mailCollider.bounds.Intersects(folderRed.GetComponent<Collider2D>().bounds))
        {
            uiCounter.IncrementTotalMail();
            Destroy(mail);
            SpawnMail();
        }
        else
        {
            // If not dropped on a folder, return the mail to the starting position
            mail.transform.position = mail.GetComponent<Mail>().startPosition;
        }
    }
}
