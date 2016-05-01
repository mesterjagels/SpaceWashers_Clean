using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public TextAsset HighscoreList;
    private string HighscoreListAsOneString;
    private string[] HighscoreListAsArrayOfStrings;
    private int[] HighscoreListAsArrayOfInts;
    private string[] Disconnector;
    
    

    public static int PlayerNumber = 3;
    public static int[] PlayerScore = new int[PlayerNumber];
    public static int TotalScore = PlayerScore[0] + PlayerScore[1] + PlayerScore[2];

    public static bool GameActive;

    // Use this for initialization
    void Awake()
    {
        HighscoreListAsArrayOfInts = new int[10];
        HighscoreListAsArrayOfStrings = new string[10];
        Disconnector = new string[10];

        //Check if instance already exists
        if (instance == null)
        {
            //if not, set instance to this
            instance = this;
        }

        //If instance already exists and it's not this:
        else if (instance != this)
        {
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        }

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

        //Call the InitGame function to initialize the game
        InitGame();
    }

    //Is called every frame and keeps track of the game state
    void Update()
    {
        if (GameActive == false)
        {
            EndGame();
        }
        if (Input.GetKeyDown(KeyCode.P)) { TotalScore = TotalScore + 9999; Debug.Log(TotalScore); }

        if (Input.GetKeyDown(KeyCode.E)) { EndGame(); }
    }
    //Initializes the game
    void InitGame()
    {
        GameActive = true;
    }

    //End the game
    void EndGame()
    {
        UpdateHighscore();
    }

    //Add points to the PlayerScore of the player that cleaned dirt.
    void AddScore(/*Active Player*/)
    {
        PlayerScore[0/*Active Player*/] = PlayerScore[0/*Active Player*/] + 100;
    }

    //Compare the TotalScore with the values in the Highscore.txt and update the file if appropriate
    void UpdateHighscore()
    {
        //Convert txt file to string
        HighscoreListAsOneString = HighscoreList.text;


        //Assign values to the disconnectors used to separate the Highscore string
        for (int i = 0; i < Disconnector.Length; i++)
        {
        Disconnector[i] = ", ";
        }

        //Split string into an array of strings
        HighscoreListAsArrayOfStrings = HighscoreListAsOneString.Split(Disconnector, StringSplitOptions.RemoveEmptyEntries);

        //Convert every element into an int
        for (int i = 0; i < HighscoreListAsArrayOfInts.Length; i++)
        {
            HighscoreListAsArrayOfInts[i] = Int32.Parse(HighscoreListAsArrayOfStrings[i]);
        }

        //Compare and update the values
        for (int i = 0; i < HighscoreListAsArrayOfInts.Length; i++)
        {
            if (TotalScore > HighscoreListAsArrayOfInts[i] )
            {
                for (int p = 0; p < HighscoreListAsArrayOfInts.Length - (i+1); p++)
                {
                    HighscoreListAsArrayOfInts[(9 - p)] = HighscoreListAsArrayOfInts[(9 - (p + 1))];
                }

                HighscoreListAsArrayOfInts[i] = TotalScore;
                i = HighscoreListAsArrayOfInts.Length;
            }
        }

        //Convert elements to strings
        for (int i = 0; i < HighscoreListAsArrayOfInts.Length; i++)
        {
            HighscoreListAsArrayOfStrings[i] = HighscoreListAsArrayOfInts[i].ToString() + ", ";
        }

        //Merge strings
        HighscoreListAsOneString = "";

        for (int i = 0; i < HighscoreListAsArrayOfStrings.Length; i++)
        {
            HighscoreListAsOneString = HighscoreListAsOneString + HighscoreListAsArrayOfStrings[i];
        }

        //Rewrite the textfile
        File.WriteAllText(Application.dataPath+ @"\Persistent Data\Highscore.txt", HighscoreListAsOneString);
    } 
}
