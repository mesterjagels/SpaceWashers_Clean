using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreBubble : MonoBehaviour
{
    public GameObject TrackingPoint;

    private RectTransform ButtonRect = null;
    private RectTransform CanvasRect = null;

    private Vector3 worldPos;
    private Vector2 screenPos;
    private Vector2 canvasPos;


    void Awake()
    {
        ButtonRect = GetComponent<RectTransform>();
        CanvasRect = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
    }

    void Update()
    {
        worldPos = TrackingPoint.transform.position;
        screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, worldPos);

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(CanvasRect, screenPos, null, out canvasPos))
        {
            ButtonRect.anchoredPosition = 0.5f * CanvasRect.sizeDelta + canvasPos;
        }
    }
}
