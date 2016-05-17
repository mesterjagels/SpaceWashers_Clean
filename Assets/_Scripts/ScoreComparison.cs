using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ScoreComparison : MonoBehaviour
{
    public GameManager gameManager;

    public Text scoreText0;
    public Text scoreText1;
    public Text scoreText2;
    public Text scoreText3;
    public Text scoreText4;
    public Text scoreText5;
    private bool scoresSet = false;
    public float scoreTimer = 4f;
    private Sequence scoreSequence = DOTween.Sequence();

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    void Update()
    {
        if (!scoresSet)
        {
            scoreSequence
            .Append(scoreText0.DOText(gameManager.totalScore.ToString(), scoreTimer, true, ScrambleMode.Numerals))
            .Append(scoreText1.DOText(gameManager.captainScore.ToString(), scoreTimer, true, ScrambleMode.Numerals))
            .Append(scoreText2.DOText(gameManager.cleanerScore0.ToString(), scoreTimer, true, ScrambleMode.Numerals))
            .Append(scoreText3.DOText(gameManager.cleanerScore1.ToString(), scoreTimer, true, ScrambleMode.Numerals))
            .Append(scoreText4.DOText(gameManager.cleanerScore2.ToString(), scoreTimer, true, ScrambleMode.Numerals));
            /*
            scoreText0.text = gameManager.totalScore.ToString();
            scoreText1.text = gameManager.captainScore.ToString();
            scoreText2.text = gameManager.cleanerScore0.ToString();
            scoreText3.text = gameManager.cleanerScore1.ToString();
            scoreText4.text = gameManager.cleanerScore2.ToString();
            */
            if (gameManager.penalty > 0)
            {
                scoreText5.text = "WELL FUCKING JOB CAPTAIN! YOU ARRIVED " + Mathf.RoundToInt(gameManager.timeLeftBuffer) + " SECONDS EARLY. BUT YOU RETURNED THE SHIP DIRTY! MOM IS NOT HAPPY ABOUT THIS AND SUBTRACTS " + gameManager.penalty.ToString() + " POINTS FROM YOUR TOTAL SCORE.";
            }
            else
                scoreText5.text = "WELL FUCKING JOB CAPTAIN! YOU ARRIVED " + Mathf.RoundToInt(gameManager.timeLeftBuffer) + " SECONDS EARLY. YOU RETURNED THE SHIP SAFE AND CLEAN! MOM HAS NO IDEA...";

            scoresSet = true;
        }
    }
}
