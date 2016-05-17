using UnityEngine;
using System.Collections;
using DG.Tweening;

public class UIProgressPanel : MonoBehaviour
{
    GameManager gameManager;
    public GameObject spaceShipLocationIndicator;
    public GameObject momLocationIndicator;
    Vector3 spaceShip;
    Vector3 mom;
    float initialTimeLeft;
    float initialDistanceMath;
    float momY;
    float spaceShipY;

    // Use this for initialization
    void Start ()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        initialTimeLeft = gameManager.timeLeft;
        initialDistanceMath = gameManager.distanceMath;

        //momY = ConvertRange(initialTimeLeft, -0.001f, -4.537f, 4.703f, gameManager.timeLeft);
        //spaceShipY = ConvertRange(initialDistanceMath, -0.001f, -4.642f, 4.703f, gameManager.distanceMath);
        //momY = Mathf.Clamp(-gameManager.timeLeft, 19.463f, 28.703f);
        //spaceShipY = Mathf.Clamp(-gameManager.distanceMath, 19.358f, 28.703f);
        //momLocationIndicator.transform.DOLocalMoveY(28.703f, initialTimeLeft,false);
       // mom = new Vector3(6.377f, momY, 1.1f);
       // spaceShip = new Vector3(6.382f, spaceShipY, 1.1f);
    }
	
	// Update is called once per frame
	void Update ()
    {
        momY = ConvertRange(initialTimeLeft, -0.001f, 19.463f, 28.703f, gameManager.timeLeft);
        spaceShipY = ConvertRange(gameManager.distanceToTravel, -0.001f, 19.358f, 28.703f, gameManager.distanceMath);
       // momY = Mathf.Clamp(-gameManager.timeLeft, 19.463f, 28.703f);
        //spaceShipY = Mathf.Clamp(-gameManager.distanceMath, 19.358f, 28.703f);

        mom = new Vector3(6.377f, momY, 1.1f);
        spaceShip = new Vector3(6.382f, spaceShipY, 1.1f);

        Debug.Log("Mom"+momY);
        Debug.Log("Spaceship"+spaceShipY);
        

        spaceShipLocationIndicator.transform.position= spaceShip;
        momLocationIndicator.transform.position = mom;
    }

    public static float ConvertRange(float originalStart, float originalEnd/* original range */, float newStart, float newEnd/* desired range*/, float value) // value to convert
    {
        float scale = (float)(newEnd - newStart) / (originalEnd - originalStart);
        return (float)(newStart + ((value - originalStart) * scale));
    }
}
