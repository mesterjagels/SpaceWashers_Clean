using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreAlpha : MonoBehaviour {

    private ScrollUV uvOffset;
    public int score = 0;
    public Text scoreText;
    public Text distanceLeft;
    private float distanceMath;
    string scoreString;
    public static float distanceToTravel = 100;

    void Start()
    {
        uvOffset = GameObject.Find("StarFieldFG").GetComponent<ScrollUV>();
    }

	void Update () {
        Debug.Log("ScoreAlpha.uvOffset = " + uvOffset);
        scoreString = score.ToString();
        scoreText.text = "SCORE: " + scoreString;
        distanceMath = distanceToTravel - uvOffset.offset.y;
        distanceLeft.text = "DISTANCE LEFT: " + distanceMath.ToString();
	}
}
