using UnityEngine;
using System.Collections;

public class SoapVelocity : MonoBehaviour
{
    SpaceshipController spaceship;
    public GameObject soapBubblesBabe;
    public GameObject soapBubblesGangster;
    public GameObject soapBubblesGrunger;


    // Use this for initialization
    void Start ()
    {
        spaceship = GameObject.FindGameObjectWithTag("Spaceship").GetComponent<SpaceshipController>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        soapBubblesBabe.GetComponent<ParticleSystem>().gravityModifier = spaceship.verticalSpeed / 200;
        soapBubblesGangster.GetComponent<ParticleSystem>().gravityModifier = spaceship.verticalSpeed / 200;
        soapBubblesGrunger.GetComponent<ParticleSystem>().gravityModifier = spaceship.verticalSpeed / 200;
    }
}
