using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ScoreAlpha : MonoBehaviour
{
    private GameManager gameManager;
    private ScrollUV uvOffset;
    public int score = 0;
    public Text scoreText;
    public Text distanceLeft;
    public Text momReturn;
    public float timeLeft = 60;
    public float distanceMath;
    string scoreString;
    public float distanceToTravel = 100;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        uvOffset = GameObject.Find("StarFieldFG").GetComponent<ScrollUV>();
    }

	void Update ()
    {
        if(scoreString != gameManager.totalScore.ToString())
        {
            scoreString = gameManager.totalScore.ToString();
            //    scoreText.DOText("SCORE: " + scoreString, 0.5f, true, ScrambleMode.Numerals);
            scoreText.text = "SCORE: " + scoreString;
        }
        
        distanceMath = distanceToTravel - uvOffset.offset.y;
        distanceLeft.text = "DISTANCE LEFT: " + distanceMath.ToString();

        if (distanceMath <= 0)
        {
            Debug.Log("Distance <= 0");
            gameManager.EndGame();
        }

        timeLeft -= Time.deltaTime;
        momReturn.text = "MOM WILL BE HOME IN: " + timeLeft;

        if (timeLeft < 0)
        {
            gameManager.GameOver();
        }
    }
}
