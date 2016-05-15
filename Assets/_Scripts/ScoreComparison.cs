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
    private bool scoresSet = false;
    public float scoreTimer = 5f;
    private Sequence scoreSequence = DOTween.Sequence();

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

    }

    void Update()
    {
        if (!scoresSet)
        {
            /*scoreSequence
            .Append(scoreText0.DOText(gameManager.totalScore.ToString(), scoreTimer, true, ScrambleMode.Numerals))
            .Append(scoreText1.DOText(gameManager.captainScore.ToString(), scoreTimer, true, ScrambleMode.Numerals))
            .Append(scoreText2.DOText(gameManager.cleanerScore0.ToString(), scoreTimer, true, ScrambleMode.Numerals))
            .Append(scoreText3.DOText(gameManager.cleanerScore1.ToString(), scoreTimer, true, ScrambleMode.Numerals))
            .Append(scoreText4.DOText(gameManager.cleanerScore2.ToString(), scoreTimer, true, ScrambleMode.Numerals));*/

            scoreText0.text = gameManager.totalScore.ToString();
            scoreText1.text = gameManager.captainScore.ToString();
            scoreText2.text = gameManager.cleanerScore0.ToString();
            scoreText3.text = gameManager.cleanerScore1.ToString();
            scoreText4.text = gameManager.cleanerScore2.ToString();
                
            scoresSet = true;
        }
    }
}
