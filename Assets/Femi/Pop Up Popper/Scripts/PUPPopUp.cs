using UnityEngine;
using UnityEngine.EventSystems;

public class PUPPopUp : MonoBehaviour, IPointerClickHandler
{
    public AudioClip popUpSound; // Assign this in the Inspector

    private AudioSource audioSource;

    void Start()
    {
        // Find the AudioSource on the GameManager
        audioSource = FindObjectOfType<PUPGameManager>().GetComponent<AudioSource>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Ensure the score is increased once per click
        FindObjectOfType<PUPGameManager>().IncreaseScore();

        // Play the pop-up sound effect
        if (audioSource != null && popUpSound != null)
        {
            audioSource.PlayOneShot(popUpSound);
        }

        // Destroy the pop-up after scoring
        Destroy(gameObject);
    }
}
