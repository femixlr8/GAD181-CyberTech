using UnityEngine;
using System.Collections;

public class MailManager : MonoBehaviour
{
    public GameObject mailPrefab;
    public GameObject mailRedPrefab;
    public Transform mailSpawnPoint;
    public Transform folder;
    public Transform folderRed;
    public UICounter uiCounter;

    private GameObject currentMail;
    private bool isSpawningMail = false;

    void Start()
    {
        SpawnMail();
    }

    void SpawnMail()
    {
        if (currentMail == null && !isSpawningMail)
        {
            isSpawningMail = true;
            int randomValue = Random.Range(0, 2); // Generate a random integer value 0 or 1
            Debug.Log("Random Value: " + randomValue); // Debug log to check the random value

            Vector3 spawnPosition = mailSpawnPoint.position; // Ensure mail spawns at the correct position
            spawnPosition.z = 0f; // Ensure z-coordinate is zero

            if (randomValue == 0)
            {
                currentMail = Instantiate(mailPrefab, spawnPosition, Quaternion.identity);
                Debug.Log("Spawning Normal Mail");
            }
            else
            {
                currentMail = Instantiate(mailRedPrefab, spawnPosition, Quaternion.identity);
                Debug.Log("Spawning Red Mail");
            }

            Mail mailComponent = currentMail.GetComponent<Mail>();
            mailComponent.OnMailDropped += HandleMailSorting;
        }
    }

    void HandleMailSorting(GameObject mail)
    {
        Debug.Log("Handling mail sorting...");
        Collider2D mailCollider = mail.GetComponent<Collider2D>();

        if (mailCollider.bounds.Intersects(folder.GetComponent<Collider2D>().bounds))
        {
            Debug.Log("Mail intersects normal folder.");
            if (mail.CompareTag("Mail"))
            {
                uiCounter.IncrementNormalMail();
                Debug.Log("Normal mail count incremented.");
            }
            uiCounter.IncrementTotalMail();
            Debug.Log("Total mail count incremented.");
            Destroy(mail);
            currentMail = null; // Set currentMail to null to allow new mail to spawn
            StartCoroutine(WaitAndSpawnNewMail());
        }
        else if (mailCollider.bounds.Intersects(folderRed.GetComponent<Collider2D>().bounds))
        {
            Debug.Log("Mail intersects red folder.");
            uiCounter.IncrementTotalMail();
            Debug.Log("Total mail count incremented.");
            Destroy(mail);
            currentMail = null; // Set currentMail to null to allow new mail to spawn
            StartCoroutine(WaitAndSpawnNewMail());
        }
        else
        {
            Debug.Log("Mail not dropped on any folder, returning to start position.");
            // If not dropped on a folder, return the mail to the starting position
            mail.transform.position = mail.GetComponent<Mail>().startPosition;
        }
    }

    IEnumerator WaitAndSpawnNewMail()
    {
        yield return new WaitForSeconds(1.5f);
        isSpawningMail = false;
        SpawnMail();
    }
}

