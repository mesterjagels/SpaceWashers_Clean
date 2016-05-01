using UnityEngine;

public class DirtController : MonoBehaviour {
    //private Scoreboard scoreboard;

    private ScoreAlpha score;
    private Destructible2D.D2dDestructible d2d;
    private float startAlpha, currentAlpha;
    public float thresholdAlpha = 50;
    public float alphaPct;

    void Start()
    {
        d2d = GetComponent<Destructible2D.D2dDestructible>();
      score = GameObject.Find("Scoreboard").GetComponent<ScoreAlpha>();
   
        startAlpha = d2d.OriginalAlphaCount;
    }

    void Update() {
        currentAlpha = d2d.AlphaCount;
        alphaPct = (currentAlpha / startAlpha) * 100;
        if (alphaPct < thresholdAlpha)
        {
            Debug.Log("Threshold reached");
            gameObject.SetActive(false);
        }
    }

    //The OnDisable method could also be OnDestroy() depending on how we remove dirt.
    void OnDisable()
    {
       score.score = score.score + 1000;
     
    }
}
