using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System;
using InControl;

public class GameManager : MonoBehaviour
{
    public Color babe;
    public Color gangsta;
    public Color grunger;
    public GameObject bubblePrefab;
    public static GameManager instance = null;

    public TextAsset highscoreList;
    public Text[] highscore;
    public string highscoreListAsOneString;
    public string[] highscoreListAsArrayOfStrings;
    public string[] disconnector;
    public int[] highscoreListAsArrayOfInts;

    public int playerCount;
    public int cleanliness;
    public int penalty;
    public int cleanerScore0 = 0;
    public int cleanerScore1 = 0;
    public int cleanerScore2 = 0;
    public int captainScore = 0;
    public int cleanerPoints0 = 0;
    public int cleanerPoints1 = 0;
    public int cleanerPoints2 = 0;
    public int captainPoints = 1000;
    public int cleanerMultiplier0 = 1;
    public int cleanerMultiplier1 = 1;
    public int cleanerMultiplier2 = 1;
    public int captainMultiplier = 1;
    //public float cleanerTimeCheck0;
    //public float cleanerTimeCheck1;
    //public float cleanerTimeCheck2;
    //public float captainTimeCheck;
    public int totalScore = 0;
    public int newHighScore;

    public float timeLeft;
    public float timeLeftBuffer;
    private float initialTimeLeft;
    public float distanceMath;
    public float distanceToTravel;

    [HideInInspector]
    public bool player1Active = false, player2Active = false, player3Active = false;
    public bool outroIsPlaying = false;
    public bool gameActive = false;
    private bool gameEnded = false;

    private int momState=1;

    void Start()
    {
        AkBankManager.LoadBank("main");
        AkSoundEngine.PostEvent("GameMusic", gameObject);
        AkSoundEngine.SetState("GameScreen", "TitleScreen");
        gameActive = false;
        initialTimeLeft = timeLeft;
        highscoreListAsArrayOfInts = new int[10];
        highscoreListAsArrayOfStrings = new string[10];
        highscore = new Text[10];
        disconnector = new string[10];

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
    }

    void Update()
    {
        if (gameActive == true)
        {
            //Add captain score
            /*if (captainTimeCheck < Time.time - 5)
            {
                if (captainPoints == 1000)
                {
                    if (captainMultiplier < 6)
                    {
                        captainMultiplier = captainMultiplier + 1;
                    }

                    AddCaptainScore();
                    captainPoints = 1000;
                }
                else
                {
                    ResetCaptainMultiplier();
                    AddCaptainScore();
                    captainPoints = 1000;
                }

                captainTimeCheck = Time.time;
            }*/
            

            totalScore = cleanerScore0 + cleanerScore1 + cleanerScore2 + captainScore;
        }

        if (!outroIsPlaying)
        {
            if (distanceMath < 0.85f)
            {
                outroIsPlaying = true;
            }
        }
        
        GetPlayers();
    }

    //Initializes the game.
    void InitGame()
    {
        cleanerScore0 = 0;
        cleanerScore1 = 0;
        cleanerScore2 = 0;
        captainScore = 0;
        cleanerPoints0 = 0;
        cleanerPoints1 = 0;
        cleanerPoints2 = 0;
        captainPoints = 1000;
        cleanerMultiplier0 = 1;
        cleanerMultiplier1 = 1;
        cleanerMultiplier2 = 1;
        captainMultiplier = 1;
        totalScore = 0;
        //captainTimeCheck = Time.time;
        momState = 1;
        penalty = 0;
        cleanliness = 0;
        newHighScore = 11;
        timeLeft = initialTimeLeft;
        distanceMath = distanceToTravel;
        outroIsPlaying = false;
        gameActive = true;
    }

