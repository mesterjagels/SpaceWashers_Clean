using UnityEngine;
using System.Collections;

public class ScoreBubbleController : MonoBehaviour
{
    private static ScoreBubble scoreBubble;
    private static GameObject canvas;

    public static void Initialize()
    {
        canvas = GameObject.Find("Canvas");
        if (!scoreBubble)
            scoreBubble = Resources.Load<ScoreBubble>("Prefabs/PopupTextParent");
    }

    public static void CreateScoreBubble(string text, Transform location)
    {
        ScoreBubble instance = Instantiate(scoreBubble);
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(new Vector2(location.position.x + Random.Range(-.2f, .2f), location.position.y + Random.Range(-.2f, .2f)));

        instance.transform.SetParent(canvas.transform, false);
        instance.transform.position = screenPosition;
        instance.SetText(text);
    }
}

