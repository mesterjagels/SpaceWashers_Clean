using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreAlpha : MonoBehaviour
{
    private GameManager gm;
    private ScrollUV uvOffset;
    public int score = 0;
    public Text scoreText;
    public Text distanceLeft;
    public float distanceMath;
    string scoreString;
    public float distanceToTravel = 100;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        uvOffset = GameObject.Find("StarFieldFG").GetComponent<ScrollUV>();
    }

	void Update () {

        scoreString = gm.totalScore.ToString();
        scoreText.text = "SCORE: " + scoreString;

        distanceMath = distanceToTravel - uvOffset.offset.y;
        distanceLeft.text = "DISTANCE LEFT: " + distanceMath.ToString();
	}
}
