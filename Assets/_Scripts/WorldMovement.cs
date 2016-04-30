using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class WorldMovement : MonoBehaviour {
    private Rigidbody2D rb;
    private SpaceshipController spaceship;
    [Header("Tweak to adjust object speed")]
    [Range(0f, 1f)]
    public float speedmultiplier;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        spaceship = GameObject.FindGameObjectWithTag("Spaceship").GetComponent<SpaceshipController>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        rb.velocity = new Vector2(spaceship.horizontalSpeed * speedmultiplier * -1, spaceship.verticalSpeed * speedmultiplier * -1);
	}
}
