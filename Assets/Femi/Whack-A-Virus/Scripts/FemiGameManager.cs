using System.Collections.Generic;
using UnityEngine;

public class FemiGameManager : MonoBehaviour
{
    [SerializeField] private List<Virus> viruses;

    [Header("UI Objects")]
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject outOfTimeText;
    [SerializeField] private GameObject bombText;
    [SerializeField] private TMPro.TextMeshProUGUI timeText;
    [SerializeField] private TMPro.TextMeshProUGUI scoreText;

    //Hardcoded variables 
    private float startingTime = 30f;

    //Global variables
    private float timeRemaining;
    private HashSet<Virus> currentVirus = new HashSet<Virus>();
    private int score;
    private bool playing = false;

    // Event for game over
    public delegate void OnGameEnd();
    public static event OnGameEnd GameEnded;

    public void StartGame()
    {
        Debug.Log("Game Started");
        //Hide/show the UI elements to show or not show
        outOfTimeText.SetActive (false);
        bombText.SetActive (false);
        gameUI.SetActive (true);
        //Hide all the visible virus
        for (int i = 0; i < viruses.Count; i++)
        {
            viruses[i].Hide();
            viruses[i].SetIndex(i);
        }
        //remove any old game state
        currentVirus.Clear ();

        //start with 30 seconds
        timeRemaining = startingTime;
        score = 0;
        scoreText.text = "0";
        playing = true;
    }

    public void GameOver(int type)
    {
        //SHow the message
        if(type == 0)
        {
            outOfTimeText.SetActive(true);
        }
        else
        {
            bombText.SetActive(true);
        }
        //Hide all the virus
        foreach(Virus virus in viruses)
        {
            virus.StopGame();
        }

        //stop the game
        playing = false;

        // Trigger event that the game has ended
        GameEnded?.Invoke();
    }

    void Update()
    {
        if (playing)
        {
            //update time
            timeRemaining -= Time.deltaTime;
            if(timeRemaining <= 0)
            {
                timeRemaining= 0;
                GameOver(0);
            }

            timeText.text = $"{(int)timeRemaining / 60}:{(int)timeRemaining % 60:D2}";
            //Check if we need to add any more virus 
            if(currentVirus.Count <= (score / 10))
            {
                //Choose a random virus
                int index = Random.Range(0, viruses.Count);
                //Doesnt matter if its alr doing something, just try again next frame
                if (!currentVirus.Contains(viruses[index]))
                {
                    currentVirus.Add(viruses[index]);
                    viruses[index].Activate(score / 10);
                }
            }
        }
    }

    public void AddScore(int virusIndex)
    {
        //Add and update score
        score += 1;
        scoreText.text = $"{score}";
        //increase time by a  little bit
        timeRemaining += 1;
        //remove from active virus
        currentVirus.Remove(viruses[virusIndex]);
    }

    public void Missed(int virusIndex, bool isVirus)
    {
        if (isVirus)
        {
            //decrease time by a little bit
            timeRemaining -= 2;
        }

        //remove from active virus
        currentVirus.Remove(viruses[virusIndex]);
    }
}
