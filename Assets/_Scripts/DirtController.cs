using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(PolygonCollider2D))]
public class DirtController : MonoBehaviour {
    //private Scoreboard scoreboard;
    private ScoreAlpha score;

    void Start()
    {
        score = GameObject.Find("Scoreboard").GetComponent<ScoreAlpha>();
        //scoreboard = Gameobject.findObjectOfType<Scoreboard>();
    }

    //The OnDisable method could also be OnDestroy() depending on how we remove dirt.
    void OnDisable()
    {
        score.score = score.score + 1000;
        //score = score + "100";
        //add points to scoreController
    }
}
