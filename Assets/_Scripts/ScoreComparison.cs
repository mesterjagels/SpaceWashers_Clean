using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

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
        scoreText0.text = gameManager.totalScore.ToString();
        scoreText1.text = gameManager.captainScore.ToString();
        scoreText2.text = gameManager.cleanerScore0.ToString();
        scoreText3.text = gameManager.cleanerScore1.ToString();
        scoreText4.text = gameManager.cleanerScore2.ToString();
    }

    void Update()
    {
    }
}
