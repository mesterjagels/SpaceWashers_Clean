using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreComparison : MonoBehaviour {

    private GameManager gameManager;

    public Text scoreText0;
    public Text scoreText1;
    public Text scoreText2;
    public Text scoreText3;
    public Text scoreText4;
    

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

        //UI Elements
        scoreText0.text = "TOTAL SCORE" + gameManager.totalScore;
        scoreText1.text = "CAPTAIN DUDE" + gameManager.captainScore;
        scoreText2.text = "FEMALE DUDE" + gameManager.cleanerScore[0];
        scoreText3.text = "BLACK DUDE" + gameManager.cleanerScore[1];
        scoreText4.text = "TOILETBRUSH DUDE" + gameManager.cleanerScore[2];
    }

    void Update()
    {

    }
}
