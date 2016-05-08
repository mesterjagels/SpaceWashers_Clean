using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public TextAsset highscoreList;
    public Text[] highscore;
    private string highscoreListAsOneString;
    public string[] highscoreListAsArrayOfStrings;
    private string[] disconnector;
    private int[] highscoreListAsArrayOfInts;

    public static int playerCount = 3;
   // private int activePlayer = 0;
    public static int[] cleanerScore;
    public static int captainScore;
    private int[] cleanerPoints;
    private int captainPoints;
    private int[] cleanerMultiplier;
    private int captainMultiplier;
    private float[] cleanerTimeCheck;
    private float captainTimeCheck;
    public int totalScore;
    private int scoreBuffer;

    public bool gameActive;

    void Start()
    {
        highscoreListAsArrayOfInts = new int[10];
        highscoreListAsArrayOfStrings = new string[10];
        highscore = new Text[10];
        disconnector = new string[10];

        cleanerScore = new int[playerCount];
        captainScore = 0;
        cleanerPoints = new int[playerCount];
        captainPoints = 1000;
        cleanerMultiplier = new int[playerCount];
        captainMultiplier = 1;
        cleanerTimeCheck = new float[playerCount];
        captainTimeCheck = Time.time;

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

    void Update()
    {
        if (gameActive == true)
        { 
            //Update the totalScore.
            for (int i = 0; i < playerCount; i++)
            {
                scoreBuffer = scoreBuffer + cleanerScore[i];
            }

            totalScore = scoreBuffer + captainScore;
            scoreBuffer = 0;
        
            //Add captain score
            if (captainTimeCheck < Time.time-10)
            {
                if (captainPoints > 0)
                {
                    captainMultiplier = captainMultiplier + 1;
                }
                else
                {
                    ResetCaptainMultiplier();
                }

                AddCaptainScore();
                captainPoints = 1000;
                captainTimeCheck = Time.time;
            }

            //Testing
            if (Input.GetKeyDown(KeyCode.Q)) {
                Debug.Log("Player1 score: " + cleanerScore[0]);
                Debug.Log("Player2 score: " + cleanerScore[1]);
                Debug.Log("Total score: " + totalScore); }
            if (Input.GetKeyDown(KeyCode.E)) { EndGame(); }
        }
    }

    //Initializes the game.
    void InitGame()
    {
        gameActive = true;
    }

    //End the game.
    public void EndGame()
    {
        gameActive = false;
        UpdateHighscore();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Highscore");
    }

    //Add points to the PlayerScore of the player that cleaned dirt.
    public void AddCleanerScore(int cleaningPlayer)
    {
        //Check for multiplier condition and change multiplier accordingly.
        if (cleanerTimeCheck[cleaningPlayer] > Time.time-5)
        {
            cleanerMultiplier[cleaningPlayer] = cleanerMultiplier[cleaningPlayer] + 1; 
        }
        else
        {
            ResetCleanerMultiplier(cleaningPlayer);
        }
        Debug.Log("Gamecontroller says: I have given player " + cleaningPlayer + " points");
        cleanerPoints[cleaningPlayer] = 100 * cleanerMultiplier[cleaningPlayer];
        cleanerScore[cleaningPlayer] = cleanerScore[cleaningPlayer] + cleanerPoints[cleaningPlayer];
        cleanerTimeCheck[cleaningPlayer] = Time.time;
    }

    //Add points to the CaptainScore.
    void AddCaptainScore()
    {
        captainScore = captainScore + (captainPoints * captainMultiplier);
    }

    public void DecreaseCaptainPoints()
    {
        captainPoints = captainPoints - 100;
    }

    //Reset the cleaner multiplier for a specific cleaner.
    public void ResetCleanerMultiplier(int activePlayer)
    {
        cleanerMultiplier[activePlayer] = 1;
    }

    void ResetCaptainMultiplier()
    {
        captainMultiplier = 1;
    }

    //Get the Highscore values of the Highscore.txt - Assigns values to HighscoreListAsArrayOfStrings[].
    public void GetHighscore()
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
