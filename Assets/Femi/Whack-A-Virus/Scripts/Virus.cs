using System.Collections;
using UnityEngine;

public class Virus : MonoBehaviour
{
    [Header("Graphics")]
    [SerializeField] private Sprite VirusA;
    [SerializeField] private Sprite VirusAHit;
    [SerializeField] private Sprite VirusB;
    [SerializeField] private Sprite VirusBHit;

    //The offset of the sprite to hide it
    private Vector2 startPosition = new Vector2(0f, -2f);
    private Vector2 endPosition = Vector2.zero;

    //How long it takes to show a virus
    private float showDuration = 0.5f;
    private float duration = 1f;

    private SpriteRenderer spriteRenderer;

    //Virus Parameters
    private bool hittable = true;
    private IEnumerator ShowHide(Vector2 start, Vector2 end)
    {
        //Make sure we start at the start
        transform.localPosition = start;

        //Show the virus
        float elapsed = 0f;
        while (elapsed < showDuration)
        {
            transform.localPosition = Vector2.Lerp(start, end, elapsed / showDuration);
            //Update at max framerate
            elapsed += Time.deltaTime;
            yield return null;
        }

        //Make sure its exactly at the end
        transform.localPosition = end; 

        //Wait for duration to pass
        yield return new WaitForSeconds(duration);

        //Hide the virus
        elapsed = 0f;
        while(elapsed < showDuration)
        {
            transform.localPosition = Vector2.Lerp(end, start, elapsed / showDuration);
            //Update at max framerate.
            elapsed += Time.deltaTime;
            yield return null;
        }

        //Make sure we're exactly back at the start position.
        transform.localPosition = start;
    }

    private void Start()
    {
        StartCoroutine(ShowHide(startPosition, endPosition));
    }

    private void Awake()
    {
        //Get references to the components to be used
        spriteRenderer = GetComponent<SpriteRenderer>();   
    }

    private void OnMouseDown()
    {
        if ( hittable)
        {
            spriteRenderer.sprite = VirusAHit;
            //Stop the animation
            StopAllCoroutines();
            StartCoroutine(QuickHide());
            //Turn off hittable so that player cant keep tapping for score
            hittable = false;
        }
    }

    private IEnumerator QuickHide()
    {
        yield return new WaitForSeconds(0.25f);

        //Whist player was waiting it may have spawned again here, so just check that hasnt happened before hiding it, this will stop it flickering in that case
        if(!hittable)
        {
            Hide();
        }
    }

    private void Hide()
    {
        //Set the appropriate Virus parameters to hide it.
        transform.localPosition = startPosition;
    }
}
    
