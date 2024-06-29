using System.Collections;
using UnityEngine;

public class Virus : MonoBehaviour
{
    //The offset of the spirte to hide it
    private Vector2 startPosition = new Vector2(0f, -2f);
    private Vector2 endPosition = Vector2.zero;

    //How long it takes to show a virus
    private float showDuration = 0.5f;
    private float duration = 1f;

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
}
    
