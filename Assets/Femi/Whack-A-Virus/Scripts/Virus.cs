using System.Collections;
using UnityEngine;

public class Virus : MonoBehaviour
{
    [Header("Graphics")]
    [SerializeField] private Sprite VirusA;
    [SerializeField] private Sprite VirusAHit;
    [SerializeField] private Sprite VirusB;
    [SerializeField] private Sprite VirusBHit;

    [Header("Game Manager")]
    [SerializeField] private GameManager gameManager;

    //The offset of the sprite to hide it
    private Vector2 startPosition = new Vector2(0f, -2f);
    private Vector2 endPosition = Vector2.zero;

    //How long it takes to show a virus
    private float showDuration = 0.5f;
    private float duration = 1f;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;
    private Vector2 boxOffset;
    private Vector2 boxSize;
    private Vector2 boxOffsetHidden;
    private Vector2 boxSizeHidden;

    //Virus Parameters
    private bool hittable = true;

    public enum VirusType { Standard, VirusB, Bomb};
    private VirusType virusType;
    private float bRate = 0.25f;
    private int lives;
    private float bombRate = 0f;
    private int virusIndex = 0;

    private IEnumerator ShowHide(Vector2 start, Vector2 end)
    {
        //Make sure we start at the start
        transform.localPosition = start;

        //Show the virus
        float elapsed = 0f;
        while (elapsed < showDuration)
        {
            transform.localPosition = Vector2.Lerp(start, end, elapsed / showDuration);
            boxCollider2D.offset = Vector2.Lerp(boxOffsetHidden, boxOffset, elapsed/ showDuration);
            boxCollider2D.size = Vector2.Lerp(boxSizeHidden, boxSize, elapsed/ showDuration);

            //Update at max framerate
            elapsed += Time.deltaTime;
            yield return null;
        }

        //Make sure its exactly at the end
        transform.localPosition = end;
        boxCollider2D.offset = boxOffset;
        boxCollider2D.size = boxSize;

        //Wait for duration to pass
        yield return new WaitForSeconds(duration);

        //Hide the virus
        elapsed = 0f;
        while(elapsed < showDuration)
        {
            transform.localPosition = Vector2.Lerp(end, start, elapsed / showDuration);
            boxCollider2D.offset = Vector2.Lerp(boxOffset, boxOffsetHidden, elapsed/ showDuration);
            boxCollider2D.size = Vector2.Lerp(boxSize, boxSizeHidden, elapsed/ showDuration);
            //Update at max framerate.
            elapsed += Time.deltaTime;
            yield return null;
        }

        //Make sure we're exactly back at the start position.
        transform.localPosition = start;
        boxCollider2D.offset = boxOffsetHidden;
        boxCollider2D.size = boxSizeHidden;

        //If we got to the end and its still hittable then player missed it
        if (hittable)
        {
            hittable = false;
            //we only give time penality if its not a bomb
            gameManager.Missed(virusIndex, virusType != VirusType.Bomb);
        }
    }

    public void Activate(int level)
    {
        SetLevel(level);
        CreateNext();
        StartCoroutine(ShowHide(startPosition, endPosition));
    }

    private void CreateNext()
    {
        float random = Random.Range(0f, 1f);
        if(random < bombRate)
        {
            //make a bomb.
            virusType = VirusType.Bomb;
            
        }
        else
        {
            if (random < bRate)
            {
                //Create a VirusB 
                virusType = VirusType.VirusB;
                spriteRenderer.sprite = VirusB;
                lives = 2;
            }
            else
            {
                //Create a normal Virus
                virusType = VirusType.Standard;
                spriteRenderer.sprite = VirusA;
                lives = 1;
            }
        }
        

        //Mark as hittable so we can register an onclick event
        hittable = true;
    }

    private void Awake()
    {
        //Get references to the components to be used
        spriteRenderer = GetComponent<SpriteRenderer>();   
        boxCollider2D = GetComponent<BoxCollider2D>();
        //Work out collider values
        boxOffset = boxCollider2D.offset;
        boxSize = boxCollider2D.size;
        boxOffsetHidden = new Vector2(boxOffset.x, -startPosition.y /2f);
        boxSizeHidden = new Vector2(boxSize.x, 0f);
    }

    private void OnMouseDown()
    {
        if (hittable)
        {
            switch (virusType)
            {
                case VirusType.Standard:
                    spriteRenderer.sprite = VirusAHit;
                    gameManager.AddScore(virusIndex);
                    //Stop the animation
                    StopAllCoroutines();
                    StartCoroutine(QuickHide());
                    //Turn off hittable so player cant keep tapping for score
                    hittable = false;
                    break;

                case VirusType.VirusB:
                    //If lives == 2 reduce life
                    if (lives == 2)
                    {
                        spriteRenderer.sprite = VirusB;
                        lives--;
                    }
                    else
                    {
                        spriteRenderer.sprite = VirusBHit;
                        gameManager.AddScore(virusIndex);
                        //stop the animation
                        StopAllCoroutines();
                        StartCoroutine(QuickHide());
                        //turn off hittable so player cant keep tapping for score
                        hittable = false;
                    }
                    break;
                case VirusType.Bomb:
                    //GAME OVER, 1 for bomb
                    gameManager.GameOver(1);
                    break;
                default:
                    break;
            }
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

    public  void Hide()
    {
        //Set the appropriate Virus parameters to hide it.
        transform.localPosition = startPosition;
        boxCollider2D.offset = boxOffsetHidden;
        boxCollider2D.size = boxSizeHidden;
    }

    private void SetLevel(int level)
        //As the level progresses, the game gets harder
    {
        //As level increases increase the bomb rate to 0.25 at level 10
        bombRate = Mathf.Min(level * 0.025f, 0.25f);

        //Increases the amounts of Double-Tap Virus until 100% at level 40
        bRate = Mathf.Min(level * 0.025f, 1f);

        //Duration bounds get quicker as we progress. no cap on insanity.
        float durationMin = Mathf.Clamp(1 - level * 0.1f, 0.01f, 1f);
        float durationMax = Mathf.Clamp(2 - level * 0.1f, 0.01f, 2f);
        duration = Random.Range(durationMin, durationMax);
    }

    //Used by the game manager to uniquely idenitfy virus
    public void SetIndex(int index)
    {
        virusIndex = index;
    }

    public void StopGame()
    {
        hittable = false;
        StopAllCoroutines();
    }
}
    
