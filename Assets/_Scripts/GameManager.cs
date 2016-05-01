using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public TextAsset highscoreList;
    private string highscoreListAsOneString;
    private string[] highscoreListAsArrayOfStrings;
    private string[] disconnector;
    private int[] highscoreListAsArrayOfInts;

    public static int playerCount = 3;
    private int activePlayer=0;
    public static int[] playerScore;
    private int[] points;
    private int[] scoreMultiplier;
    private float[] timeCheck;
    public static int totalScore;

    public static bool gameActive;

    // Use this for initialization.
    void Awake()
    {
        highscoreListAsArrayOfInts = new int[10];
        highscoreListAsArrayOfStrings = new string[10];
        disconnector = new string[10];

        playerScore = new int[playerCount];
        points = new int[playerCount];
        scoreMultiplier = new int[playerCount];
        timeCheck = new float[playerCount];

        //Check if instance already exists.
        if (instance == null)
        {
            //If not, set instance to this.
            instance = this;
        }

        //If instance already exists and it's not this:
        else if (instance != this)
        {
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        }

        //Sets this to not be destroyed when reloading scene.
        DontDestroyOnLoad(gameObject);

        //Call the InitGame function to initialize the game.
        InitGame();
    }

    //Is called every frame and keeps track of the game state.
    void Update()
    {
        totalScore = playerScore[0] + playerScore[1] + playerScore[2];
        
        //Just for testing purposes.
        if (Input.GetKeyDown(KeyCode.P)) { AddScore(activePlayer); Debug.Log(playerScore[0]);}
        if (Input.GetKeyDown(KeyCode.O)) { AddScore(activePlayer+1); Debug.Log(playerScore[1]); }
        if (Input.GetKeyDown(KeyCode.Q)) { Debug.Log(totalScore); }
        if (Input.GetKeyDown(KeyCode.E)) { EndGame(); }

        //End the game.
        if (gameActive == false)
        {
            EndGame();
        }
        
    }
    //Initializes the game.
    void InitGame()
    {
        gameActive = true;
    }

    //End the game.
    void EndGame()
    {
        UpdateHighscore();
    }

    //Add points to the PlayerScore of the player that cleaned dirt.
    void AddScore(int activePlayer)
    {
        //Check for multiplier condition and change multiplier accordingly.
        if (timeCheck[activePlayer] > Time.time-5)
        {
            scoreMultiplier[activePlayer] = scoreMultiplier[activePlayer] + 1; 
        }

        else
        {
            scoreMultiplier[activePlayer] = 1;
        }

        points[activePlayer] = 100 * scoreMultiplier[activePlayer];
        playerScore[activePlayer] = playerScore[activePlayer] + points[activePlayer];
        timeCheck[activePlayer] = Time.time;
    }

    //Get the Highscore values of the Highscore.txt - Assigns values to HighscoreListAsArrayOfStrings[].
    void GetHighscore()
    {
        //Convert txt file to string.
        highscoreListAsOneString = highscoreList.text;

        //Assign values to the disconnectors used to separate the Highscore string.
        for (int i = 0; i < disconnector.Length; i++)
        {
            disconnector[i] = ", ";
        }

        //Split string into an array of strings.
        highscoreListAsArrayOfStrings = highscoreListAsOneString.Split(disconnector, StringSplitOptions.RemoveEmptyEntries);
    }

    //Compare the TotalScore with the values in the Highscore.txt and update the file if appropriate.
    void UpdateHighscore()
    {
        GetHighscore();

        //Convert every element into an int.
        for (int i = 0; i < highscoreListAsArrayOfInts.Length; i++)
        {
            highscoreListAsArrayOfInts[i] = Int32.Parse(highscoreListAsArrayOfStrings[i]);
        }

        //Compare and update the values.
        for (int i = 0; i < highscoreListAsArrayOfInts.Length; i++)
        {
            if (totalScore > highscoreListAsArrayOfInts[i])
            {
                for (int p = 0; p < highscoreListAsArrayOfInts.Length - (i+1); p++)
                {
                    highscoreListAsArrayOfInts[(9 - p)] = highscoreListAsArrayOfInts[(9 - (p + 1))];
                }

                highscoreListAsArrayOfInts[i] = totalScore;
                i = highscoreListAsArrayOfInts.Length;
            }
        }

        //Convert elements to strings.
        for (int i = 0; i < highscoreListAsArrayOfInts.Length; i++)
        {
            highscoreListAsArrayOfStrings[i] = highscoreListAsArrayOfInts[i].ToString() + ", ";
        }

        //Merge strings.
        highscoreListAsOneString = "";

        for (int i = 0; i < highscoreListAsArrayOfStrings.Length; i++)
        {
            highscoreListAsOneString = highscoreListAsOneString + highscoreListAsArrayOfStrings[i];
        }

        //Rewrite the textfile.
        File.WriteAllText(Application.dataPath+ @"\Persistent Data\Highscore.txt", highscoreListAsOneString);
    } 
}
