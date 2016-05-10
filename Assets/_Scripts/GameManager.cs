using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System;
using InControl;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public TextAsset highscoreList;
    public Text[] highscore;
    private string highscoreListAsOneString;
    public string[] highscoreListAsArrayOfStrings;
    private string[] disconnector;
    private int[] highscoreListAsArrayOfInts;

    public int playerCount = 3;
    public int[] cleanerScore;
    public int captainScore;
    public int[] cleanerPoints;
    public int captainPoints;
    private int[] cleanerMultiplier;
    public int captainMultiplier;
    private float[] cleanerTimeCheck;
    private float captainTimeCheck;
    public int totalScore;
    private int scoreBuffer = 0;

    public bool gameActive;

    void Start()
    {
        highscoreListAsArrayOfInts = new int[10];
        highscoreListAsArrayOfStrings = new string[10];
        highscore = new Text[10];
        disconnector = new string[10];

        cleanerScore = new int[playerCount];

        for(int i = 0; i < playerCount; i++)
        {
            cleanerScore[i] = 0;
        }

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
        
            //Add captain score
            if (captainTimeCheck < Time.time-10)
            {
                if (captainPoints == 1000)
                {
                    if (captainMultiplier < 6)
                    {
                        captainMultiplier = captainMultiplier + 1;
                        AddCaptainScore();
                        captainPoints = 1000;
                    }
                }
                else
                {
                    ResetCaptainMultiplier();
                    AddCaptainScore();
                    captainPoints = 1000;
                }

                captainTimeCheck = Time.time;
            }

            totalScore = scoreBuffer + captainScore;

            scoreBuffer = 0;                      

            //Testing
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Debug.Log("Captain score: " + captainScore);
                Debug.Log("Player1 score: " + cleanerScore[0]);
                Debug.Log("Player2 score: " + cleanerScore[1]);
                Debug.Log("Player3 score: " + cleanerScore[2]);
                Debug.Log("Total score: " + totalScore);
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
               EndGame();
            }
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
        UnityEngine.SceneManagement.SceneManager.LoadScene("Teamscore");
    }

    //Add points to the PlayerScore of the player that cleaned dirt.
    public void AddCleanerScore(int cleaningPlayer)
    {
        if (gameActive == true)
        {
            //Check for multiplier condition and change multiplier accordingly.
            if (cleanerTimeCheck[cleaningPlayer] > Time.time - 5)
            {
               if (cleanerMultiplier[cleaningPlayer] < 6)
               {
                   cleanerMultiplier[cleaningPlayer] = cleanerMultiplier[cleaningPlayer] + 1;
               }
            }
            else
            {
                ResetCleanerMultiplier(cleaningPlayer);
            }

            cleanerPoints[cleaningPlayer] = 100 * cleanerMultiplier[cleaningPlayer];
            cleanerScore[cleaningPlayer] = cleanerScore[cleaningPlayer] + cleanerPoints[cleaningPlayer];

            cleanerPoints[cleaningPlayer] = 0;
            cleanerTimeCheck[cleaningPlayer] = Time.time;
        }
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
    public void ResetCleanerMultiplier(int cleaningPlayer)
    {
        cleanerMultiplier[cleaningPlayer] = 1;
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

    //Check player count
    void GetPlayers()
    {
        if (Application.loadedLevelName == "Title")
        {
           playerCount = FindObjectOfType<TitleScreen>().playerCountInTitle;
        }
        if(Application.loadedLevelName == "Jasper")
        {

        }
    }
}
