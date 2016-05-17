using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ScoreAlpha : MonoBehaviour
{
    private GameManager gameManager;
    private ScrollUV uvOffset;
    public int score = 0;
    public Text scoreText;
    //public Text distanceLeft;
    //public Text momReturn;
    public Text dirtiness;
    string scoreString;

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
        
        gameManager.distanceMath = gameManager.distanceToTravel - uvOffset.offset.y;
        //distanceLeft.text = "DISTANCE LEFT: " + gameManager.distanceMath.ToString();

        if (gameManager.distanceMath <= 0)
        {
            Debug.Log("Distance <= 0");
            gameManager.EndGame();
        }

        gameManager.timeLeft -= Time.deltaTime;
        //momReturn.text = "MOM'S HOME IN: " + gameManager.timeLeft;

        if (gameManager.timeLeft < 0)
        {
            gameManager.GameOver();
        }

        dirtiness.text = "CLEANLINESS: "+ (100-gameManager.cleanliness).ToString();
    }
}
