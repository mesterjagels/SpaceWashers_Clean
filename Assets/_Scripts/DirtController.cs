using UnityEngine;

public class DirtController : MonoBehaviour {
    //private Scoreboard scoreboard;

    private ScoreAlpha score;
    private Destructible2D.D2dDestructible d2d;
    private float startAlpha, currentAlpha;
    public float thresholdAlpha = 50;
    public float alphaPct;
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
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
        int cleaningPlayer = 0;
        int i = 0;
        CleanerController[] cleanerControllers = FindObjectsOfType(typeof (CleanerController)) as CleanerController[];

        float[] distance = new float[cleanerControllers.Length];
        Vector2[] positions = new Vector2[cleanerControllers.Length];

        foreach (CleanerController cleanerController in cleanerControllers)
        {
            distance[i] = Vector2.Distance(cleanerController.transform.position, gameObject.transform.position);
           // Debug.Log("Player" + (i+ 1) + " distance to dirt is " + distance[i]);
            i++;
        }

        float shortestDistance = Mathf.Min(distance);

       if (shortestDistance == distance[0])
        {
            //Debug.Log("Player1 should get points because it have the shortest distance of: " + distance[0]);
            cleaningPlayer = 0;
        }
        else if (cleanerControllers.Length > 1 && shortestDistance == distance[1])
        {
           // Debug.Log("Player2 should get points  because it have the shortest distance of: " + distance[1]);
            cleaningPlayer = 1;
        }
        else if(cleanerControllers.Length > 2 && shortestDistance == distance[2])
        {
           //Debug.Log("Player3 should get points because it have the shortest distance of: " + distance[2])
            cleaningPlayer = 2;
        }
<<<<<<< HEAD

        gameManager.AddCleanerScore(cleaningplayer);
=======
        
        gameManager.AddCleanerScore(cleaningPlayer);
>>>>>>> Scorescreen
       // score.score = score.score + 1000;
    }
}