    //Game is lost
    public void GameOver()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(4);
    }

    //End the game.
    public void EndGame()
    {
        gameEnded = true;
        gameActive = false;

        captainScore = captainScore + (Mathf.RoundToInt(timeLeft) * 400);
        totalScore = cleanerScore0 + cleanerScore1 + cleanerScore2 + captainScore;

        if (cleanliness > 0)
        {
            if (cleanliness > 100)
            {
                cleanliness = 100;
            }
            
            penalty = ((totalScore / 100) * cleanliness);

            totalScore = totalScore - penalty;
        }
        else if (cleanliness < 0)
        {
            cleanliness = 0;
        }

        if (cleanliness < 100 && cleanliness > 74)
        {
            momState = 5;//Disappointed
        }
        else if (cleanliness < 75 && cleanliness > 49)
        {
            momState = 4;//Surprised Mom
        }
        else if (cleanliness < 50 && cleanliness > 24)
        {
            momState = 3;//Neutral Mom
        }
        else if (cleanliness < 25 && cleanliness > 0)
        {
            momState = 2;//Chill Mom
        }
        else if (cleanliness == 0)
        {
            momState = 1; //Happy Mom
        }

        UpdateHighscore();
        outroIsPlaying = false;
        timeLeftBuffer = timeLeft;
        timeLeft = initialTimeLeft;

        if (cleanliness == 100)
        {
            Application.LoadLevel(5); //Game Over - Made it home before Mom but the ship looks like shit.
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(2);
        }  
    }

    //Add points to the PlayerScore of the player that cleaned dirt.
    public int AddCleanerScore(int cleaningPlayer)
    {
        int dirtPoints=0;

        if (gameActive == true)
        {
            //Check for multiplier condition and change multiplier accordingly.
            if (cleaningPlayer == 0)
            {
                /*if (cleanerTimeCheck0 > Time.time - 5)
                {
                    if (cleanerMultiplier0 < 6)
                    {
                        cleanerMultiplier0 = cleanerMultiplier0 + 1;
                    }
                }
                else
                {
                    ResetCleanerMultiplier(cleaningPlayer);
                }*/

                cleanerPoints0 = 100 * cleanerMultiplier0;
                dirtPoints = cleanerPoints0;
                cleanerScore0 = cleanerScore0 + cleanerPoints0;

                cleanerPoints0 = 0;
                //cleanerTimeCheck0 = Time.time;
            }
            else if (cleaningPlayer == 1)
            {
                /*if (cleanerTimeCheck1 > Time.time - 5)
                {
                    if (cleanerMultiplier1 < 6)
                    {
                        cleanerMultiplier1 = cleanerMultiplier1 + 1;
                    }
                }
                else
                {
                    ResetCleanerMultiplier(cleaningPlayer);
                }*/

                cleanerPoints1 = 100 * cleanerMultiplier1;
                dirtPoints = cleanerPoints1;
                cleanerScore1 = cleanerScore1 + cleanerPoints1;

                cleanerPoints1 = 0;
                //cleanerTimeCheck1 = Time.time;
            }
            else if (cleaningPlayer == 2)
            {
                /*if (cleanerTimeCheck2 > Time.time - 5)
                {
                    if (cleanerMultiplier2 < 6)
                    {
                        cleanerMultiplier2 = cleanerMultiplier2 + 1;
                    }
                }
                else
                {
                    ResetCleanerMultiplier(cleaningPlayer);
                }*/

                cleanerPoints2 = 100 * cleanerMultiplier2;
                dirtPoints = cleanerPoints2;
                cleanerScore2 = cleanerScore2 + cleanerPoints2;

                cleanerPoints2 = 0;
                //cleanerTimeCheck2 = Time.time;
            }
        }
        return dirtPoints;
    }

    public void CaptainPickup()
    {
        captainScore = captainScore + 1000;
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
        if (cleaningPlayer == 0)
        {
            cleanerMultiplier0 = 1;
        }
        else if (cleaningPlayer == 1)
        {
            cleanerMultiplier1 = 1;
        }
        else if (cleaningPlayer == 2)
        {
            cleanerMultiplier2 = 1;
        }
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
                for (int p = 0; p < highscoreListAsArrayOfInts.Length - (i + 1); p++)
                {
                    highscoreListAsArrayOfInts[(9 - p)] = highscoreListAsArrayOfInts[(9 - (p + 1))];
                }

                newHighScore = i;
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
        File.WriteAllText(Application.dataPath + @"\Persistent Data\Highscore.txt", highscoreListAsOneString);
    }

    //Check player count
    void GetPlayers()
    {
        switch (Application.loadedLevelName)
        {
            case "TitleScreen":
                if (gameEnded)
                {
                    gameEnded = false;
                }
                playerCount = FindObjectOfType<TitleScreen>().playerCountInTitle;
                player1Active = FindObjectOfType<TitleScreen>().player1joined;
                player2Active = FindObjectOfType<TitleScreen>().player2joined;
                player3Active = FindObjectOfType<TitleScreen>().player3joined;
                break;

            case "Jasper":
                if (!gameActive)
                {
                    if (!gameEnded)
                    {
                        InitGame();
                    }
                }
                if (!player1Active)
                {
                    GameObject.FindGameObjectWithTag("Player1").SetActive(false);
                }
                else
                {
                    GameObject.FindGameObjectWithTag("Player1").SetActive(true);
                }
                if (!player2Active && GameObject.FindGameObjectWithTag("Player2") != null)
                {
                    GameObject.FindGameObjectWithTag("Player2").SetActive(false);
                }
                else
                {
                    if (GameObject.FindGameObjectWithTag("Player2") != null)
                        GameObject.FindGameObjectWithTag("Player2").SetActive(true);
                }

                if (!player3Active && GameObject.FindGameObjectWithTag("Player3") != null)
                {
                    GameObject.FindGameObjectWithTag("Player3").SetActive(false);
                }
                else
                {
                    if (GameObject.FindGameObjectWithTag("Player3") != null)
                        GameObject.FindGameObjectWithTag("Player3").SetActive(true);
                }
                break;

            case "Teamscore":
                if (Input.GetKey(KeyCode.LeftArrow) || InputManager.Devices[0].Command)
                {
                    Application.LoadLevel(3);
                }
                if (!player1Active)
                {
                    GameObject.FindGameObjectWithTag("Player1").SetActive(false);
                }
                else if (player1Active)
                {
                    GameObject.FindGameObjectWithTag("Player1").SetActive(true);
                }

                if (!player2Active && GameObject.FindGameObjectWithTag("Player2") != null)
                {
                    GameObject.FindGameObjectWithTag("Player2").SetActive(false);
                }
                else
                {
                    if(GameObject.FindGameObjectWithTag("Player2") != null) {
                    GameObject.FindGameObjectWithTag("Player2").SetActive(true);
                    }
                }

                if (!player3Active && GameObject.FindGameObjectWithTag("Player3") != null)
                {
                    GameObject.FindGameObjectWithTag("Player3").SetActive(false);
                }
                else
                {
                    if(GameObject.FindGameObjectWithTag("Player3") != null) {
                        GameObject.FindGameObjectWithTag("Player3").SetActive(true);
                    }
                }
                break;

            case "Highscore":
                if (Input.GetKey(KeyCode.LeftArrow) || InputManager.Devices[0].Command)
                {
                    Application.LoadLevel(0);
                }

                if (!player1Active)
                {
                    GameObject.FindGameObjectWithTag("Player1").SetActive(false);
                }
                else if (player1Active)
                {
                    GameObject.FindGameObjectWithTag("Player1").SetActive(true);
                }

                if (!player2Active)
                {
                    GameObject.FindGameObjectWithTag("Player2").SetActive(false);
                }
                else
                {
                    GameObject.FindGameObjectWithTag("Player2").SetActive(true);
                }

                if (!player3Active)
                {
                    GameObject.FindGameObjectWithTag("Player3").SetActive(false);
                }
                else
                {
                    GameObject.FindGameObjectWithTag("Player3").SetActive(true);
                }
                break;

            case "GameOver":
                if (Input.GetKey(KeyCode.LeftArrow) || InputManager.Devices[0].Command)
                {
                    Application.LoadLevel(0);
                }
                break;

            case "GameOver2":
                if (Input.GetKey(KeyCode.LeftArrow) || InputManager.Devices[0].Command)
                {
                    Application.LoadLevel(0);
                }
                break;
        }        
    }

    void OnLevelWasLoaded(int level)
    {
        if (level == 0)
        {
            AkSoundEngine.SetState("GameScreen", "TitleScreen");
        }
        if (level == 1)
        {
            AkSoundEngine.SetState("GameScreen", "InGame");
        }
        if (level == 2)
        {
            AkSoundEngine.SetState("GameScreen", "Scoreboard");
            AkSoundEngine.SetState("isBoosting", "None");
        }
        if (level == 4)
        {
            AkSoundEngine.SetState("GameScreen", "death");
        }
        if (level == 5)
        {
            AkSoundEngine.SetState("GameScreen", "death");
        }
        if (level == 6)
        {
            AkSoundEngine.SetState("GameScreen", "credits");
        }
    }
}
