using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<Virus> viruses;

    //Hardcoded variables 
    private float startingTime = 30f;

    //Global variables
    private float timeRemaining;
    private HashSet<Virus> currentVirus = new HashSet<Virus> ();
    private int score;
    private bool playing = false;

    public void StartGame()
    {
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
        playing = true;
    }

    public void GameOver(int type)
    {
        //Hide all the moles
        foreach(Virus virus in viruses)
        {
            virus.StopGame();
        }

        //stop the game
        playing = false;
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
