using UnityEngine;
using System.Collections;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public TextAsset HighscoreList;
    private string HighscoreListAsOneString;
    private string[] HighscoreListAsArrayOfStrings;
    private int[] HighscoreListAsArrayOfInts;
    private string[] Disconnector = new string[10];
    

    public static int PlayerNumber = 3;
    public static int[] PlayerScore = new int[PlayerNumber];
    public static int TotalScore = PlayerScore[0] + PlayerScore[1] + PlayerScore[2];

    public static bool GameActive;

    // Use this for initialization
    void Awake()
    {
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

    //Compare the TotalScore at the end of the game with the values in the Highscore list and update the list if appropriate
    void UpdateHighscore()
    {
        //Convert txt file to string
        HighscoreListAsOneString = HighscoreList.text;


        //Assign values to the disconnectors used to separate the Highscore string
        for (int i = 0; i <= 9; i++)
        {
            Disconnector[i] = (i+1)+". ";
        }

        //Split string into an array of strings
        HighscoreListAsArrayOfStrings = HighscoreListAsOneString.Split(Disconnector, 10, System.StringSplitOptions.None);

        //Convert every element into an int
        for (int i = 0; i <= 9; i++)
        {
            HighscoreListAsArrayOfInts[i] = System.Int32.Parse(HighscoreListAsArrayOfStrings[i]);
        }

        //Compare and update the values
        for (int i = 0; i <= 9; i++)
        {
            if (TotalScore > HighscoreListAsArrayOfInts[i])
            {
                for (int o = 0; o <= 9-i; o++)
                {
                    HighscoreListAsArrayOfInts[9 - o] = HighscoreListAsArrayOfInts[9 - (o + 1)];
                }

                HighscoreListAsArrayOfInts[i] = TotalScore;
            }
        }

        //Convert elements to strings
        for (int i = 0; i <= 9; i++)
        {
            HighscoreListAsArrayOfStrings[i] = (i + 1) + ". " + HighscoreListAsArrayOfInts[i].ToString();
        }

        //Rewrite the textfile
        File.WriteAllText(@"C:\Users\Nespitt\Documents\SpaceWashers_Clean\Assets\Persistent Data\Highscore.txt", (HighscoreListAsArrayOfStrings[0] + HighscoreListAsArrayOfStrings[1] + HighscoreListAsArrayOfStrings[2] + HighscoreListAsArrayOfStrings[3] + HighscoreListAsArrayOfStrings[4] + HighscoreListAsArrayOfStrings[5] + HighscoreListAsArrayOfStrings[6] + HighscoreListAsArrayOfStrings[7] + HighscoreListAsArrayOfStrings[8] + HighscoreListAsArrayOfStrings[9]));
    } 
}
