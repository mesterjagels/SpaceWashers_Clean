using UnityEngine;
using System.Collections;

public class NoMoreThruster : MonoBehaviour {

    private GameManager gm;
    private ParticleSystem ps;
    
	void Start () {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        ps = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
	if(gm.distanceMath < 0.5f)
        {
            if(ps.enableEmission == true) {
                ps.enableEmission = false;
            }
        }
	}
}
