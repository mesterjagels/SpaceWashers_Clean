using UnityEngine;
using UnityEngine.UI;

public class ScoreComparison : MonoBehaviour {

    public GameManager gameManager;

    public Text scoreText0;
    public Text scoreText1;
    public Text scoreText2;
    public Text scoreText3;
    public Text scoreText4;
    private bool scoresSet = false;

    void Start()
    {

    }

    void Update()
    {
        if(gameManager == null)
        {
            gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

            if (!scoresSet)
            {
                scoreText0.text = gameManager.totalScore.ToString();
                scoreText1.text = gameManager.captainScore.ToString();
                scoreText2.text = gameManager.cleanerScore0.ToString();
                scoreText3.text = gameManager.cleanerScore1.ToString();
                scoreText4.text = gameManager.cleanerScore2.ToString();

                scoresSet = true;
            }
        }
    }
}
