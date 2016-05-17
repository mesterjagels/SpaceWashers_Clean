using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HighlightHighScore : MonoBehaviour
{
    GameManager gameManager;
    //public Text[] scoreText = new Text[10];
    public Text scoreText0;
    public Text scoreText1;
    public Text scoreText2;
    public Text scoreText3;
    public Text scoreText4;
    public Text scoreText5;
    public Text scoreText6;
    public Text scoreText7;
    public Text scoreText8;
    public Text scoreText9;

    // Use this for initialization
    void Start ()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

        /*for (int i = 0; i < 10; i++)
        {
           if (gameManager.newHighScore == i)
            {
                scoreText[i].GetComponent<Text>().color = gameManager.gangsta;
                i = 10;
            }
        }*/

        if (gameManager.newHighScore == 0)
        {
            scoreText0.GetComponent<Text>().color = gameManager.gangsta;
        }
        else if (gameManager.newHighScore == 1)
        {
            scoreText1.GetComponent<Text>().color = gameManager.gangsta;
        }
        else if (gameManager.newHighScore == 2)
        {
            scoreText2.GetComponent<Text>().color = gameManager.gangsta;
        }
        else if (gameManager.newHighScore == 3)
        {
            scoreText3.GetComponent<Text>().color = gameManager.gangsta;
        }
        else if (gameManager.newHighScore == 4)
        {
            scoreText4.GetComponent<Text>().color = gameManager.gangsta;
        }
        else if (gameManager.newHighScore == 5)
        {
            scoreText5.GetComponent<Text>().color = gameManager.gangsta;
        }
        else if (gameManager.newHighScore == 6)
        {
            scoreText6.GetComponent<Text>().color = gameManager.gangsta;
        }
        else if (gameManager.newHighScore == 7)
        {
            scoreText7.GetComponent<Text>().color = gameManager.gangsta;
        }
        else if (gameManager.newHighScore == 8)
        {
            scoreText8.GetComponent<Text>().color = gameManager.gangsta;
        }
        else if (gameManager.newHighScore == 9)
        {
            scoreText9.GetComponent<Text>().color = gameManager.gangsta;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
	    
	}
}
