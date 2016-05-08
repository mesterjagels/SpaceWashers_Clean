using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreBeta : MonoBehaviour
{
    private GameManager gameManager;

    public Text scoreText1;
    public Text scoreText2;
    public Text scoreText3;
    public Text scoreText4;
    public Text scoreText5;
    public Text scoreText6;
    public Text scoreText7;
    public Text scoreText8;
    public Text scoreText9;
    public Text scoreText10;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

        gameManager.GetHighscore();

        //UI Elements
        scoreText1.text = " 1.   " + gameManager.highscoreListAsArrayOfStrings[0];
        scoreText2.text = " 2.   " + gameManager.highscoreListAsArrayOfStrings[1];
        scoreText3.text = " 3.   " + gameManager.highscoreListAsArrayOfStrings[2];
        scoreText4.text = " 4.   " + gameManager.highscoreListAsArrayOfStrings[3];
        scoreText5.text = " 5.   " + gameManager.highscoreListAsArrayOfStrings[4];
        scoreText6.text = " 6.   " + gameManager.highscoreListAsArrayOfStrings[5];
        scoreText7.text = " 7.   " + gameManager.highscoreListAsArrayOfStrings[6];
        scoreText8.text = " 8.   " + gameManager.highscoreListAsArrayOfStrings[7];
        scoreText9.text = " 9.   " + gameManager.highscoreListAsArrayOfStrings[8];
        scoreText10.text = "10.   " + gameManager.highscoreListAsArrayOfStrings[9];
    }

    void Update()
    {
        
    }
}
   