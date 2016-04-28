using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PolygonCollider2D))]
public class DirtController : MonoBehaviour {
    //private Scoreboard scoreboard;

    void Start()
    {
        //scoreboard = Gameobject.findObjectOfType<Scoreboard>();
    }

    void OnDisable()
    {
        //add points to scoreController
    }
}
