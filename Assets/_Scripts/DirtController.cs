using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PolygonCollider2D))]
public class DirtController : MonoBehaviour {
    //private Scoreboard scoreboard;

    void Start()
    {
        //scoreboard = Gameobject.findObjectOfType<Scoreboard>();
    }

    //The OnDisable method could also be OnDestroy() depending on how we remove dirt.
    void OnDisable()
    {
        //add points to scoreController
    }
}
