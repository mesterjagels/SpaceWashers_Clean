using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class WorldMovement : MonoBehaviour {
    Rigidbody2D rb;
    SpaceshipController spaceship;
    [Header("Tweak to adjust object speed")]
    [Range(0f, 10f)]
    public float speedmultiplier;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        spaceship = GameObject.FindGameObjectWithTag("Spaceship").GetComponent<SpaceshipController>();
	}
	
	// Update is called once per frame
	void Update () {
        rb.velocity = new Vector2(spaceship.horizontalSpeed * speedmultiplier * -1, spaceship.verticalSpeed * speedmultiplier * -1);
	}
}
